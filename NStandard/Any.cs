using System;
using System.Linq;

namespace NStandard
{
    public static class Any
    {
        public static T Create<T>(Func<T> func) => func();

        public static void Swap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }
    }
}
