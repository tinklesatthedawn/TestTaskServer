using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Network.Dto
{
    internal interface IEntityDto<T,K> where T : class where K : IEntityDto<T,K>
    {
        public string ToJson();

        public static abstract K? FromJson(string json);

        public T ToEntity();

        public static abstract K? Of(T? entity);

        public static abstract string ArrayToJson(ICollection<K> dtoArray);

        public static abstract K[] JsonToArray(string jsonArray);

        public static abstract T[] ArrayToEntityArray(ICollection<K> dtoArray);

        public static abstract K[] OfArray(ICollection<T> entityArray);
    }
}
