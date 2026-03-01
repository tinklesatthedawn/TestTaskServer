using Network.TCP.Controllers.RegisterValue.RegisterValueOperations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP.Controllers.RegisterValue
{
    internal class RegisterValueController : TCPController
    {
        private Dictionary<string, IControllerOperation> _routes = new Dictionary<string, IControllerOperation>();

        public RegisterValueController(string request) : base(request)
        {
            _routes.Add("getbyregisterid", new GetByRegisterIdOperation());
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
