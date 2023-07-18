using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static partial class ArrayExtensions
    {
        /// <summary>
        /// Gets the index of the first element in the array.
        ///     Usually, LBound() returns 0, since arrays are zero-based by default.
        ///     but in some rare cases they are not.
        ///     For example, you use Array.CreateInstance(Type, int[], int[]) to create an Array.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int LBound(this Array @this) => @this.GetLowerBound(0);

        /// <summary>
        /// Gets the index of the last element in the array.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int UBound(this Array @this) => @this.GetUpperBound(0);

        /// <summary>
        /// Retrieves an array from this instance. The new array starts at a specified
        ///     element position and continues to the end of the array.
        ///     (If the parameter is negative, the search will start on the right.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static T[] Slice<T>(this T[] @this, int start) => Slice(@this, start, @this.Length);

        /// <summary>
        /// Retrieves an array from this instance. The new array starts at a specified
        ///     element position and ends with a specified element position.
        ///     (If the parameters is negative, the search will start on the right.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static T[] Slice<T>(this T[] @this, int start, int stop)
        {
            start = GetElementPosition(ref @this, start);
            stop = GetElementPosition(ref @this, stop);

            var length = stop - start;
            if (length > 0)
            {
                var ret = new T[length];
                Array.Copy(@this, start, ret, 0, length);
                return ret;
            }
            else if (length == 0)
            {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET46_OR_GREATER
                return Array.Empty<T>();
#else
                return ArrayEx.Empty<T>();
#endif
            }
            else throw new IndexOutOfRangeException($"'{nameof(start)}:{start}' can not greater than '{nameof(stop)}:{stop}'.");
        }
        private static int GetElementPosition<T>(ref T[] str, int pos) => pos < 0 ? str.Length + pos : pos;

        /// <summary>
        /// Shuffles array and returns itself.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static void Shuffle<T>(this T[] @this)
        {
            var length = @this.Length;
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                var rnd = random.Next(length);
                var take = @this[i];
                @this[i] = @this[rnd];
                @this[rnd] = take;
            }
        }

        public static T GetValueOrDefault<T>(this T[] @this, int index)
        {
            var lbound = @this.GetLowerBound(0);
            var ubound = @this.GetUpperBound(0);
            if (lbound <= index && index <= ubound)
            {
                return @this[index];
            }
            else return default;
        }

        public static bool TryGetValue<T>(this T[] @this, int index, out T value)
        {
            var lbound = @this.GetLowerBound(0);
            var ubound = @this.GetUpperBound(0);
            if (lbound <= index && index <= ubound)
            {
                value = @this[index];
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        private static IEnumerable<long> DoGetLongLengths(Array @this)
        {
            for (int i = 0; i < @this.Rank; i++)
            {
                yield return @this.GetLongLength(i);
            }
        }

        public static long[] GetLongLengths(this Array @this)
        {
            return DoGetLongLengths(@this).ToArray();
        }

        private static IEnumerable<int> DoGetLengths(Array @this)
        {
            for (int i = 0; i < @this.Rank; i++)
            {
                yield return @this.GetLength(i);
            }
        }

        public static int[] GetLengths(this Array @this)
        {
            return DoGetLengths(@this).ToArray();
        }

        public static int GetSequenceLength(this Array @this)
        {
            int length = 1;
            for (int i = 0; i < @this.Rank; i++)
            {
                length *= @this.GetLength(i);
            }
            return length;
        }

        public static long GetSequenceLongLength(this Array @this)
        {
            long length = 1;
            for (int i = 0; i < @this.Rank; i++)
            {
                length *= @this.GetLongLength(i);
            }
            return length;
        }

        /// <summary>
        /// Use the specified function to initialize each element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IEnumerable<TElement> AsEnumerable<TElement>(this Array @this)
        {
            foreach (TElement item in @this)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Converts all elements to the specified type.
        /// </summary>
        /// <typeparam name="TConvertFrom"></typeparam>
        /// <typeparam name="TConvertTo"></typeparam>
        /// <param name="source"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static Array Map<TConvertFrom, TConvertTo>(this Array source, Func<TConvertFrom, TConvertTo> convert)
        {
            return ArrayMapper.Map(source, convert);
        }

    }
}
