using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public class Any
    {
        public static readonly Any _ = new();
        private Any() { }

        public static T For<T>(Func<T> func) => func();

    }
}
