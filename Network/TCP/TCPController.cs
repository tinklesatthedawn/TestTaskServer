using Network.TCP.Controllers.Device;
using Network.TCP.Controllers.Interface;
using Network.TCP.Controllers.Register;
using Network.TCP.Controllers.RegisterValue;

namespace Network.TCP
{
    public abstract class TCPController
    {
        private delegate TCPController GetController(string request);
        protected string _request;

        private static Dictionary<string, GetController> routes = new Dictionary<string, GetController>()
        {
            {"interface", request => new InterfaceController(request)},
            {"device", request => new DeviceController(request)},
            {"register", request => new RegisterController(request)},
            {"registervalue", request => new RegisterValueController(request)}
        };

        public TCPController(string request)
        {
            _request = request;
        }

        public async Task<string> Handle(CancellationToken cancellationToken)
        {
            string operation;
            string arguments;
            ParseRequest(out operation, out arguments);

            foreach (var item in GetRoutes())
            {
                if (item.Key == operation)
                {
                    return await item.Value.Execute(arguments, cancellationToken);
                }
            }

            Console.WriteLine($"Operation with name { operation } doesn't exist");
            return string.Empty;
        }

        public static TCPController Get(string request)
        {
            try
            {
                var split = request.Split(' ', 2);
                string route = split[0];
                string arguments = split[1];

                foreach (var item in routes)
                {
                    if (item.Key == route)
                    {
                        return item.Value(arguments);
                    }
                }

                throw new InvalidDataException("No such route exists");
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidDataException($"The request has an incorrect format: {request}");
            }
        }

        protected abstract Dictionary<string, IControllerOperation> GetRoutes();

        protected void ParseRequest(out string operation, out string arguments)
        {
            var split = _request.Split(" ", 2);
            operation = split[0];

            if (split.Length > 1)
            {
                arguments = split[1];
            }
            else
            {
                arguments = string.Empty;
            }
        }

    }
}
