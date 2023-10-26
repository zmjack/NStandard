using NStandard.Collections;
using NStandard.ValueTuples;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class IEnumerableExtensions
{
    /// <summary>
    /// Returns a collection of ValueTuple which contains the element's index and value.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static IEnumerable<IndexAndValue<TSource>> AsIndexValuePairs<TSource>(this IEnumerable @this)
    {
        int i = 0;
        foreach (TSource item in @this)
        {
            yield return new(i++, item);
        }
    }

    /// <summary>
    /// Returns a collection of ValueTuple which contains the element's index and value.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="this"></param>
    /// <returns></returns>
    public static IEnumerable<IndexAndValue<TSource>> AsIndexValuePairs<TSource>(this IEnumerable<TSource> @this)
    {
        int i = 0;
        foreach (var item in @this)
        {
            yield return new(i++, item);
        }
    }

    /// <summary>
    /// Do action for each item.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="this"></param>
    /// <param name="task"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Each<TSource>(this IEnumerable<TSource> @this, Action<TSource> task)
    {
        foreach (var item in @this)
            task(item);
        return @this;
    }

    /// <summary>
    /// Do action for each item.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="this"></param>
    /// <param name="task"></param>
    /// <returns></returns>
    public static IEnumerable Each<TSource>(IEnumerable @this, Action<TSource> task)
    {
        foreach (TSource item in @this)
            task(item);
        return @this;
    }

    /// <summary>
    /// Do action for each item.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="this"></param>
    /// <param name="task"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> Each<TSource>(this IEnumerable<TSource> @this, Action<TSource, int> task)
    {
        int i = 0;
        foreach (var item in @this)
            task(item, i++);
        return @this;
    }

    /// <summary>
    /// Do action for each item.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="this"></param>
    /// <param name="task"></param>
    /// <returns></returns>
    public static IEnumerable Each<TSource>(this IEnumerable @this, Action<TSource, int> task)
    {
        int i = 0;
        foreach (TSource item in @this)
            task(item, i++);
        return @this;
    }

    /// <summary>
    /// Concatenates the members of a collection, using the specified separator between each member.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="this"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string Join<TSource>(this IEnumerable<TSource> @this, string separator)
    {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET451_OR_GREATER
        return string.Join(separator, @this);
#else
        return string.Join(separator, @this.Select(x => x.ToString()).ToArray());
#endif
    }

    /// <summary>
    /// Creates a sliding window for the specified enumerable object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="capacity"></param>
    /// <param name="sharedCache"></param>
    /// <returns></returns>
    public static Sliding<T> Slide<T>(this IEnumerable<T> @this, int capacity, bool sharedCache)
    {
        return new(@this, capacity, sharedCache);
    }

}
