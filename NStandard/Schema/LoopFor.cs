using System;
using System.Collections;
using System.Collections.Generic;

namespace NStandard.Schema;

public static class LoopFor
{
    public static LoopFor<T> Create<T>(Func<T> init, Predicate<T> predicate, Func<T, T> final) where T : struct
    {
        return new LoopFor<T>(init, predicate, final);
    }
}

public class LoopFor<T> : IEnumerable<T> where T : struct
{
    public IEnumerable<T> Enumerable { get; private set; }

    public LoopFor(Func<T> init, Predicate<T> predicate, Func<T, T> final)
    {
        Enumerable = BuildEnumerable(init, predicate, final);
    }

    public LoopFor(IEnumerable<T> enumerable)
    {
        Enumerable = enumerable;
    }

    private IEnumerable<T> BuildEnumerable(Func<T> init, Predicate<T> predicate, Func<T, T> final)
    {
        for (var param = init(); predicate(param); param = final(param))
        {
            yield return param;
        }
    }

    public IEnumerator<T> GetEnumerator() => Enumerable.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Enumerable.GetEnumerator();

}
