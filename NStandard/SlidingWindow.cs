using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public class SlidingWindow<T>
    {
        public T[] Values { get; set; }

        public SlidingWindow(T[] values)
        {
            Values = values;
        }

    }
}
