using Application.UseCases.Device.Update;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.Device.DeviceOperations
{
    internal class UpdateOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            var device = DeviceDto.FromJson(arguments)?.ToEntity();
            if (device == null) return string.Empty;
            UpdateDevice usecase = DependencyInjection.DI.Get<UpdateDevice>();
            var result = await usecase.Handle(device, cancellationToken);
            var deviceResponsed = DeviceDto.Of(result.Device);
            return deviceResponsed?.ToJson() ?? string.Empty;
        }
    }
}
