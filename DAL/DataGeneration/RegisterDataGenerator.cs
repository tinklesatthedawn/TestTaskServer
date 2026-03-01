using Domain.Entities;

namespace DAL.DataGeneration
{
    public class RegisterDataGenerator
    {
        public ApplicationDbContext Context { get; set; }

        public RegisterDataGenerator(ApplicationDbContext context)
        {
            Context = context;
        }

        public void Run()
        {
            var devicesList = Context.Devices.ToList();
            foreach (var device in devicesList)
            {
                for (int i = 0; i < 10; i++)
                {
                    Random random = new Random();

                    var register = new RegisterEntity();
                    register.DeviceId = device.Id;
                    register.SetName(_generateName());
                    register.SetDescription(_generateDescription());
                    register.EditingDate = DateTime.Now;
                    Context.Registers.Add(register);
                }

            }
            Context.SaveChanges();
        }

        private string _generateName()
        {
            string name = "Register%";
            var random = new Random();
            int length = random.Next(3, 10);

            for (int i = 0; i < length; i++)
            {
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
