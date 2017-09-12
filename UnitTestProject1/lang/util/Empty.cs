using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.lang.util
{
    internal sealed class Empty<T> : IOption<T>
    {
        internal Empty() { }
        IOption<V> IOption<T>.Cast<V>() 
        {
            return Option.Empty<V>();
        }

        IOption<T> IOption<T>.If(Predicate<T> predicate)
        {
            return this;
        }

        IOption<V> IOption<T>.Map<V>(Func<T, V> function)
        {
            return Option.Empty<V>();
        }

        IOption<V> IOption<T>.Map<V>(Func<T, V?> function)
        {
            return Option.Empty<V>();
        }

        IOption<V> IOption<T>.MapValue<V>(Func<T, V> function) 
        {
            return Option.Empty<V>();
        }

        T IOption<T>.OrElseGet(Func<T> supplier)
        {
            return supplier();
        }

        bool IOption<T>.Set(out T value)
        {
            value = default(T);
            return false;
        }

        void IOption<T>.IfPresent(Action<T> consumer)
        {
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield break;
        }
    }
}
