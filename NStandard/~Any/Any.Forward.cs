using System;
using System.Collections.Generic;

namespace NStandard
{
    public static partial class Any
    {
        /// <summary>
        /// Computes the element by path and returns the element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seed"></param>
        /// <param name="forward"></param>
        /// <returns></returns>
        public static IEnumerable<T> Forward<T>(T seed, Func<T, T> forward) where T : class
        {
            T current = seed;
            while (true)
            {
                if (current is null) break;
                yield return current;
                current = forward(current);
            }
        }
    }
}
