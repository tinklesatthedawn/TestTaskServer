using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Utils;

namespace Domain.Entities
{
    public class DeviceEntity : ICopyable<DeviceEntity>
    {
        public enum FigureType
        {
            Circle,
            Square,
            Line
        }
        public long Id { get; set; }
        public long InterfaceId {  get; set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public bool IsEnabled {  get; set; }
        public DateTime EditingDate { get; set; }
        public FigureType Figure { get; set; }
        public int Size { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Color Color { get; set; }

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

        public DeviceEntity Copy()
        {
            DeviceEntity _device = new DeviceEntity();
            _device.Id = Id;
            _device.InterfaceId = InterfaceId;
            _device.Name = Name;
            _device.Description = Description;
            _device.IsEnabled = IsEnabled;
            _device.EditingDate = EditingDate;
            _device.Figure = Figure;
            _device.Size = Size;
            _device.PosX = PosX;
            _device.PosY = PosY;
            _device.Color = Color;
            return _device;
        }

        public DeviceEntity CopyPropertiesFrom(DeviceEntity toCopy)
        {
            Id = toCopy.Id;
            InterfaceId = toCopy.InterfaceId;
            Name = toCopy.Name;
            Description = toCopy.Description;
            IsEnabled = toCopy.IsEnabled;
            EditingDate = toCopy.EditingDate;
            Figure = toCopy.Figure;
            Size = toCopy.Size;
            PosX = toCopy.PosX;
            PosY = toCopy.PosY;
            Color = toCopy.Color;
            return this;
        }
    }
}
