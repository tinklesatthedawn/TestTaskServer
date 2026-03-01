using Network.Http.RegisterValue.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.RegisterValue
{
    internal class RegisterValueController : HttpController
    {
        private Dictionary<string, IHttpControllerOperation> _routes = new Dictionary<string, IHttpControllerOperation>();

        public RegisterValueController(HttpControllerRequest request) : base(request)
        {
            _routes.Add("all", new GetAllOperation());
            _routes.Add("create", new CreateOperation());
            _routes.Add("delete", new DeleteOperation());
            _routes.Add("get", new GetByIdOperation());
            _routes.Add("byregister", new GetByRegisterIdOperation());
        }

        protected override Dictionary<string, IHttpControllerOperation> GetRoutes()
        {
            return _routes;
        }
    }
}
