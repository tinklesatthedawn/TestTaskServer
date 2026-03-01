using Network.Http.Interface.Operations;
using Network.TCP;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Network.Http.Interface
{
    internal class InterfaceController : HttpController
    {
        private Dictionary<string, IHttpControllerOperation> _routes = new Dictionary<string, IHttpControllerOperation>();

        public InterfaceController(HttpControllerRequest request) : base(request)
        {
            _routes.Add("all", new GetAllOperation());
            _routes.Add("create", new CreateOperation());
            _routes.Add("delete", new DeleteOperation());
            _routes.Add("get", new GetByIdOperation());
            _routes.Add("update", new UpdateOperation());
        }

        protected override Dictionary<string, IHttpControllerOperation> GetRoutes()
        {
            return _routes;
        }
    }
}
