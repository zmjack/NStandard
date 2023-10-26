using NStandard.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NStandard;

public static partial class Any
{
    private static ArgumentException EmptyArgumentException(string paramName) => new("The argument can not be empty.", paramName);

    /// <summary>
    /// Transform nested loops to a single loop.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="enumerables"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<ChainIterator<T>> Chain<T>(IEnumerable<T>[] enumerables)
    {
        if (enumerables is null) throw new ArgumentNullException(nameof(enumerables));
        if (enumerables.Length == 0) throw EmptyArgumentException(nameof(enumerables));

        var length = enumerables.Length;
        var cursor = 0;
        var maxIndex = length - 1;

        var iterators = new T[length];
        var chain = new ChainIterator<T>
        {
            Iterators = iterators,
        };

        var enumerators = new IEnumerator[length];
        for (int i = 0; i < enumerables.Length; i++)
        {
            enumerators[i] = enumerables[i].GetEnumerator();
        }
        var enumerator = enumerators[0];

        bool MoveNext()
        {
            if (enumerator.MoveNext())
            {
                var current = (T)enumerator.Current;
                iterators[cursor] = current;
                chain.Cursor = cursor;

                if (cursor < maxIndex)
                {
                    chain.Origin = ChainOrigin.Begin;

                    cursor++;
                    enumerator = enumerators[cursor];
                    enumerator.Reset();
                    enumerators[cursor] = enumerator;
                    return true;
                }
                else
                {
                    chain.Origin = ChainOrigin.Current;
                    return true;
                }
            }
            else
            {
                iterators[cursor] = default;
                cursor--;
                if (cursor >= 0)
                {
                    chain.Cursor = cursor;

                    chain.Origin = ChainOrigin.End;
                    enumerator = enumerators[cursor];
                    return true;
                }
                else return false;
            }
        }

        while (MoveNext())
        {
            yield return chain;
        }
    }

    /// <summary>
    /// Transform nested loops to a single loop.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="seed"></param>
    /// <param name="generators"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<ChainIterator<T>> Chain<T>(T seed, Func<T, IEnumerable<T>>[] generators)
    {
        if (seed is null) throw new ArgumentNullException(nameof(seed));
        if (generators is null) throw new ArgumentNullException(nameof(generators));
        if (generators.Length == 0) throw EmptyArgumentException(nameof(generators));

        var length = generators.Length;
        var cursor = 0;
        var maxIndex = length - 1;

        var iterators = new T[length];
        var chain = new ChainIterator<T>
        {
            Iterators = iterators,
        };

        var enumerators = new IEnumerator[length];
        enumerators[0] = generators[0](seed).GetEnumerator();
        var enumerator = enumerators[0];

        bool MoveNext()
        {
            if (enumerator.MoveNext())
            {
                var current = (T)enumerator.Current;
                iterators[cursor] = current;
                chain.Cursor = cursor;

                if (cursor < maxIndex)
                {
                    chain.Origin = ChainOrigin.Begin;

                    cursor++;
                    enumerator = generators[cursor](current).GetEnumerator();
                    enumerators[cursor] = enumerator;
                    return true;
                }
                else
                {
                    chain.Origin = ChainOrigin.Current;
                    return true;
                }
            }
            else
            {
                iterators[cursor] = default;
                cursor--;
                if (cursor >= 0)
                {
                    chain.Cursor = cursor;

                    chain.Origin = ChainOrigin.End;
                    enumerator = enumerators[cursor];
                    return true;
                }
                else return false;
            }
        }

        while (MoveNext())
        {
            yield return chain;
        }
    }

}
