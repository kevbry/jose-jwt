using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jose.lang.util
{
    internal sealed class ObjectOption<T> : AbstractOption<T> where T : class
    {
        private readonly T value;

        internal ObjectOption(T v)
        {
            value = v ?? throw new ArgumentNullException();
        }

        protected override T Value => value;
    }
}
