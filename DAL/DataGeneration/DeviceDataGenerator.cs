using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using Domain.Entities;

namespace DAL.DataGeneration
{
    public class DeviceDataGenerator
    {
        public ApplicationDbContext Context { get; set; }

        public DeviceDataGenerator(ApplicationDbContext context) {
            Context = context;
        }

        public void Run()
        {
            var interfacesList = Context.Interfaces.ToList();
            foreach (var @interface in interfacesList)
            {
                for (int i = 0; i < 30; i++)
                {
                    Random random = new Random();

                    var device = new DeviceEntity();
                    device.InterfaceId = @interface.Id;
                    device.SetName(GenerateName());
                    device.SetDescription(GenerateDescription());
                    device.IsEnabled = (random.Next(2) == 0);
                    device.EditingDate = DateTime.Now;
                    device.Figure = (DeviceEntity.FigureType)random.Next(3);
                    device.Size = random.Next(4, 10);
                    device.PosX = random.Next(0, 10);
                    device.PosY = random.Next(0, 10);
                    device.Color = GenerateColor();

                    Context.Devices.Add(device);
                }

            }
            Context.SaveChanges();
        }

        private static Color GenerateColor()
        {
            Random random = new Random();
            int r = random.Next(256);
            int g = random.Next(256);
            int b = random.Next(256);
            int a = 255;
            return Color.FromArgb(a, r, g, b);
        }

        private static string GenerateName()
        {
            string name = "Device%";
            var random = new Random();
            int length = random.Next(3, 10);

            for (int i = 0; i < length; i++)
            {
                int randValue = random.Next(65, 91);
                name += Convert.ToChar(randValue);
            }

            return name;
        }

        private static string GenerateDescription()
        {
            string name = string.Empty;
            var random = new Random();
            int wordsCount = random.Next(5, 10);

            for (int i = 0; i < wordsCount; i++)
            {
                int wordLength = random.Next(3, 10);
                for (int k = 0; k < wordLength; k++)
                {
                    int randValue = random.Next(65, 91);
                    name += Convert.ToChar(randValue);
                }
                name += " ";
            }

            return name.TrimEnd();
        }

    }
}
