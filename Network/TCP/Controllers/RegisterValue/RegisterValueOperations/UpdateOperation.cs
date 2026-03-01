using Application.UseCases.RegisterValue.Create;
using Application.UseCases.RegisterValue.Update;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.RegisterValue.RegisterValueOperations
{
    internal class UpdateOperation : IControllerOperation
    {
        //Общий вид запроса /registervalue update {JSON}
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            var registerValue = RegisterValueDto.FromJson(arguments)?.ToEntity();
            if (registerValue == null) return string.Empty;
            UpdateRegisterValue usecase = DependencyInjection.DI.Get<UpdateRegisterValue>();
            var result = await usecase.Handle(registerValue, cancellationToken);
            var registerResponsed = RegisterValueDto.Of(result.RegisterValue);
            return registerResponsed?.ToJson() ?? string.Empty;
        }
    }
}
