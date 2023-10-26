﻿// <auto-generated/>
using System.Collections.Generic;
using System.Linq;

namespace NStandard;

public static partial class Any
{
    /// <summary>
    /// Iterate over each element of multiple sequences simultaneously.
    /// </summary>
    public static IEnumerable<IEnumerable<T1>> Zip<T1>(IEnumerable<IEnumerable<T1>> enumerables)
    {
        IEnumerator<T1>[] enumerators = enumerables.Select(x => x.GetEnumerator()).ToArray();
        while (enumerators.All(x => x.MoveNext()))
        {
            yield return enumerators.Select(e => e.Current);
        }
    }
    /// <summary>
    /// Iterate over each element of multiple sequences simultaneously.
    /// </summary>
    public static IEnumerable<StructTuple<T1, T2>> Zip<T1, T2>(
        IEnumerable<T1> e1,
        IEnumerable<T2> e2
    )
    {
        using IEnumerator<T1> _e1 = e1.GetEnumerator();
        using IEnumerator<T2> _e2 = e2.GetEnumerator();

        while (_e1.MoveNext()
            && _e2.MoveNext())
        {
            yield return new(
                _e1.Current,
                _e2.Current
            );
        }
    }
    /// <summary>
    /// Iterate over each element of multiple sequences simultaneously.
    /// </summary>
    public static IEnumerable<StructTuple<T1, T2, T3>> Zip<T1, T2, T3>(
        IEnumerable<T1> e1,
        IEnumerable<T2> e2,
        IEnumerable<T3> e3
    )
    {
        using IEnumerator<T1> _e1 = e1.GetEnumerator();
        using IEnumerator<T2> _e2 = e2.GetEnumerator();
        using IEnumerator<T3> _e3 = e3.GetEnumerator();

        while (_e1.MoveNext()
            && _e2.MoveNext()
            && _e3.MoveNext())
        {
            yield return new(
                _e1.Current,
                _e2.Current,
                _e3.Current
            );
        }
    }
    /// <summary>
    /// Iterate over each element of multiple sequences simultaneously.
    /// </summary>
    public static IEnumerable<StructTuple<T1, T2, T3, T4>> Zip<T1, T2, T3, T4>(
        IEnumerable<T1> e1,
        IEnumerable<T2> e2,
        IEnumerable<T3> e3,
        IEnumerable<T4> e4
    )
    {
        using IEnumerator<T1> _e1 = e1.GetEnumerator();
        using IEnumerator<T2> _e2 = e2.GetEnumerator();
        using IEnumerator<T3> _e3 = e3.GetEnumerator();
        using IEnumerator<T4> _e4 = e4.GetEnumerator();

        while (_e1.MoveNext()
            && _e2.MoveNext()
            && _e3.MoveNext()
            && _e4.MoveNext())
        {
            yield return new(
                _e1.Current,
                _e2.Current,
                _e3.Current,
                _e4.Current
            );
        }
    }
    /// <summary>
    /// Iterate over each element of multiple sequences simultaneously.
    /// </summary>
    public static IEnumerable<StructTuple<T1, T2, T3, T4, T5>> Zip<T1, T2, T3, T4, T5>(
        IEnumerable<T1> e1,
        IEnumerable<T2> e2,
        IEnumerable<T3> e3,
        IEnumerable<T4> e4,
        IEnumerable<T5> e5
    )
    {
        using IEnumerator<T1> _e1 = e1.GetEnumerator();
        using IEnumerator<T2> _e2 = e2.GetEnumerator();
        using IEnumerator<T3> _e3 = e3.GetEnumerator();
        using IEnumerator<T4> _e4 = e4.GetEnumerator();
        using IEnumerator<T5> _e5 = e5.GetEnumerator();

        while (_e1.MoveNext()
            && _e2.MoveNext()
            && _e3.MoveNext()
            && _e4.MoveNext()
            && _e5.MoveNext())
        {
            yield return new(
                _e1.Current,
                _e2.Current,
                _e3.Current,
                _e4.Current,
                _e5.Current
            );
        }
    }
    /// <summary>
    /// Iterate over each element of multiple sequences simultaneously.
    /// </summary>
    public static IEnumerable<StructTuple<T1, T2, T3, T4, T5, T6>> Zip<T1, T2, T3, T4, T5, T6>(
        IEnumerable<T1> e1,
        IEnumerable<T2> e2,
        IEnumerable<T3> e3,
        IEnumerable<T4> e4,
        IEnumerable<T5> e5,
        IEnumerable<T6> e6
    )
    {
        using IEnumerator<T1> _e1 = e1.GetEnumerator();
        using IEnumerator<T2> _e2 = e2.GetEnumerator();
        using IEnumerator<T3> _e3 = e3.GetEnumerator();
        using IEnumerator<T4> _e4 = e4.GetEnumerator();
        using IEnumerator<T5> _e5 = e5.GetEnumerator();
        using IEnumerator<T6> _e6 = e6.GetEnumerator();

        while (_e1.MoveNext()
            && _e2.MoveNext()
            && _e3.MoveNext()
            && _e4.MoveNext()
            && _e5.MoveNext()
            && _e6.MoveNext())
        {
            yield return new(
                _e1.Current,
                _e2.Current,
                _e3.Current,
                _e4.Current,
                _e5.Current,
                _e6.Current
            );
        }
    }
    /// <summary>
    /// Iterate over each element of multiple sequences simultaneously.
    /// </summary>
    public static IEnumerable<StructTuple<T1, T2, T3, T4, T5, T6, T7>> Zip<T1, T2, T3, T4, T5, T6, T7>(
        IEnumerable<T1> e1,
        IEnumerable<T2> e2,
        IEnumerable<T3> e3,
        IEnumerable<T4> e4,
        IEnumerable<T5> e5,
        IEnumerable<T6> e6,
        IEnumerable<T7> e7
    )
    {
        using IEnumerator<T1> _e1 = e1.GetEnumerator();
        using IEnumerator<T2> _e2 = e2.GetEnumerator();
        using IEnumerator<T3> _e3 = e3.GetEnumerator();
        using IEnumerator<T4> _e4 = e4.GetEnumerator();
        using IEnumerator<T5> _e5 = e5.GetEnumerator();
        using IEnumerator<T6> _e6 = e6.GetEnumerator();
        using IEnumerator<T7> _e7 = e7.GetEnumerator();

        while (_e1.MoveNext()
            && _e2.MoveNext()
            && _e3.MoveNext()
            && _e4.MoveNext()
            && _e5.MoveNext()
            && _e6.MoveNext()
            && _e7.MoveNext())
        {
            yield return new(
                _e1.Current,
                _e2.Current,
                _e3.Current,
                _e4.Current,
                _e5.Current,
                _e6.Current,
                _e7.Current
            );
        }
    }
    /// <summary>
    /// Iterate over each element of multiple sequences simultaneously.
    /// </summary>
    public static IEnumerable<StructTuple<T1, T2, T3, T4, T5, T6, T7, TRest>> Zip<T1, T2, T3, T4, T5, T6, T7, TRest>(
        IEnumerable<T1> e1,
        IEnumerable<T2> e2,
        IEnumerable<T3> e3,
        IEnumerable<T4> e4,
        IEnumerable<T5> e5,
        IEnumerable<T6> e6,
        IEnumerable<T7> e7,
        IEnumerable<TRest> rest
    )
    {
        using IEnumerator<T1> _e1 = e1.GetEnumerator();
        using IEnumerator<T2> _e2 = e2.GetEnumerator();
        using IEnumerator<T3> _e3 = e3.GetEnumerator();
        using IEnumerator<T4> _e4 = e4.GetEnumerator();
        using IEnumerator<T5> _e5 = e5.GetEnumerator();
        using IEnumerator<T6> _e6 = e6.GetEnumerator();
        using IEnumerator<T7> _e7 = e7.GetEnumerator();
        using IEnumerator<TRest> _rest = rest.GetEnumerator();

        while (_e1.MoveNext()
            && _e2.MoveNext()
            && _e3.MoveNext()
            && _e4.MoveNext()
            && _e5.MoveNext()
            && _e6.MoveNext()
            && _e7.MoveNext()
            && _rest.MoveNext())
        {
            yield return new(
                _e1.Current,
                _e2.Current,
                _e3.Current,
                _e4.Current,
                _e5.Current,
                _e6.Current,
                _e7.Current,
                _rest.Current
            );
        }
    }
}

