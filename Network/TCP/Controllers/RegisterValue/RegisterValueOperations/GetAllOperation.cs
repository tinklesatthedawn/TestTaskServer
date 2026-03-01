using Application.UseCases.Register.GetAll;
using Application.UseCases.RegisterValue.GetAll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Network.TCP.Controllers.RegisterValue.RegisterValueOperations
{
    internal class GetAllOperation : IControllerOperation
    {
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            GetAllRegisterValue usecase = DependencyInjection.DI.Get<GetAllRegisterValue>();
            var result = await usecase.Handle(cancellationToken);
            return JsonSerializer.Serialize(result.RegisterValues.ToArray());
        }
    }
}
