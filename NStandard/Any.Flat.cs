using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NStandard
{
    public static partial class Any
    {
        private static string IncompatibleRank() => "The lengths can not be incompatible with the array.";

        private static IEnumerable<T> DoFlat<T>(IEnumerable source)
        {
            var enumerator = source.GetEnumerator();
            for (int i = 0; enumerator.MoveNext(); i++)
            {
                yield return (T)enumerator.Current;
            }
        }

        private static IEnumerable<T> DoFlatMany<T>(IEnumerable[] sources)
        {
            foreach (var source in sources)
            {
                var enumerator = source.GetEnumerator();
                for (int i = 0; enumerator.MoveNext(); i++)
                {
                    yield return (T)enumerator.Current;
                }
            }
        }

        /// <summary>
        /// Creates a one-dimensional array containing all elements of a multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T[] Flat<T>(IEnumerable source)
        {
            return DoFlat<T>(source).ToArray();
        }

        /// <summary>
        /// Creates a one-dimensional array containing all elements of the specified multidimensional arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T[] Flat<T>(params IEnumerable[] sources)
        {
            return DoFlatMany<T>(sources).ToArray();
        }

#if NET5_0_OR_GREATER
        private static string LengthMustBeGreaterThanZero() => $"The length must be greater than 0.";
        private static string AnyLengthMustBeGreaterThanZero() => $"Any lengths must be greater than 0.";
        private static string IncompatibleLength() => "The length of sources must be the same as the specified length.";

        /// <summary>
        /// Creates a one-dimensional array containing all elements of a multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="psource"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static unsafe T[] Flat<T>(T* psource, int length) where T : unmanaged
        {
            if (length < 0) throw new ArgumentException(LengthMustBeGreaterThanZero(), nameof(length));

            var dst = Array.CreateInstance(typeof(T), length) as T[];
            fixed (T* pdst = dst)
            {
                Unsafe.CopyBlock(pdst, psource, (uint)(length * sizeof(T)));
            }
            return dst;
        }

        /// <summary>
        /// Creates a one-dimensional array containing all elements of the specified multidimensional arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="psources"></param>
        /// <param name="lengths"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static unsafe T[] Flat<T>(T*[] psources, int[] lengths) where T : unmanaged
        {
            if (psources.Length != lengths.Length) throw new ArgumentException(IncompatibleLength(), nameof(lengths));
            if (lengths.Any(x => x <= 0)) throw new ArgumentException(AnyLengthMustBeGreaterThanZero(), nameof(lengths));

            var size = sizeof(T);
            var dst = Array.CreateInstance(typeof(T), lengths.Sum()) as T[];
            fixed (T* pdst = dst)
            {
                var _pdst = pdst;
                for (int i = 0; i < psources.Length; i++)
                {
                    Unsafe.CopyBlock(_pdst, psources[i], (uint)(lengths[i] * size));
                    _pdst += lengths[i];
                }
            }
            return dst;
        }
#endif
    }
}
