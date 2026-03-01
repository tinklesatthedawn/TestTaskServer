using Application.UseCases.Device.GetById;
using Application.UseCases.Register.GetById;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Register.Operations
{
    internal class GetByIdOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            int id;
            if (int.TryParse(request.Arguments.Get("Id"), out id))
            {
                GetByIdRegister usecase = DependencyInjection.DI.Get<GetByIdRegister>();
                var result = await usecase.Handle(id, cancellationToken);
                var registerResponded = RegisterDto.Of(result.Register);
                if (registerResponded != null)
                {
                    string json = registerResponded.ToJson();
                    return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
                }
                throw new IdNotExistException(this, id);
            }
            throw new QueryInvalidException(this, "Id", request.Arguments.Get("Id"));
        }
    }
}
