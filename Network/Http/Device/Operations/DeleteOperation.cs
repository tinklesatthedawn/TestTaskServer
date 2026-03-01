using Application.UseCases.Device.Create;
using Application.UseCases.Device.Delete;
using Application.UseCases.Interface.Create;
using Domain.Entities;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Network.Http.Device.Operations
{
    internal class DeleteOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            var device = GetFromRequest(request);
            if (device != null)
            {
                DeleteDevice usecase = DependencyInjection.DI.Get<DeleteDevice>();
                var result = await usecase.Handle(device, cancellationToken);
                var deviceResponsed = DeviceDto.Of(result.Device);
                if (deviceResponsed != null)
                {
                    string json = deviceResponsed.ToJson();
                    return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
                }
                throw new EntityNotExistException(this, device);
            }
            throw new UnknownErrorException(this);
        }

        private DeviceEntity? GetFromRequest(HttpControllerRequest request)
        {
            var device = DeviceDto.FromJson(request.Body)?.ToEntity();
            if (device == null)
            {
                var arguments = request.Arguments;
                int id;

                if (!int.TryParse(arguments.Get("Id"), out id)) throw new QueryInvalidException(this, "Id", arguments.Get("Id"));
                
                DeviceEntity entity = new DeviceEntity();
                entity.Id = id;
                
                return entity;
            }
            return device;
        }
    }
}
