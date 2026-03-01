using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Utils;

namespace Domain.Entities
{
    public class RegisterEntity : ICopyable<RegisterEntity>
    {
        public long Id { get; set; }
        public long DeviceId { get; set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public DateTime EditingDate { get; set; }

        public void SetName(string? name)
        {
            if (name == null)
            {
                Name = null;
            }
            else
            {
                if (StringUtils.StartsWithOneOfChars(name, StringUtils.ALPHABET_CHARS) && !name.IsWhiteSpace()) Name = name;
            }
        }

        public void SetDescription(string? description)
        {
            if (!description.IsWhiteSpace()) Description = description;
        }

        public RegisterEntity Copy()
        {
            RegisterEntity register = new RegisterEntity();
            register.Id = Id;
            register.DeviceId = DeviceId;
            register.Name = Name;
            register.Description = Description;
            register.EditingDate = EditingDate;
            return register;
        }

        public RegisterEntity CopyPropertiesFrom(RegisterEntity toCopy)
        {
            Id = toCopy.Id;
            DeviceId = toCopy.DeviceId;
            Name = toCopy.Name;
            Description = toCopy.Description;
            EditingDate = toCopy.EditingDate;
            return this;
        }
    }
}
