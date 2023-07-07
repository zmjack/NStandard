using System;
using System.Linq;

namespace NStandard
{
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] @this, T value)
        {
            return Array.IndexOf(@this, value);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] @this, T value, int startIndex)
        {
            return Array.IndexOf(@this, value, startIndex);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] @this, T value, int startIndex, int count)
        {
            return Array.IndexOf(@this, value, startIndex, count);
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified element in this array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] @this, Func<T, bool> predicate)
        {
            int i = 0;
            foreach (var e in @this)
            {
                if (predicate(e))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified element in this array.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] @this, Func<T, bool> predicate, int startIndex)
        {
            int i = startIndex;
            foreach (var e in @this.Skip(startIndex))
            {
                if (predicate(e))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified element in this array.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] @this, Func<T, bool> predicate, int startIndex, int count)
        {
            int i = startIndex;
            int end = startIndex + count;
            if (end < 1) return -1;

            foreach (var e in @this.Skip(startIndex))
            {
                if (predicate(e))
                    return i;
                i++;
                if (i == end) break;
            }
            return -1;
        }

    }
}
