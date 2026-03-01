using Network.Http.Device.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Device
{
    internal class DeviceController : HttpController
    {
        private Dictionary<string, IHttpControllerOperation> _routes = new Dictionary<string, IHttpControllerOperation>();

        public DeviceController(HttpControllerRequest request) : base(request)
        {
            _routes.Add("all", new GetAllOperation());
            _routes.Add("create", new CreateOperation());
            _routes.Add("delete", new DeleteOperation());
            _routes.Add("get", new GetByIdOperation());
            _routes.Add("update", new UpdateOperation());
            _routes.Add("byinterface", new GetByInterfaceIdOperation());
        }

        protected override Dictionary<string, IHttpControllerOperation> GetRoutes()
        {
            return _routes;
        }
    }
}
