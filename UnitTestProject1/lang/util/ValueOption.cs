using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.lang.util
{
    internal sealed class ValueOption<T> : AbstractOption<T> where T : struct
    {
        private readonly T value;

        internal ValueOption(T? v)
        {
            value = v ?? throw new ArgumentNullException();
        }

        protected override T Value => value;

    }
}
