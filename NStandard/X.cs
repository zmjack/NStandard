using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public static class X
    {
        public static T Create<T>(Func<T> create)
        {
            return create();
        }

    }
}
