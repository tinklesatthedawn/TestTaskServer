using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace Domain.Entities
{
    public class InterfaceEntity : ICopyable<InterfaceEntity>
    {
        public long Id { get; set; }
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

        public InterfaceEntity Copy()
        {
            InterfaceEntity @interface = new InterfaceEntity();
            @interface.Id = Id;
            @interface.Name = Name;
            @interface.Description = Description;
            @interface.EditingDate = EditingDate;
            return @interface;
        }

        public InterfaceEntity CopyPropertiesFrom(InterfaceEntity toCopy)
        {
            Id = toCopy.Id;
            Name = toCopy.Name;
            Description = toCopy.Description;
            EditingDate = toCopy.EditingDate;
            return this;
        }
    }
}
