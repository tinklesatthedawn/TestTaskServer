using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Exceptions
{
    public class EntityFailedCreationException : Exception
    {
        public object Entity { get; set; }
        public EntityFailedCreationException(object source, object entity) 
        {
            Entity = entity;
            Source = source.GetType().ToString();
        }
    }
}
