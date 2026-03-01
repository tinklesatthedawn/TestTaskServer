using DAL;
using Network;
using System.Text;
using System.Text.Unicode;
using TestTask.DI;
using DAL.DataGeneration;

DI.Boot();
ResetDatabase();
GenerateData();

Server.StartTcp();
Server.StartHttp();

while (true)
{
    string? input = Console.ReadLine();
    if (input == "/stop")
    {
        Server.Stop();
        break;
    }
    if (input == "/stophttp")
    {
        Server.StopHttp();
    }
    if (input == "/stoptcp")
    {
        Server.StopTcp();
    }
    if (input == "/starthttp")
    {
        Server.StartHttp();
    }
    if (input == "/starttcp")
    {
        Server.StartTcp();
    }
}

void ResetDatabase()
{
    new ApplicationDbContext().Database.EnsureDeleted();
    new ApplicationDbContext().Database.EnsureCreated();
}

void GenerateData()
{
    var dbContext = new ApplicationDbContext();
    new InterfaceDataGenerator(dbContext).Run();
    new DeviceDataGenerator(dbContext).Run();
    new RegisterDataGenerator(dbContext).Run();
    new RegisterValueDataGenerator(dbContext).Run();
}
