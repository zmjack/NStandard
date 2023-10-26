using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NStandard;

public static partial class Any
{
    /// <summary>
    /// Creates a one-dimensional enumeration containing all elements of the specified multidimensional arrays.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sources"></param>
    /// <returns></returns>
    public static IEnumerable<T> Flat<T>(params IEnumerable[] sources)
    {
        var flater = new Flater<T>(sources.GetEnumerator());
        while (flater.MoveNext())
        {
            yield return (T)flater.Current;
        }
    }

#if NETCOREAPP3_0_OR_GREATER
    private static ArgumentException Exception_LengthMustBeGreaterThanZero(string paramName) => new($"The length must be greater than 0.", paramName);
    private static ArgumentException Exception_AnyLengthMustBeGreaterThanZero(string paramName) => new($"Any lengths must be greater than 0.", paramName);
    private static ArgumentException Exception_IncompatibleLength(string paramName) => new("The length of sources must be the same as the specified length.", paramName);

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
        if (length < 0) throw Exception_LengthMustBeGreaterThanZero(nameof(length));

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
        if (psources.Length != lengths.Length) throw Exception_IncompatibleLength(nameof(lengths));
        if (lengths.Any(x => x <= 0)) throw Exception_AnyLengthMustBeGreaterThanZero(nameof(lengths));

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
