using System.Net;

namespace Network.Http
{
    internal class HttpControllerResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public string Body { get; set; } = string.Empty;

        public HttpControllerResponse(HttpStatusCode statusCode, Dictionary<string, string> headers, string body)
        {
            StatusCode = statusCode;
            Headers = headers;
            Body = body;
        }

        public HttpControllerResponse(HttpStatusCode statusCode, string body)
        {
            StatusCode = statusCode;
            Body = body;
        }

        public HttpControllerResponse(HttpStatusCode statusCode, Dictionary<string, string> headers)
        {
            StatusCode = statusCode;
            Headers = headers;
        }

        public void AddHeader(string key, string value)
        {
            Headers.Add(key, value);
        }

        public static HttpControllerResponse Error_404(string message)
        {
            return new HttpControllerResponse(HttpStatusCode.NotFound, message);
        }

        public static HttpControllerResponse Error_404()
        {
            return new HttpControllerResponse(HttpStatusCode.NotFound, "Route not found 404");
        }
    }
}
