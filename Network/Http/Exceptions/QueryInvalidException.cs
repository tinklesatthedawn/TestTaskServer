using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Http.Exceptions
{
    public class QueryInvalidException : Exception
    {
        public QueryInvalidException(object source, string name, string? value) 
        {
            Source = source.GetType().ToString();
            ArgumentName = name;
            ArgumentValue = value;
        }

        public QueryInvalidException(object source, string name)
        {
            Source = source.GetType().ToString();
            ArgumentName = name;
        }

        public string ArgumentName { get; set; } = "ArgumentName";
        public string? ArgumentValue { get; set; }
    }
}
