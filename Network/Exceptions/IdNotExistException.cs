using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Exceptions
{
    internal class IdNotExistException : Exception
    {
        public long Id { get; set; }

        public IdNotExistException(object source, long id) 
        {
            Source = source.GetType().ToString(); 
            Id = id;
        }
    }
}
