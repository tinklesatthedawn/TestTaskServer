using Application.UseCases.Register.GetByDeviceId;
using Application.UseCases.RegisterValue.GetByRegisterId;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.RegisterValue.Operations
{
    internal class GetByRegisterIdOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            int id;
            if (int.TryParse(request.Arguments.Get("Id"), out id))
            {
                GetByRegisterIdRegisterValue usecase = DependencyInjection.DI.Get<GetByRegisterIdRegisterValue>();
                var result = await usecase.Handle(id, cancellationToken);
                var registerValuesResponded = RegisterValueDto.OfArray(result.RegisterValues);
                string json = RegisterValueDto.ArrayToJson(registerValuesResponded);
                return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
            }
            throw new QueryInvalidException(this, "Id", request.Arguments.Get("Id"));
        }
    }
}
