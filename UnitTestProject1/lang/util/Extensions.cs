using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.lang.util
{
    public static class Extensions
    {
        public static IOption<T> Get<T>(this IDictionary<string, object> header, string key) where T : class
        {
            return Get(header, key).Cast<T>();
        }
        public static IOption<object> Get(this IDictionary<string,object> header, string key)
        {
            return header.TryGetValue(key, out object value)
                ? Option.Of(value)
                : Option.Empty<object>();
        }
    }
}
