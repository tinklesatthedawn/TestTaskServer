using Application.UseCases.Register.GetAll;
using Application.UseCases.RegisterValue.GetAll;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.RegisterValue.Operations
{
    internal class GetAllOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            GetAllRegisterValue usecase = DependencyInjection.DI.Get<GetAllRegisterValue>();
            var result = await usecase.Handle(cancellationToken);
            var dtos = RegisterValueDto.OfArray(result.RegisterValues);
            string body = RegisterValueDto.ArrayToJson(dtos);
            return new HttpControllerResponse(System.Net.HttpStatusCode.OK, body);
        }
    }
}
