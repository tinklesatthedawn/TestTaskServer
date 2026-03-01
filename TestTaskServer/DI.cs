using Application.UseCases.Interface.Create;
using Application.UseCases.Interface.GetById;
using Application.UseCases.Interface.GetAll;
using Application.UseCases.Interface.Update;
using Application.UseCases.Interface.Delete;
using Application.UseCases.Interface.Filter;

using Application.UseCases.Device.Create;
using Application.UseCases.Device.GetById;
using Application.UseCases.Device.GetAll;
using Application.UseCases.Device.GetByInterfaceId;
using Application.UseCases.Device.Update;
using Application.UseCases.Device.Delete;
using Application.UseCases.Device.Filter;

using Application.UseCases.Register.Create;
using Application.UseCases.Register.GetById;
using Application.UseCases.Register.GetAll;
using Application.UseCases.Register.GetByDeviceId;
using Application.UseCases.Register.Update;
using Application.UseCases.Register.Delete;

using Application.UseCases.RegisterValue.Create;
using Application.UseCases.RegisterValue.GetById;
using Application.UseCases.RegisterValue.GetAll;
using Application.UseCases.RegisterValue.GetByRegisterId;
using Application.UseCases.RegisterValue.Update;
using Application.UseCases.RegisterValue.Delete;

using DAL.Repository;
using Domain.Repositories;
using Domain.Repository;
using DAL.Repositories;
using dependencyInjection = DependencyInjection.DI;
using Application.UseCases.LogMessage.Create;
using Application.UseCases.LogMessage.GetAll;
using Logger;

namespace TestTask.DI
{
    internal class DI
    {
        public static void Boot()
        {
            //Repositories
            dependencyInjection.AddTransient<IInterfaceRepository, InterfaceRepository>();
            dependencyInjection.AddTransient<IDeviceRepository, DeviceRepository>();
            dependencyInjection.AddTransient<IRegisterRepository, RegisterRepository>();
            dependencyInjection.AddTransient<IRegisterValueRepository, RegisterValueRepository>();
            dependencyInjection.AddTransient<ILogMessageRepository, LogMessageRepository>();

            //UseCases
            //Interface
            dependencyInjection.AddTransient<CreateInterface>();
            dependencyInjection.AddTransient<GetByIdInterface>();
            dependencyInjection.AddTransient<GetAllInterface>();
            dependencyInjection.AddTransient<UpdateInterface>();
            dependencyInjection.AddTransient<DeleteInterface>();
            dependencyInjection.AddTransient<FilterInterface>();

            //Device
            dependencyInjection.AddTransient<CreateDevice>();
            dependencyInjection.AddTransient<GetByIdDevice>();
            dependencyInjection.AddTransient<GetAllDevice>();
            dependencyInjection.AddTransient<GetByInterfaceIdDevice>();
            dependencyInjection.AddTransient<UpdateDevice>();
            dependencyInjection.AddTransient<DeleteDevice>();
            dependencyInjection.AddTransient<FilterInterface>();

            //Register
            dependencyInjection.AddTransient<CreateRegister>();
            dependencyInjection.AddTransient<GetByIdRegister>();
            dependencyInjection.AddTransient<GetAllRegister>();
            dependencyInjection.AddTransient<GetByDeviceIdRegister>();
            dependencyInjection.AddTransient<UpdateRegister>();
            dependencyInjection.AddTransient<DeleteRegister>();

            //RegisterValue
            dependencyInjection.AddTransient<CreateRegisterValue>();
            dependencyInjection.AddTransient<GetByIdRegisterValue>();
            dependencyInjection.AddTransient<GetAllRegisterValue>();
            dependencyInjection.AddTransient<GetByRegisterIdRegisterValue>();
            dependencyInjection.AddTransient<UpdateRegisterValue>();
            dependencyInjection.AddTransient<DeleteRegisterValue>();

            //LogMessage
            dependencyInjection.AddTransient<CreateLogMessage>();
            dependencyInjection.AddTransient<GetAllLogMessage>();

            //Logger
            dependencyInjection.AddTransient<ILogger, ServerLogger>();
        }
    }
}
