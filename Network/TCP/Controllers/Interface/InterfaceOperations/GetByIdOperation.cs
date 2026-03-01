using Application.UseCases.Interface.GetById;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.Interface.InterfaceOperations
{
    internal class GetByIdOperation : IControllerOperation
    {
        //Общий вид запроса /interface getbyid {int}
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            int interfaceId;
            if (!int.TryParse(arguments, out interfaceId)) return string.Empty;
            GetByIdInterface usecase = DependencyInjection.DI.Get<GetByIdInterface>();
            var result = await usecase.Handle(new Application.UseCases.Interface.GetById.Request(interfaceId), cancellationToken);
            var @interface = InterfaceDto.Of(result.Interface);
            return @interface == null ? string.Empty : @interface.ToJson();
        }
    }
}
