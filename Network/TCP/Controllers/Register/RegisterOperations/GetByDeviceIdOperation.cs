using Application.UseCases.Register.GetByDeviceId;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Network.TCP.Controllers.Register.RegisterOperations
{
    //Общий вид запроса /register getbydeviceid {int}
    internal class GetByDeviceIdOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            int deviceId;
            if (!int.TryParse(arguments, out deviceId)) return string.Empty;
            GetByDeviceIdRegister usecase = DependencyInjection.DI.Get<GetByDeviceIdRegister>();
            var result = await usecase.Handle(deviceId, cancellationToken);
            RegisterDto[] dtos = RegisterDto.OfArray(result.Registers);
            return RegisterDto.ArrayToJson(dtos);
        }
    }
}
