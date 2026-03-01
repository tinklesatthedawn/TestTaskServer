using Application.UseCases.Register.Create;
using Application.UseCases.RegisterValue.Create;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.RegisterValue.RegisterValueOperations
{
    //Общий вид запроса /registervalue create {JSON}
    internal class CreateOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            var registerValue = RegisterValueDto.FromJson(arguments)?.ToEntity();
            if (registerValue == null) return string.Empty;
            CreateRegisterValue usecase = DependencyInjection.DI.Get<CreateRegisterValue>();
            var result = await usecase.Handle(registerValue, cancellationToken);
            var registerResponsed = RegisterValueDto.Of(result.RegisterValue);
            return registerResponsed?.ToJson() ?? string.Empty;
        }
    }
}
