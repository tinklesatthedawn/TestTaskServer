using Application.UseCases.Device.GetAll;
using Application.UseCases.Register.GetAll;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Register.Operations
{
    internal class GetAllOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            GetAllRegister usecase = DependencyInjection.DI.Get<GetAllRegister>();
            var result = await usecase.Handle(cancellationToken);
            var dtos = RegisterDto.OfArray(result.Registers);
            string body = RegisterDto.ArrayToJson(dtos);
            return new HttpControllerResponse(System.Net.HttpStatusCode.OK, body);
        }
    }
}
