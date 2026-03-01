using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace DAL.DataGeneration
{
    public class InterfaceDataGenerator
    {
        public ApplicationDbContext Context { get; set; }
        public InterfaceDataGenerator(ApplicationDbContext context) {
            Context = context;
        }

        public void Run()
        {
            for (int i = 0; i < 10; i++) {
                var @interface = new InterfaceEntity();
                @interface.SetName(_generateName());
                @interface.SetDescription(_generateDescription());
                @interface.EditingDate = DateTime.Now;
                Context.Interfaces.Add(@interface);
            }
            Context.SaveChanges();
        }

        private string _generateName()
        {
            string name = "Interface%";
            var random = new Random();
            int length = random.Next(3, 10);

            for (int i = 0; i < length ; i++) {
                int randValue = random.Next(65, 91);
                name += Convert.ToChar(randValue);
            }

            return name;
        }

        private string _generateDescription()
        {
            string name = string.Empty;
            var random = new Random();
            int wordsCount = random.Next(5, 10);
            
            for (int i = 0; i < wordsCount; i ++) 
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
