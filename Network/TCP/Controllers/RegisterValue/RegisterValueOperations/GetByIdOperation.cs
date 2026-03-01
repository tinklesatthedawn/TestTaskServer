using Application.UseCases.Register.GetById;
using Application.UseCases.RegisterValue.GetById;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.RegisterValue.RegisterValueOperations
{
    internal class GetByIdOperation : IControllerOperation
    {
        //Общий вид запроса /registervalue getbyid {int}
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            int id;
            if (!int.TryParse(arguments, out id)) return string.Empty;
            GetByIdRegisterValue usecase = DependencyInjection.DI.Get<GetByIdRegisterValue>();
            var result = await usecase.Handle(id, cancellationToken);
            var dto = RegisterValueDto.Of(result.RegisterValue);
            return dto?.ToJson() ?? string.Empty;
        }
    }
}
