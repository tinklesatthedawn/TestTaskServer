using Application.UseCases.Interface.GetAll;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Interface.Operations
{
    internal class GetAllOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            GetAllInterface usecase = DependencyInjection.DI.Get<GetAllInterface>();
            var result = await usecase.Handle(cancellationToken);
            var dtos = InterfaceDto.OfArray(result.Interfaces);
            string body = InterfaceDto.ArrayToJson(dtos);
            return new HttpControllerResponse(System.Net.HttpStatusCode.OK, body);
        }
    }
}
