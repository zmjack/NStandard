using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NStandard
{
    public class Flater<T> : IEnumerator
    {
        public bool End { get; private set; }

        private readonly Stack<IEnumerator> Stack = new();
        private object _current;

        /// <summary>
        /// The sources for flatting.
        /// </summary>
        public IEnumerator Source { get; }

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public object Current => _current;

        public Flater(IEnumerator source)
        {
            Source = source;
            Stack.Push(Source);
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            while (Stack.Count > 0)
            {
                var peekElement = Stack.Peek();
                if (peekElement.MoveNext())
                {
                    var current = peekElement.Current;
                    if (current is not T && current is IEnumerable enumerator)
                    {
                        Stack.Push(enumerator.GetEnumerator());
                    }
                    else
                    {
                        if (current is T element)
                        {
                            _current = element;
                            return true;
                        }
                    }
                }
                else
                {
                    Stack.Pop();
                }
            }
            return false;
        }

        /// <summary>
        /// Sets all the source enumerators to its initial position, which is before the first element
        ///     in the collection.
        /// </summary>
        public void Reset()
        {
            Stack.Clear();
            Stack.Push(Source);
            Source.Reset();
        }
    }

    public static partial class Any
    {
        private static string IncompatibleRank() => "The lengths can not be incompatible with the array.";

        /// <summary>
        /// Creates a one-dimensional array containing all elements of the specified multidimensional arrays.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static IEnumerable<T> Flat<T>(params IEnumerable[] sources)
        {
            var flaters = sources.Select(x => new Flater<T>(x.GetEnumerator())).ToArray();
            foreach (var flater in flaters)
            {
                while (flater.MoveNext())
                {
                    yield return (T)flater.Current;
                }
            }
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
