using Application.UseCases.Device.GetById;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.Device.DeviceOperations
{
    internal class GetByIdOperation : IControllerOperation
    {
        //Общий вид запроса /interface getbyid {int}
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            int interfaceId;
            if (!int.TryParse(arguments, out interfaceId)) return string.Empty;
            GetByIdDevice usecase = DependencyInjection.DI.Get<GetByIdDevice>();
            var result = await usecase.Handle(interfaceId, cancellationToken);
            var dto = DeviceDto.Of(result.Device);
            return dto == null ? string.Empty : dto.ToJson();
        }
    }
}
