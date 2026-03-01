using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Text.Json;

namespace Network.Dto
{
    internal class DeviceDto : IEntityDto<DeviceEntity, DeviceDto>
    {
        public enum FigureType
        {
            Circle,
            Square,
            Line
        }

        public long Id { get; set; }
        public long InterfaceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime EditingDate { get; set; }
        public FigureType Figure { get; set; }
        public int Size { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int ColorArgb { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        public static DeviceDto? FromJson(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<DeviceDto>(json);
            }
            catch
            {
                return null;
            }

        }

        public static DeviceDto? Of(DeviceEntity? entity)
        {
            if (entity == null) return null;
            var dto = new DeviceDto();
            dto.Id = entity.Id;
            dto.InterfaceId = entity.InterfaceId;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.IsEnabled = entity.IsEnabled;
            dto.EditingDate = entity.EditingDate;
            dto.Figure = GetFromDevice(entity);
            dto.Size = entity.Size;
            dto.PosX = entity.PosX;
            dto.PosY = entity.PosY;
            dto.ColorArgb = entity.Color.ToArgb();
            return dto;
        }

        public DeviceEntity ToEntity()
        {
            DeviceEntity device = new DeviceEntity();
            device.Id = Id;
            device.InterfaceId = InterfaceId;
            device.SetName(Name);
            device.SetDescription(Description);
            device.IsEnabled = IsEnabled;
            device.EditingDate = EditingDate;
            device.Figure = GetFromDto(this);
            device.Size = Size;
            device.PosX = PosX;
            device.PosY = PosY;
            device.Color = Color.FromArgb(ColorArgb);
            return device;
        }

        public static string ArrayToJson(ICollection<DeviceDto> dtoArray)
        {
            try
            {
                return JsonSerializer.Serialize(dtoArray, new JsonSerializerOptions { WriteIndented = true});
            }
            catch
            {
                return string.Empty;
            }
        }

        public static DeviceDto[] JsonToArray(string jsonArray)
        {
            try
            {
                return JsonSerializer.Deserialize<DeviceDto[]>(jsonArray) ?? [];
            }
            catch
            {
                return [];
            }
        }

        public static DeviceEntity[] ArrayToEntityArray(ICollection<DeviceDto> dtoArray)
        {
            List<DeviceEntity> entities = new List<DeviceEntity>();

            foreach (var item in dtoArray)
            {
                entities.Add(item.ToEntity());
            }

            return entities.ToArray();
        }

        public static DeviceDto[] OfArray(ICollection<DeviceEntity> entityArray)
        {
            List<DeviceDto> dtos = new List<DeviceDto>();

            foreach (var item in entityArray)
            {
                var dto = DeviceDto.Of(item);
                if (dto != null) dtos.Add(dto);
            }

            return dtos.ToArray();
        }

        public override string ToString()
        {
            return $"Name: {this.Name}, Description: {this.Description}, Editing date: {this.EditingDate}";
        }

        private static DeviceEntity.FigureType GetFromDto(DeviceDto device)
        {
            DeviceEntity.FigureType figureType;
            if (Enum.TryParse<DeviceEntity.FigureType>(device.Figure.ToString(), true, out figureType))
            {
                return figureType;
            }
            return DeviceEntity.FigureType.Circle;
        }

        private static FigureType GetFromDevice(DeviceEntity device)
        {
            FigureType figureType;
            if (Enum.TryParse<FigureType>(device.Figure.ToString(), true, out figureType))
            {
                return figureType;
            }
            return FigureType.Circle;
        }
    }
}
