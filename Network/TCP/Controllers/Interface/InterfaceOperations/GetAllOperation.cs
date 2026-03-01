using Application.UseCases.Interface.GetAll;
using Network.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Network.TCP.Controllers.Interface.InterfaceOperations
{
    internal class GetAllOperation : IControllerOperation
    {
        //Общий вид запроса /interface getall
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            GetAllInterface usecase = DependencyInjection.DI.Get<GetAllInterface>();
            var result = await usecase.Handle(cancellationToken);
            var dtos = InterfaceDto.OfArray(result.Interfaces);
            return InterfaceDto.ArrayToJson(dtos);
        }
    }
}
