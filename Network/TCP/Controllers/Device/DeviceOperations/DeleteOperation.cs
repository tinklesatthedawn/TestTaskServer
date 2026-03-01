using Application.UseCases.Device.Create;
using Application.UseCases.Device.Delete;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.Device.DeviceOperations
{
    internal class DeleteOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            var device = DeviceDto.FromJson(arguments)?.ToEntity();
            if (device == null) return string.Empty;
            DeleteDevice usecase = DependencyInjection.DI.Get<DeleteDevice>();
            var result = await usecase.Handle(device, cancellationToken);
            var dto = DeviceDto.Of(result.Device);
            return dto?.ToJson() ?? string.Empty;
        }
    }
}
