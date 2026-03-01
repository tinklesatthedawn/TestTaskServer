using Application.UseCases.RegisterValue.Create;
using Application.UseCases.RegisterValue.Delete;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.RegisterValue.RegisterValueOperations
{
    internal class DeleteOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            var registerValue = RegisterValueDto.FromJson(arguments)?.ToEntity();
            if (registerValue == null) return string.Empty;
            DeleteRegisterValue usecase = DependencyInjection.DI.Get<DeleteRegisterValue>();
            var result = await usecase.Handle(registerValue, cancellationToken);
            var registerResponsed = RegisterValueDto.Of(result.RegisterValue);
            return registerResponsed?.ToJson() ?? string.Empty;
        }
    }
}
