using DependencyInjection;
using Domain.Entities;
using Logger;
using Network.Exceptions;
using Network.Http.Device;
using Network.Http.Exceptions;
using Network.Http.Interface;
using Network.Http.Register;
using Network.Http.RegisterValue;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Network.Http
{
    internal abstract class HttpController
    {
        public delegate HttpController CreateController(HttpControllerRequest request);
        protected HttpControllerRequest _request;

        private static Dictionary<string, CreateController> _routes = new Dictionary<string, CreateController>()
        {
            {"interface", (request) => new InterfaceController(request)},
            {"device", (request) => new DeviceController(request)},
            {"register", (request) => new RegisterController(request)},
            { "registervalue", (request) => new RegisterValueController(request)}
        };

        public HttpController(HttpControllerRequest request)
        {
            _request = request;
        }

        public virtual async Task<HttpControllerResponse> Handle(CancellationToken cancellationToken)
        {
            foreach (var item in GetRoutes())
            {
                if (_request.Operation == item.Key)
                {
                    string errorMessage = string.Empty;
                    try
                    {
                        var result = await item.Value.Execute(_request, cancellationToken);
                        await GetLogger().WriteInformationMessage(GetType().ToString(), "Successfully responded", result.Body, GetLoggerCancellationToken());
                        return result;
                    }
                    catch (OperationCanceledException e)
                    {
                        string title = "Operation time is out";
                        errorMessage = title + "|" + e.Message;
                        var source = e.Source ?? GetType().ToString();
                        await GetLogger().WriteWarningMessage(source, title, e.Message, GetLoggerCancellationToken());
                    }
                    catch (EntityFailedCreationException e)
                    {
                        string title = "Entity creation failed";
                        errorMessage = title + "|" + e.Entity.ToString();
                        var source = e.Source ?? GetType().ToString();
                        await GetLogger().WriteInformationMessage(source, title, e.Entity.ToString(), GetLoggerCancellationToken());
                    }
                    catch (QueryInvalidException e)
                    {
                        string title = "Request query string is invalid";
                        errorMessage = title + "|" + $"{e.ArgumentName}: {e.ArgumentValue}";
                        var source = e.Source ?? GetType().ToString();
                        await GetLogger().WriteInformationMessage(source, title, $"{e.ArgumentName}: {e.ArgumentValue}", GetLoggerCancellationToken());
                    }
                    catch (EntityNotExistException e)
                    {
                        string title = "Entity does not exist";
                        errorMessage = title + "|" + e.Entity.ToString();
                        var source = e.Source ?? GetType().ToString();
                        await GetLogger().WriteInformationMessage(source, title, e.Entity.ToString(), GetLoggerCancellationToken());
                    }
                    catch (IdNotExistException e)
                    {
                        string title = "Entity does not exist";
                        errorMessage = title + "|" + e.Id.ToString();
                        var source = e.Source ?? GetType().ToString();
                        await GetLogger().WriteInformationMessage(source, title, e.Id.ToString(), GetLoggerCancellationToken());
                    }
                    catch (Exception e)
                    {
                        string title = "Unknown error occured";
                        errorMessage = title + "|" + e.Message;
                        var source = e.Source ?? GetType().ToString();
                        await GetLogger().WriteErrorMessage(source, title, e.Message, GetLoggerCancellationToken());
                    }
                    return new HttpControllerResponse(HttpStatusCode.BadRequest, [], errorMessage);
                }   
            }
            
            return HttpControllerResponse.Error_404();
        }

        protected abstract Dictionary<string, IHttpControllerOperation> GetRoutes();

        public static HttpController Get(HttpControllerRequest request)
        {
            foreach (var item in _routes)
            {
                if (item.Key == request.Domain) return item.Value(request);
            }
            return new Error404Controller();
        }

        private ILogger GetLogger()
        {
            return DI.Get<ILogger>();
        }

        private CancellationToken GetLoggerCancellationToken()
        {
            using (CancellationTokenSource source = new CancellationTokenSource())
            {
                source.CancelAfter(5000);
                return source.Token;
            }
        }

        //Controller for error 404
        private class Error404Controller : HttpController
        {
            public Error404Controller() : base(new HttpControllerRequest(string.Empty, string.Empty, [], string.Empty)) { }

            protected override Dictionary<string, IHttpControllerOperation> GetRoutes()
            {
                return [];
            }
        }
    }
}
