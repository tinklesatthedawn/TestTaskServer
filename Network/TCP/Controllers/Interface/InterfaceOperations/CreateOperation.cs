using Application.UseCases.Interface.Create;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.Interface.InterfaceOperations
{
    //Общий вид запроса /interface create {JSON}
    internal class CreateOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            var @interface = InterfaceDto.FromJson(arguments)?.ToEntity();
            if (@interface == null) return string.Empty;
            CreateInterface usecase = DependencyInjection.DI.Get<CreateInterface>();
            var result = await usecase.Handle(new Application.UseCases.Interface.Create.Request(@interface), cancellationToken);
            var interfaceResponsed = InterfaceDto.Of(result.@interface);
            return interfaceResponsed?.ToJson() ?? string.Empty;
        }
    }
}
