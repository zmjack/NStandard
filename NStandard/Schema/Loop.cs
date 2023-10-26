using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NStandard.Schema;

public static class Loop
{
    public static Loop<int> Create(params int[] lengths)
    {
        return new Loop<int>(lengths.Select(x => LoopFor.Create(() => 0, i => i < x, i => i + 1)).ToArray());
    }

    public static Loop<T> Create<T>(params LoopFor<T>[] loopFors) where T : struct => new(loopFors);
    public static Loop<T> Create<T>(params IEnumerable<T>[] enumerables) where T : struct => new(enumerables);
}

public class Loop<T> : IEnumerable<T?[]> where T : struct
{
    public IEnumerable<T?[]> Enumerable { get; private set; }

    public Loop(params LoopFor<T>[] loopFors) : this(loopFors.OfType<IEnumerable<T>>().ToArray()) { }
    public Loop(params IEnumerable<T>[] enumerables)
    {
        Enumerable = BuildEnumerable(enumerables);
    }

    IEnumerable<T?[]> BuildEnumerable(IEnumerable<T>[] enumerables)
    {
        var length = enumerables.Length;
        int lastIndex = length - 1;
        var enumerators = enumerables.Select(x => x.GetEnumerator()).ToArray();

        foreach (var kv in enumerators.AsIndexValuePairs())
        {
            var enumerator = kv.Value;
            if (!enumerator.MoveNext()) lastIndex = kv.Index - 1;
        }

        bool next(int pos)
        {
            if (pos < 0) return false;

            var enumerator = enumerators[pos];
            if (!enumerator.MoveNext())
            {
                enumerator = enumerators[pos] = enumerables[pos].GetEnumerator();
                enumerator.MoveNext();
                return next(pos - 1);
            }
            else return true;
        }

        if (lastIndex > -1)
        {
            do
            {
                var iterator = new T?[length];
                for (int i = 0; i < length; i++)
                {
                    if (i <= lastIndex) iterator[i] = enumerators[i].Current;
                    else iterator[i] = null;
                }
                yield return iterator;
            } while (next(lastIndex));
        }
    }

    public IEnumerator<T?[]> GetEnumerator() => Enumerable.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
