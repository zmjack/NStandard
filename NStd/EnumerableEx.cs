using System.Collections.Generic;

namespace NStd
{
    public static class EnumerableEx
    {
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] enumerables)
        {
            foreach (var enumerable in enumerables)
                foreach (var item in enumerable)
                    yield return item;
        }
    }
}
