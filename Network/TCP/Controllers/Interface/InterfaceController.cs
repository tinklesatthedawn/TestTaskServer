using Network.TCP.Controllers.Interface.InterfaceOperations;

namespace Network.TCP.Controllers.Interface
{
    public class InterfaceController : TCPController
    {
        private Dictionary<string, IControllerOperation> _routes = new Dictionary<string, IControllerOperation>();

        public InterfaceController(string request) : base(request)
        {
            _routes.Add("getbyid", new GetByIdOperation());
            _routes.Add("getall", new GetAllOperation());
            _routes.Add("create", new CreateOperation());
            _routes.Add("filter", new FilterOperation());
            _routes.Add("delete", new DeleteOperation());
            _routes.Add("update", new UpdateOperation());
        }

        protected override Dictionary<string, IControllerOperation> GetRoutes()
        {
            return _routes;
        }
    }
}
