using Application.UseCases.Device.GetById;
using Application.UseCases.Device.GetByInterfaceId;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Network.TCP.Controllers.Device.DeviceOperations
{
    internal class GetByInterfaceIdOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            int interfaceId;
            if (!int.TryParse(arguments, out interfaceId)) return string.Empty;
            GetByInterfaceIdDevice usecase = DependencyInjection.DI.Get<GetByInterfaceIdDevice>();
            var result = await usecase.Handle(interfaceId, cancellationToken);
            var dtos = DeviceDto.OfArray(result.Devices);
            return DeviceDto.ArrayToJson(dtos);
        }
    }
}
