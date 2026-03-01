using System;
using System.Collections.Generic;
using System.Text;

namespace Network.TCP
{
    public interface IControllerOperation
    {
        Task<string> Execute(string arguments, CancellationToken cancellationToken);
    }
}
