﻿namespace LinqSharp;

public static partial class IEnumerableExtensions
{
    public static IEnumerable<TSource> WhereMin<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    {
        if (source.Any())
        {
            var min = source.Min(selector);
            return source.Where(x => selector(x).Equals(min));
        }
        else return source;
    }

}
