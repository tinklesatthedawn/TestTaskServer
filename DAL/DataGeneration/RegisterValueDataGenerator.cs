using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace DAL.DataGeneration
{
    public class RegisterValueDataGenerator
    {
        public ApplicationDbContext Context { get; set; }

        public RegisterValueDataGenerator(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Run()
        {
            var registersList = Context.Registers.ToList();
            foreach (var register in registersList)
            {
                for (int i = 0; i < 10; i++)
                {
                    Random random = new Random();

                    var registerValue = new RegisterValueEntity();
                    registerValue.RegisterId = register.Id;
                    registerValue.SetValue(_generateValue());
                    registerValue.Timestamp = DateTime.Now;
                    Context.RegisterValues.Add(registerValue);
                }

            }
            Context.SaveChanges();
        }
        private string _generateValue()
        {
            string name = string.Empty;
            var random = new Random();
            int length = random.Next(5, 15);

            for (int i = 0; i < length; i++)
            {
                name += StringUtils.GetRandomChar(RegisterValueEntity.ALLOWED_CHARS);
            }

            return name.TrimEnd();
        }

        
    }
}
