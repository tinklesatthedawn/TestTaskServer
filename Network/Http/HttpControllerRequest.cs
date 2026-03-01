using System.Collections.Specialized;
using System.Text.Json;

namespace Network.Http
{
    internal record HttpControllerRequest(string Domain, string Operation, NameValueCollection Arguments, string Body)
    {
    }
}
