using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    internal interface ICopyable<T> where T : class
    {
        T Copy();
        T CopyPropertiesFrom(T toCopy);
    }
}
