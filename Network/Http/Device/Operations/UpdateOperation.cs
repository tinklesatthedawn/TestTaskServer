using Application.UseCases.Device.Create;
using Application.UseCases.Device.Update;
using Domain.Entities;
using Network.Dto;
using Network.Exceptions;
using Network.Http.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Network.Http.Device.Operations
{
    internal class UpdateOperation : IHttpControllerOperation
    {
        public async Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken)
        {
            var device = GetFromRequest(request);
            if (device != null)
            {
                UpdateDevice usecase = DependencyInjection.DI.Get<UpdateDevice>();
                var result = await usecase.Handle(device, cancellationToken);
                var deviceResponsed = DeviceDto.Of(result.Device);
                if (deviceResponsed != null)
                {
                    string json = deviceResponsed.ToJson();
                    return new HttpControllerResponse(System.Net.HttpStatusCode.OK, json);
                }
                throw new EntityNotExistException(this, device);
            }
            throw new UnknownErrorException(this);
        }

        private DeviceEntity? GetFromRequest(HttpControllerRequest request)
        {
            var device = DeviceDto.FromJson(request.Body)?.ToEntity();
            if (device == null)
            {
                var arguments = request.Arguments;
                int id;
                int interfaceId;
                string? name;
                string? description;
                bool isEnabled;
                DeviceEntity.FigureType figureType;
                int size;
                int posX;
                int posY;
                int colorArgb;

                name = arguments.Get("Name") ?? throw new QueryInvalidException(this, "Name");
                description = arguments.Get("Description") ?? throw new QueryInvalidException(this, "Description");
                if (!int.TryParse(arguments.Get("Id"), out id)) throw new QueryInvalidException(this, "Id", arguments.Get("Id"));
                if (!int.TryParse(arguments.Get("InterfaceId"), out interfaceId)) throw new QueryInvalidException(this, "InterfaceId", arguments.Get("InterfaceId"));
                if (!bool.TryParse(arguments.Get("IsEnabled"), out isEnabled)) throw new QueryInvalidException(this, "IsEnabled", arguments.Get("IsEnabled"));
                if (!TryParseFigure(arguments.Get("Figure"), out figureType)) throw new QueryInvalidException(this, "Figure", arguments.Get("Figure"));
                if (!int.TryParse(arguments.Get("Size"), out size)) throw new QueryInvalidException(this, "Size", arguments.Get("Size"));
                if (!int.TryParse(arguments.Get("PosX"), out posX)) throw new QueryInvalidException(this, "PosX", arguments.Get("PosX"));
                if (!int.TryParse(arguments.Get("PosY"), out posY)) throw new QueryInvalidException(this, "PosY", arguments.Get("PosY"));
                if (!int.TryParse(arguments.Get("Color"), out colorArgb)) throw new QueryInvalidException(this, "Color", arguments.Get("Color"));

                DeviceEntity entity = new DeviceEntity();
                entity.InterfaceId = interfaceId;
                entity.SetName(name);
                entity.SetDescription(description);
                entity.IsEnabled = isEnabled;
                entity.Figure = figureType;
                entity.Size = size;
                entity.PosX = posX;
                entity.PosY = posY;
                entity.Color = Color.FromArgb(colorArgb);

                return entity;
            }
            return device;
        }

        private bool TryParseFigure(string? value, out DeviceEntity.FigureType figureType)
        {
            if (Enum.TryParse(value, out figureType)) return true;
            if (value == "Circle")
            {
                figureType = DeviceEntity.FigureType.Circle;
                return true;
            }
            if (value == "Square")
            {
                figureType = DeviceEntity.FigureType.Square;
                return true;
            }
            if (value == "Line")
            {
                figureType = DeviceEntity.FigureType.Line;
                return true;
            }
            return false;
        }
    }
}
