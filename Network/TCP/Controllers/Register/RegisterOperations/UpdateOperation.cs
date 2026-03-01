using Application.UseCases.Register.Create;
using Application.UseCases.Register.Update;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.Register.RegisterOperations
{
    //Общий вид запроса /register create {JSON}
    internal class UpdateOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            var register = RegisterDto.FromJson(arguments)?.ToEntity();
            if (register == null) return string.Empty;
            UpdateRegister usecase = DependencyInjection.DI.Get<UpdateRegister>();
            var result = await usecase.Handle(register, cancellationToken);
            var registerResponsed = RegisterDto.Of(result.Register);
            return registerResponsed?.ToJson() ?? string.Empty;
        }
    }
}
