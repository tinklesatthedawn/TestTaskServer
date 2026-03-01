using DependencyInjection;
using Logger;
using Network.TCP;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Network.Http
{
    public class HttpConnection
    {
        private IPAddress _ip;
        private int _port;
        private HttpListener _httpListener;
        private bool _disposed = false;

        //http://127.0.0.1:7777/
        public HttpConnection(IPAddress ip, int port)
        {
            _ip = ip;
            _port = port;
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add(GetHomePageUrl());
        }

        public async void StartRequestsListening()
        {
            _httpListener.Start();
            await GetLogger().WriteInformationMessage(GetType().ToString(), "Http server started", "Http Connection started", CancelAfter(2000));
            while (!_disposed)
            {
                try
                {
                    var token = CancelAfter(2000);
                    var client = await _httpListener.GetContextAsync();
                    var response = await GetResponse(client, token);
                    await WriteResponse(client, response, token);
                }
                catch (OperationCanceledException) { }
                catch (HttpListenerException) { }
                catch (Exception e)
                {
                    await GetLogger().WriteErrorMessage(GetType().ToString(), "CRITICAL ERROR", e.Message, CancelAfter(2000));
                } 
            }
        }

        private string GetHomePageUrl()
        {
            return $"http://{_ip}:{_port}/";
        }

        private async Task<HttpControllerResponse> GetResponse(HttpListenerContext client, CancellationToken cancellationToken)
        {
            var request = ParseRequest(client);
            return await HttpController.Get(request).Handle(cancellationToken);
        }

        private HttpControllerRequest ParseRequest(HttpListenerContext client)
        {
            var httpListenerRequest = client.Request;
            string path = httpListenerRequest.Url?.AbsolutePath.Trim('/') ?? string.Empty;
            var domainAndOperation = path.Split('/', 2);

            string domain = domainAndOperation.ElementAtOrDefault(0) ?? string.Empty;
            string operation = domainAndOperation.ElementAtOrDefault(1) ?? string.Empty;
            var arguments = client.Request.QueryString;
            string body = client.Request.HasEntityBody ? ReadRequestBody(client.Request) : string.Empty;
            
            return new HttpControllerRequest(domain, operation, arguments, body);
        }

        private string ReadRequestBody(HttpListenerRequest request)
        {
            using (Stream body = request.InputStream)
            {
                using (var reader = new StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private async Task WriteResponse(HttpListenerContext client, HttpControllerResponse serverResponse, CancellationToken cancellationToken)
        {       
            var response = client.Response;

            response.StatusCode = (int)serverResponse.StatusCode;
            foreach (var item in serverResponse.Headers)
            {
                response.Headers.Add(item.Key, item.Value);
            }

            byte[] buffer = Encoding.UTF8.GetBytes(serverResponse.Body);
            response.ContentLength64 = buffer.Length;
            using Stream output = response.OutputStream;
            await output.WriteAsync(buffer, cancellationToken);
            await output.FlushAsync(cancellationToken);
        }

        public async void StopRequestListening()
        {
            _disposed = true;
            _httpListener.Stop();
            await GetLogger().WriteInformationMessage(GetType().ToString(), "Http server stopped", "Http Connection stoped", CancelAfter(2000));
        }

        private CancellationToken CancelAfter(int ms)
        {
            using (CancellationTokenSource source = new CancellationTokenSource())
            {
                source.CancelAfter(ms);
                return source.Token;
            }
        }

        private ILogger GetLogger()
        {
            return DI.Get<ILogger>();
        }
    }
}
