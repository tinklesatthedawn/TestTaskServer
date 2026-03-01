using Application.UseCases.Interface.Filter;
using System.Text.Json;
using FilterRequest = Application.UseCases.Interface.Filter.Request;

namespace Network.TCP.Controllers.Interface.InterfaceOperations
{
    internal class FilterOperation : IControllerOperation
    {
        //Общий вид запроса /interface filter property%=%value,...
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            FilterInterface usecase = DependencyInjection.DI.Get<FilterInterface>();
            var result = await usecase.Handle(ParseArgs(arguments), cancellationToken);
            string response = JsonSerializer.Serialize(result.Interfaces);
            return response;
        }

        private FilterRequest ParseArgs(string arguments)
        {
            string? name = null;
            string? description = null;

            var properties = Utils.GetProperties(arguments);
            if (properties.ContainsKey("Name")) name = properties["Name"];
            if (properties.ContainsKey("Description")) description = properties["Description"];

            return new FilterRequest(name, description);
        }
    }
}

