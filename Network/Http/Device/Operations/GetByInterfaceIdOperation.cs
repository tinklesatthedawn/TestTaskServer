using Application.UseCases.Device.GetById;
using Application.UseCases.Device.GetByInterfaceId;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Device.Operations
{
    internal class GetByInterfaceIdOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            int id;
            if (int.TryParse(request.Arguments.Get("Id"), out id))
            {
                GetByInterfaceIdDevice usecase = DependencyInjection.DI.Get<GetByInterfaceIdDevice>();
                var result = await usecase.Handle(id, cancellationToken);
                var devicesResponsed = DeviceDto.OfArray(result.Devices);
                string json = DeviceDto.ArrayToJson(devicesResponsed);
                return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
            }
            throw new QueryInvalidException(this, "Id", request.Arguments.Get("Id"));
        }
    }
}
