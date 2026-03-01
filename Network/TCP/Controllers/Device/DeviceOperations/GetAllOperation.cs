using Application.UseCases.Device.GetAll;
using Application.UseCases.Interface.GetAll;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Network.TCP.Controllers.Device.DeviceOperations
{
    internal class GetAllOperation : IControllerOperation
    {
        //Общий вид запроса /interface getall
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            GetAllDevice usecase = DependencyInjection.DI.Get<GetAllDevice>();
            var result = await usecase.Handle(cancellationToken);
            var dtos = DeviceDto.OfArray(result.Devices);
            return DeviceDto.ArrayToJson(dtos);
        }
    }
}
