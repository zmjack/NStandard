using NStandard.Algorithm;
using System.Collections.Generic;

namespace NStandard
{
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int Locate<T>(this T[] @this, T[] pattern)
        {
            var searcher = new PatternSearcher<T>(pattern);
            return searcher.Match(@this, 0, @this.Length);
        }

        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="pattern"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int Locate<T>(this T[] @this, T[] pattern, int startIndex)
        {
            var searcher = new PatternSearcher<T>(pattern);
            return searcher.Match(@this, startIndex, @this.Length - startIndex);
        }

        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="pattern"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int Locate<T>(this T[] @this, T[] pattern, int startIndex, int count)
        {
            var searcher = new PatternSearcher<T>(pattern);
            return searcher.Match(@this, startIndex, count);
        }

        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static IEnumerable<int> Locates<T>(this T[] @this, T[] pattern)
        {
            var searcher = new PatternSearcher<T>(pattern);
            return searcher.Matches(@this, 0, @this.Length);
        }

        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="pattern"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static IEnumerable<int> Locates<T>(this T[] @this, T[] pattern, int startIndex)
        {
            var searcher = new PatternSearcher<T>(pattern);
            return searcher.Matches(@this, startIndex, @this.Length - startIndex);
        }

        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="pattern"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<int> Locates<T>(this T[] @this, T[] pattern, int startIndex, int count)
        {
            var searcher = new PatternSearcher<T>(pattern);
            return searcher.Matches(@this, startIndex, count);
        }

        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="searcher"></param>
        /// <returns></returns>
        public static int Locate<T>(this T[] @this, PatternSearcher<T> searcher)
        {
            return searcher.Match(@this, 0, @this.Length);
        }

        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="searcher"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int Locate<T>(this T[] @this, PatternSearcher<T> searcher, int startIndex)
        {
            return searcher.Match(@this, startIndex, @this.Length - startIndex);
        }

        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="searcher"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int Locate<T>(this T[] @this, PatternSearcher<T> searcher, int startIndex, int count)
        {
            return searcher.Match(@this, startIndex, count);
        }

        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="searcher"></param>
        /// <returns></returns>
        public static IEnumerable<int> Locates<T>(this T[] @this, PatternSearcher<T> searcher)
        {
            return searcher.Matches(@this, 0, @this.Length);
        }

        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="searcher"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static IEnumerable<int> Locates<T>(this T[] @this, PatternSearcher<T> searcher, int startIndex)
        {
            return searcher.Matches(@this, startIndex, @this.Length - startIndex);
        }

        /// <summary>
        /// Reports the zero-based index of all the occurrences of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="searcher"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<int> Locates<T>(this T[] @this, PatternSearcher<T> searcher, int startIndex, int count)
        {
            return searcher.Matches(@this, startIndex, count);
        }

    }
}
