using System;

namespace NStandard
{
    public static partial class Any
    {
        /// <summary>
        /// Create a variable by specifying a function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T Create<T>(Func<T> func) => func();

        /// <summary>
        /// Swap the values of two variables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap<T>(ref T a, ref T b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }

    }
}
