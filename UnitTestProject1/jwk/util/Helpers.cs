using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnitTestProject1.jwk.util
{
    public static class Helpers
    {
        public static bool TryGet<T>(this IDictionary<string, object> json, string key, out T value) 
        {
            object t;
            if(json.TryGetValue(key, out t))
            {
                if(t is T)
                {
                    value = (T)t;
                    return true;
                }
            }
            value = default(T);
            return false;
        }
        public static byte[] GetBytes(this IDictionary<string, object> json, string key)
        {
            return json.TryGet<string>(key, out string value)
                ? Base64Url.Decode(Regex.Replace(value, @"\s+", "", RegexOptions.Multiline))
                : null;
        }
        public static string GetString(this IDictionary<string, object> json, string key)
        {
            return json.TryGet<string>(key, out string value)
                ? value
                : null;
        }
        public static void Set(this IDictionary<string, object> json, string key, string value)
        {
            json.Add(key, value);
        }
        public static void Set(this IDictionary<string, object> json, string key, byte[] value)
        {
            json.Set(key, Base64Url.Encode(value));
        }
    }
}
