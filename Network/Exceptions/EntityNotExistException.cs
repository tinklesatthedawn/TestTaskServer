using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Exceptions
{
    internal class EntityNotExistException : Exception
    {
        public object Entity { get; set; }

        public EntityNotExistException(object source, object entity) 
        { 
            Entity = entity;
            Source = source.GetType().ToString();
        }
    }
}
