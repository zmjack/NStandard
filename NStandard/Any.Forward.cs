using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NStandard
{
    public static partial class Any
    {
        /// <summary>
        /// Calculate the element by path and return the element.
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
