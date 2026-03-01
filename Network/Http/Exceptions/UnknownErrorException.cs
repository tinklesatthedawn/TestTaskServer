using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Exceptions
{
    public class UnknownErrorException : Exception
    {
        public UnknownErrorException(object source) 
        {
            Source = source.GetType().ToString();
        }
    }
}
