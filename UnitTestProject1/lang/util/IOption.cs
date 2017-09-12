using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.lang.util
{
    public interface IOption<T> : IEnumerable<T>
    {
        IOption<V> Map<V>(Func<T, V> function) where V : class;
        IOption<V> Map<V>(Func<T, V?> function) where V : struct;
        IOption<V> MapValue<V>(Func<T, V> function) where V : struct;
        IOption<T> If(Predicate<T> predicate);
        IOption<V> Cast<V>() where V : class;
        T OrElseGet(Func<T> supplier);
        bool Set(out T value);
        void IfPresent(Action<T> consumer);
    }
}
