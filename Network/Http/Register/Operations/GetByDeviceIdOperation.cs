using Application.UseCases.Device.GetByInterfaceId;
using Application.UseCases.Register.GetByDeviceId;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Register.Operations
{
    internal class GetByDeviceIdOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            int id;
            if (int.TryParse(request.Arguments.Get("Id"), out id))
            {
                GetByDeviceIdRegister usecase = DependencyInjection.DI.Get<GetByDeviceIdRegister>();
                var result = await usecase.Handle(id, cancellationToken);
                var registerResponded = RegisterDto.OfArray(result.Registers);
                string json = RegisterDto.ArrayToJson(registerResponded);
                return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
            }
            throw new QueryInvalidException(this, "Id", request.Arguments.Get("Id"));
        }
    }
}
