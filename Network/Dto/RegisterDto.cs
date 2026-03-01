using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace Network.Dto
{
    internal class RegisterDto : IEntityDto<RegisterEntity, RegisterDto>
    {
        public long Id { get; set; }
        public long DeviceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime EditingDate { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }

        public static RegisterDto? FromJson(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<RegisterDto>(json);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static RegisterDto? Of(RegisterEntity? entity)
        {
            if (entity == null) return null;
            var dto = new RegisterDto();
            dto.Id = entity.Id;
            dto.DeviceId = entity.DeviceId;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.EditingDate = entity.EditingDate;
            return dto;
        }

        public RegisterEntity ToEntity()
        {
            RegisterEntity register = new RegisterEntity();
            register.Id = Id;
            register.DeviceId = DeviceId;
            register.SetName(Name);
            register.SetDescription(Description);
            register.EditingDate = EditingDate;
            return register;
        }

        public static string ArrayToJson(ICollection<RegisterDto> dtoArray)
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

        public static RegisterDto[] JsonToArray(string jsonArray)
        {
            try
            {
                return JsonSerializer.Deserialize<RegisterDto[]>(jsonArray) ?? [];
            }
            catch (Exception)
            {
                return [];
            }
        }

        public static RegisterEntity[] ArrayToEntityArray(ICollection<RegisterDto> dtoArray)
        {
            List<RegisterEntity> registers = new List<RegisterEntity>();

            foreach (var item in dtoArray)
            {
                registers.Add(item.ToEntity());
            }

            return registers.ToArray();
        }

        public static RegisterDto[] OfArray(ICollection<RegisterEntity> entityArray)
        {
            List<RegisterDto> dtos = new List<RegisterDto>();

            foreach (var item in entityArray)
            {
                var dto = Of(item);
                if (dto != null) dtos.Add(dto);
            }

            return dtos.ToArray();
        }
    }
}
