using Application.UseCases.Register.GetByDeviceId;
using Application.UseCases.Register.GetById;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Network.TCP.Controllers.Register.RegisterOperations
{
    //Общий вид запроса /register getbyid {int}
    internal class GetByIdOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            int deviceId;
            if (!int.TryParse(arguments, out deviceId)) return string.Empty;
            GetByIdRegister usecase = DependencyInjection.DI.Get<GetByIdRegister>();
            var result = await usecase.Handle(deviceId, cancellationToken);
            var registerDto = RegisterDto.Of(result.Register);
            return registerDto?.ToJson() ?? string.Empty;
        }
    }
}
