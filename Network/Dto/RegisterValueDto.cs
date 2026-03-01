using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace Network.Dto
{
    internal class RegisterValueDto : IEntityDto<RegisterValueEntity, RegisterValueDto>
    {
        public long Id { get; set; }
        public long RegisterId { get; set; }
        public string? Value { get; set; }
        public DateTime Timestamp { get; set; }

        public static RegisterValueEntity[] ArrayToEntityArray(ICollection<RegisterValueDto> dtoArray)
        {
            List<RegisterValueEntity> entities = new List<RegisterValueEntity>();

            foreach (var item in dtoArray)
            {
                entities.Add(item.ToEntity());
            }

            return entities.ToArray();
        }

        public static string ArrayToJson(ICollection<RegisterValueDto> dtoArray)
        {
            try
            {
                return JsonSerializer.Serialize(dtoArray, new JsonSerializerOptions { WriteIndented = true });
            }
            catch
            {
                return string.Empty;
            }
        }

        public static RegisterValueDto? FromJson(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<RegisterValueDto>(json);
            }
            catch
            {
                return null;
            }
        }

        public static RegisterValueDto[] JsonToArray(string jsonArray)
        {
            try
            {
                return JsonSerializer.Deserialize<RegisterValueDto[]>(jsonArray) ?? [];
            }
            catch
            {
                return [];
            }
        }

        public static RegisterValueDto? Of(RegisterValueEntity? entity)
        {
            if (entity == null) return null;
            RegisterValueDto dto = new RegisterValueDto();
            dto.Id = entity.Id;
            dto.RegisterId = entity.RegisterId;
            dto.Value = entity.Value;
            dto.Timestamp = entity.Timestamp;
            return dto;
        }

        public static RegisterValueDto[] OfArray(ICollection<RegisterValueEntity> entityArray)
        {
            List<RegisterValueDto> dtos = new List<RegisterValueDto>();

            foreach (var item in entityArray)
            {
                var dto = RegisterValueDto.Of(item);
                if (dto != null) dtos.Add(dto);
            }

            return dtos.ToArray();
        }

        public RegisterValueEntity ToEntity()
        {
            RegisterValueEntity entity = new RegisterValueEntity();
            entity.Id = Id;
            entity.RegisterId = RegisterId;
            entity.SetValue(Value);
            entity.Timestamp = Timestamp;
            return entity;
        }

        public string ToJson()
        {
            try
            {
                return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
