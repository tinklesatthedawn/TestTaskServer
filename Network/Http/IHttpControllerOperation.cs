using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http
{
    internal interface IHttpControllerOperation
    {
        Task<HttpControllerResponse> Execute(HttpControllerRequest request, CancellationToken cancellationToken);
    }
}
