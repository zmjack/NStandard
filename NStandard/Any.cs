using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public static class Any
    {
        public static T Create<T>(Func<T> func) => func();
    }
}
