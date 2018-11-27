using Newtonsoft.Json;
using System;
using System.Text;

namespace Medit.PMS.Utility
{
    public class ByteConvertUtility
    {
        public static byte[] Object2Bytes(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return  Encoding.UTF8.GetBytes(json);
        }

        public static object Bytes2Object(byte[] bytes)
        {
            string json = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject(json);
        }

        public static T Bytes2Object<T>(byte[] bytes)
        {
            string json = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
