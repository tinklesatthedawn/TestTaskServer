using Application.UseCases.Register.Create;
using Application.UseCases.Register.GetAll;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Network.TCP.Controllers.Register.RegisterOperations
{
    internal class GetAllOperation : IControllerOperation
    {
        //Общий вид запроса /register getall
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            GetAllRegister usecase = DependencyInjection.DI.Get<GetAllRegister>();
            var result = await usecase.Handle(cancellationToken);
            return JsonSerializer.Serialize(result.Registers.ToArray());
        }
    }
}
