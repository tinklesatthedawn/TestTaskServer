using Application.UseCases.Register.Create;
using Application.UseCases.Register.Delete;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.Register.RegisterOperations
{
    internal class DeleteOperation : IControllerOperation
    {
        //Общий вид запроса /register delete {JSON}
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            var register = RegisterDto.FromJson(arguments)?.ToEntity();
            if (register == null) return string.Empty;
            DeleteRegister usecase = DependencyInjection.DI.Get<DeleteRegister>();
            var result = await usecase.Handle(register, cancellationToken);
            var registerResponsed = RegisterDto.Of(result.Register);
            return registerResponsed?.ToJson() ?? string.Empty;
        }
    }
}
