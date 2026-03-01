using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace Domain.Entities
{
    public class RegisterValueEntity : ICopyable<RegisterValueEntity>
    {
        public static readonly char[] ALLOWED_CHARS = [
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z','A', 'B', 'C', 'D',
            'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
            'O', 'P', 'Q', 'R', 'S', 'T','U', 'V', 'W', 'X',
            'Y', 'Z','@','?','%','$','&', '0', '1', '2', '3',
            '4', '5', '6', '7', '8', '9'
            ];

        public long Id { get; set; }
        public long RegisterId { get; set; }
        public string? Value { get; private set; }
        public DateTime Timestamp { get; set; }

        public RegisterValueEntity Copy()
        {
            RegisterValueEntity registerValue = new RegisterValueEntity();
            registerValue.Id = Id;
            registerValue.RegisterId = RegisterId;
            registerValue.Value = Value;
            registerValue.Timestamp = Timestamp;
            return registerValue;
        }

        public RegisterValueEntity CopyPropertiesFrom(RegisterValueEntity toCopy)
        {
            Id = toCopy.Id;
            RegisterId = toCopy.RegisterId;
            Value = toCopy.Value;
            Timestamp = toCopy.Timestamp;
            return this;
        }

        public void SetValue(string? value)
        {
            if (value == null)
            {
                Value = null;
            }
            else
            {
                if (!value.IsWhiteSpace() && StringUtils.ContainsOnly(value, ALLOWED_CHARS)) Value = value;
            }
        }     
    }
}
