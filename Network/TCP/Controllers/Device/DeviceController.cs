using Network.TCP.Controllers.Device.DeviceOperations;

namespace Network.TCP.Controllers.Device
{
    internal class DeviceController : TCPController
    {
        private Dictionary<string, IControllerOperation> _routes = new Dictionary<string, IControllerOperation>();

        public DeviceController(string request) : base(request)
        {
            _routes.Add("delete", new DeleteOperation());
            _routes.Add("getbyid", new GetByIdOperation());
            _routes.Add("getall", new GetAllOperation());
            _routes.Add("create", new CreateOperation());
            _routes.Add("update", new UpdateOperation());
            _routes.Add("filter", new FilterOperation());
            _routes.Add("getbyinterfaceid", new GetByInterfaceIdOperation());
        }

        protected override Dictionary<string, IControllerOperation> GetRoutes()
        {
            return _routes;
        }
    }
}
