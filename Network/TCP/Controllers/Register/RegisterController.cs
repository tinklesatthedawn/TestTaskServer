using System;
using System.Collections.Generic;
using System.Text;
using Network.TCP.Controllers.Register.RegisterOperations;

namespace Network.TCP.Controllers.Register
{
    internal class RegisterController : TCPController
    {
        private Dictionary<string, IControllerOperation> _routes = new Dictionary<string, IControllerOperation>();

        public RegisterController(string request) : base(request)
        {
            _routes.Add("getbydeviceid", new GetByDeviceIdOperation());
            _routes.Add("getbyid", new GetByIdOperation());
            _routes.Add("getall", new GetAllOperation());
            _routes.Add("create", new CreateOperation());
            _routes.Add("delete", new DeleteOperation());
            _routes.Add("update", new UpdateOperation());
        }

        protected override Dictionary<string, IControllerOperation> GetRoutes()
        {
            return _routes;
        }
    }
}
