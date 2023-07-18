using System;
using System.Collections.Generic;
using System.Linq;
#if NET5_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace NStandard
{
    public static class ArrayEx
    {
        private static ArgumentException Exception_InsufficientElements(string paramName) => new("Insufficient elements in source array.", paramName);
        private static ArgumentException Exception_CopyingOverflow(string paramName) => new("Copying the specified array results in overflow.", paramName);

#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET46_OR_GREATER
#else
        private static class EmptyArray<T>
        {
#pragma warning disable CA1825 // this is the implementation of Array.Empty<T>()
            internal static readonly T[] Value = new T[0];
#pragma warning restore CA1825
        }

        public static T[] Empty<T>()
        {
            return EmptyArray<T>.Value;
        }
#endif

        /// <summary>
        /// Assign values to arrays of indeterminate dimensions using one-dimensional arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        public static void Assign<T>(Array destination, IEnumerable<T> source)
        {
            var count = source.Count();
            if (destination.GetSequenceLength() < count) throw Exception_CopyingOverflow(nameof(source));

            var stepper = new IndicesStepper(0, destination.GetLengths());
            foreach (var (value, indices) in Any.Zip(source, stepper))
            {
                destination.SetValue(value, indices);
            }
        }

        /// <summary>
        /// Assign values to arrays of indeterminate dimensions using one-dimensional arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destination"></param>
        /// <param name="destinationIndex"></param>
        /// <param name="source"></param>
        /// <param name="sourceIndex"></param>
        /// <param name="length"></param>
        public static void Assign<T>(Array destination, int destinationIndex, T[] source, int sourceIndex, int length)
        {
            if ((source.Length - sourceIndex) < length) throw Exception_InsufficientElements(nameof(source));
            if ((destination.GetSequenceLength() - destinationIndex) < length) throw Exception_CopyingOverflow(nameof(source));

            var stepper = new IndicesStepper(destinationIndex, destination.GetLengths());
            foreach (var (value, indices) in Any.Zip(source.Skip(sourceIndex).Take(length), stepper))
            {
                destination.SetValue(value, indices);
            }
        }

#if NETCOREAPP3_0_OR_GREATER
        /// <summary>
        /// Assign values to arrays of indeterminate dimensions using one-dimensional arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="length"></param>
        public static unsafe void Assign<T>(T* destination, T* source, int length) where T : unmanaged
        {
            Unsafe.CopyBlock(destination, source, (uint)(length * sizeof(T)));
        }

        /// <summary>
        /// Assign values to arrays of indeterminate dimensions using one-dimensional arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="destination"></param>
        /// <param name="destinationIndex"></param>
        /// <param name="source"></param>
        /// <param name="sourceIndex"></param>
        /// <param name="length"></param>
        public static unsafe void Assign<T>(T* destination, int destinationIndex, T* source, int sourceIndex, int length) where T : unmanaged
        {
            Unsafe.CopyBlock(destination + destinationIndex, source + sourceIndex, (uint)(length * sizeof(T)));
        }
#endif
    }
}
