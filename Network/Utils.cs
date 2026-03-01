using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Network
{
    public class Utils
    {
        //filters должен иметь вид property%=%value,property%=%value...
        public static Dictionary<string, string> GetProperties(string filtersString)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();
            var filters = filtersString.Split(',');
            foreach (var item in filters)
            {
                var keyValuePair = item.Split('=');
                properties.Add(keyValuePair[0], keyValuePair[1]);
            }
            return properties;
        }

        public static List<T[]> SplitArray<T>(T[] array, int elementCountToSplit)
        {
            List<T[]> bytesArrayList = new List<T[]>();
            int segmentsCount = (int)Math.Ceiling((double)array.Length / elementCountToSplit);
            int segmentCounter = 0;
            int arrayIndex = 0;
            bytesArrayList.Add(new T[elementCountToSplit]);

            for (int i = 0; i < array.Length; i++)
            {
                bytesArrayList.ElementAt(segmentCounter)[arrayIndex] = array[i];
                arrayIndex++;
                if (arrayIndex == elementCountToSplit && i < array.Length - 1)
                {
                    segmentCounter++;
                    arrayIndex = 0;
                    bytesArrayList.Add(new T[elementCountToSplit]);
                }
            }

            return bytesArrayList;
        }

        public static bool? TryParseBool(string s)
        {
            try
            {
                return Boolean.Parse(s);
            } 
            catch
            {
                return null;
            }
        }

        public static Color? TryParseColor(string s)
        {
            try
            {
                return Color.FromArgb(int.Parse(s));
            }
            catch
            {
                return null;
            }
        }

        public static int? TryParseInt(string s)
        {
            try
            {
                return int.Parse(s);
            }
            catch
            {
                return null;
            }
        }

        public static T? TryParseEnum<T>(string s) where T : struct, Enum
        {
            try
            {
                return (T)Enum.Parse(typeof(T), s);
            } 
            catch
            {
                return null;
            }
        }
    }
}
