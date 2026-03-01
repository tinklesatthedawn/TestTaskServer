using Application.UseCases.Register.GetByDeviceId;
using Application.UseCases.RegisterValue.GetByRegisterId;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Network.TCP.Controllers.RegisterValue.RegisterValueOperations
{
    internal class GetByRegisterIdOperation : IControllerOperation
    {
        //Общий вид запроса /registervalue getbyregisterid {int}
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            int deviceId;
            if (!int.TryParse(arguments, out deviceId)) return string.Empty;
            GetByRegisterIdRegisterValue usecase = DependencyInjection.DI.Get<GetByRegisterIdRegisterValue>();
            var result = await usecase.Handle(deviceId, cancellationToken);
            RegisterValueDto[] dtos = RegisterValueDto.OfArray(result.RegisterValues);
            return RegisterValueDto.ArrayToJson(dtos);
        }
    }
}
