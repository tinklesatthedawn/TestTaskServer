using Application.UseCases.Device.Create;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.Device.DeviceOperations
{
    //Общий вид запроса /interface create {JSON}
    internal class CreateOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            var device = DeviceDto.FromJson(arguments)?.ToEntity();
            if (device == null) return string.Empty;
            CreateDevice usecase = DependencyInjection.DI.Get<CreateDevice>();
            var result = await usecase.Handle(device, cancellationToken);
            var dto = DeviceDto.Of(result.Device);
            return dto?.ToJson() ?? string.Empty;
        }
    }
}
