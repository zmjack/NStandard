using System;
using System.Collections.Generic;

namespace NStandard
{
    public static class EnumerableEx
    {
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] enumerables)
        {
            foreach (var enumerable in enumerables)
                foreach (var item in enumerable)
                    yield return item;
        }

        public static IEnumerable<T> Create<T>(int count, Func<int, T> generate)
        {
            for (int i = 0; i < count; i++)
                yield return generate(i);
        }

    }
}
