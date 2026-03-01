using Application.UseCases.Interface.Delete;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.Interface.InterfaceOperations
{
    internal class DeleteOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            var @interface = InterfaceDto.FromJson(arguments)?.ToEntity();
            if (@interface == null) return string.Empty;
            DeleteInterface usecase = DependencyInjection.DI.Get<DeleteInterface>();
            var result = await usecase.Handle(new Application.UseCases.Interface.Delete.Request(@interface), cancellationToken);
            var interfaceResponsed = InterfaceDto.Of(result.@interface);
            return interfaceResponsed?.ToJson() ?? string.Empty;
        }
    }
}
