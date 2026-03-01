using Application.UseCases.Device.GetById;
using Application.UseCases.Interface.GetById;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Device.Operations
{
    internal class GetByIdOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            int id;
            if (int.TryParse(request.Arguments.Get("Id"), out id))
            {
                GetByIdDevice usecase = DependencyInjection.DI.Get<GetByIdDevice>();
                var result = await usecase.Handle(id, cancellationToken);
                var deviceResponsed = DeviceDto.Of(result.Device);
                if (deviceResponsed != null)
                {
                    string json = deviceResponsed.ToJson();
                    return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
                }
                throw new IdNotExistException(this, id);
            }
            throw new QueryInvalidException(this, "Id", request.Arguments.Get("Id"));
        }
    }
}
