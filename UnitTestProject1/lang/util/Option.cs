using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jose.lang.util
{
    public static class Option
    {
        public static IOption<T> Empty<T>()
        {
            return new Empty<T>();
        }
        public static IOption<T> Of<T>(T v) where T : class
        {
            return (v != default(T))
                ? new ObjectOption<T>(v)
                : Empty<T>();
        }
        public static IOption<T> Of<T>(T? v) where T : struct
        {
            return (v.HasValue)
                ? new ValueOption<T>(v)
                : Empty<T>();
        }
        public static IOption<T> OfValue<T>(T v) where T : struct
        {
            return new ValueOption<T>(v);
        }
    }
}
