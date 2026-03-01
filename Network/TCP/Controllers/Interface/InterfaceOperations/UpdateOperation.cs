using Application.UseCases.Device.Create;
using Application.UseCases.Device.Delete;
using Application.UseCases.Interface.Update;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.Interface.InterfaceOperations
{
    internal class UpdateOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            var device = InterfaceDto.FromJson(arguments)?.ToEntity();
            if (device == null) return string.Empty;
            UpdateInterface usecase = DependencyInjection.DI.Get<UpdateInterface>();
            var result = await usecase.Handle(device, cancellationToken);
            var interfaceResponsed = InterfaceDto.Of(result.@interface);
            return interfaceResponsed?.ToJson() ?? string.Empty;
        }
    }
}
