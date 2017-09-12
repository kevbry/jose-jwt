using System;
using System.Collections;
using System.Collections.Generic;

namespace UnitTestProject1.lang.util
{
    public abstract class AbstractOption<T> : IOption<T>
    {
        protected abstract T Value { get; }
        IOption<V> IOption<T>.Cast<V>() 
        {
            return this
                .If(t => t is V)
                .Map(t => t as V);
        }
        public IOption<T> If(Predicate<T> predicate)
        {
            return predicate(Value)
                ? this
                : Option.Empty<T>();
        }
        IOption<V> IOption<T>.Map<V>(Func<T, V> function) 
        {
            V v = function(Value);
            return (v != default(V))
                ? Option.Of<V>(v)
                : Option.Empty<V>();
        }

        IOption<V> IOption<T>.Map<V>(Func<T, V?> function) 
        {
            V? v = function(Value);
            return v.HasValue
                ? Option.Of<V>(v)
                : Option.Empty<V>();
        }

        IOption<V> IOption<T>.MapValue<V>(Func<T, V> function) 
        {
            V v = function(Value);
            return Option.OfValue<V>(v);
        }

        T IOption<T>.OrElseGet(Func<T> supplier)
        {
            return Value;
        }

        bool IOption<T>.Set(out T value)
        {
            value = Value;
            return true;
        }

        void IOption<T>.IfPresent(Action<T> consumer)
        {
            consumer(Value);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            yield return Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Value;
        }

    }
}
