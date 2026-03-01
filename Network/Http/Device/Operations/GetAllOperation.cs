using Application.UseCases.Device.GetAll;
using Application.UseCases.Interface.GetAll;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Device.Operations
{
    internal class GetAllOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            GetAllDevice usecase = DependencyInjection.DI.Get<GetAllDevice>();
            var result = await usecase.Handle(cancellationToken);
            var dtos = DeviceDto.OfArray(result.Devices);
            string body = DeviceDto.ArrayToJson(dtos);
            return new HttpControllerResponse(System.Net.HttpStatusCode.OK, body);
        }
    }
}
