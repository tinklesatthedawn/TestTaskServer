using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace Network.Dto
{
    internal class InterfaceDto : IEntityDto<InterfaceEntity, InterfaceDto>
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime EditingDate { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        public static InterfaceDto? FromJson(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<InterfaceDto>(json);
            }
            catch
            {
                return null;
            }       
        }

        public InterfaceEntity ToEntity()
        {
            InterfaceEntity @interface = new InterfaceEntity();
            @interface.Id = Id;
            @interface.SetName(Name);
            @interface.SetDescription(Description);
            @interface.EditingDate = EditingDate;
            return @interface;
        }

        public static InterfaceDto? Of(InterfaceEntity? entity)
        {
            if (entity == null) return null;
            var dto = new InterfaceDto();
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.EditingDate = entity.EditingDate;
            return dto;
        }

        public static string ArrayToJson(ICollection<InterfaceDto> dtoArray)
        {
            try
            {
                return JsonSerializer.Serialize(dtoArray, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static InterfaceDto[] JsonToArray(string jsonArray)
        {
            try
            {
                return JsonSerializer.Deserialize<InterfaceDto[]>(jsonArray) ?? [];
            }
            catch (Exception)
            {
                return [];
            }
        }

        public static InterfaceEntity[] ArrayToEntityArray(ICollection<InterfaceDto> dtoArray)
        {
            List<InterfaceEntity> entities = new List<InterfaceEntity>();

            foreach (var item in dtoArray)
            {
                entities.Add(item.ToEntity());
            }

            return entities.ToArray();
        }

        public static InterfaceDto[] OfArray(ICollection<InterfaceEntity> entityArray)
        {
            List<InterfaceDto> dtos = new List<InterfaceDto>();

            foreach (var item in entityArray)
            {
                var dto = InterfaceDto.Of(item);
                if (dto != null) dtos.Add(dto);
            }

            return dtos.ToArray();
        }

        public override string ToString()
        {
            return $"Name: {this.Name}, Description: {this.Description}, Editing date: {this.EditingDate}";
        }
    }
}
