using Application.UseCases.Device.Filter;
using System.Drawing;
using System.Text.Json;
using FilterRequest = Application.UseCases.Device.Filter.Request;

namespace Network.TCP.Controllers.Device.DeviceOperations
{
    internal class FilterOperation : IControllerOperation
    {
        //Общий вид запроса /interface filter property%=%value,...
        public async Task<string> Execute(string arguments, CancellationToken cancellationToken)
        {
            FilterDevice usecase = DependencyInjection.DI.Get<FilterDevice>();
            var result = await usecase.Handle(ParseArgs(arguments), cancellationToken);
            string response = JsonSerializer.Serialize(result.Devices);
            return response;
        }

        private FilterRequest ParseArgs(string arguments)
        {
            string? name = null;
            string? description = null;
            bool? isEnabled = null;
            Domain.Entities.DeviceEntity.FigureType? figureType = null;
            int? size = null;
            Color? color = null;

            var properties = Utils.GetProperties(arguments);
            if (properties.ContainsKey("Name")) name = properties["Name"];
            if (properties.ContainsKey("Description")) description = properties["Description"];
            if (properties.ContainsKey("IsEnabled")) isEnabled = Utils.TryParseBool(properties["IsEnabled"]);
            if (properties.ContainsKey("FigureType")) figureType = Utils.TryParseEnum<Domain.Entities.DeviceEntity.FigureType>(properties["FigureType"]);
            if (properties.ContainsKey("Size")) size = Utils.TryParseInt(properties["Size"]);
            if (properties.ContainsKey("Color")) color = Utils.TryParseColor(properties["Color"]);

            return new FilterRequest(name, description, isEnabled, figureType, size, color);
        }
    }
}

