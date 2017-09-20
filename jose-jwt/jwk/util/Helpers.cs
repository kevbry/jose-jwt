using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jose.jwk.util
{
    internal static class Helpers
    {
        internal static bool TryGet<T>(this IDictionary<string, object> json, string key, out T value) 
        {
            if (json.TryGetValue(key, out object t))
            {
                if (t is T)
                {
                    value = (T)t;
                    return true;
                }
            }
            value = default(T);
            return false;
        }
        internal static byte[] GetBytes(this IDictionary<string, object> json, string key)
        {
            return json.TryGet<string>(key, out string value)
                ? Base64Url.Decode(Regex.Replace(value, @"\s+", "", RegexOptions.Multiline))
                : null;
        }
        internal static string GetString(this IDictionary<string, object> json, string key)
        {
            return json.TryGet<string>(key, out string value)
                ? value
                : null;
        }
        internal static void Set(this IDictionary<string, object> json, string key, string value)
        {
            json.Add(key, value);
        }
        internal static void Set(this IDictionary<string, object> json, string key, byte[] value)
        {
            json.Set(key, Base64Url.Encode(value));
        }
    }
}
