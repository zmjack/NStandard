using System;

namespace NStandard
{
    public static class Any
    {
        public static T Create<T>(Func<T> func) => func();
    }
}
