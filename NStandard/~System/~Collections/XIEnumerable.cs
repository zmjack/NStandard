using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XIEnumerable
    {
        /// <summary>
        /// Returns a collection of tuples containing values and indexes.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, TSource>> AsKvPairs<TSource>(this IEnumerable @this)
        {
            int i = 0;
            foreach (TSource item in @this)
                yield return new KeyValuePair<int, TSource>(i++, item);
        }

        /// <summary>
        /// Returns a collection of KeyValuePair which contains the element's index(Key) and value.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, TSource>> AsKvPairs<TSource>(this IEnumerable<TSource> @this)
        {
            int i = 0;
            foreach (var item in @this)
                yield return new KeyValuePair<int, TSource>(i++, item);
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
        public static string Join<TSource>(this IEnumerable<TSource> @this, string separator) => string.Join(separator, @this);

    }
}
