using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NStandard.Collections;

public class Sequence<T> : IEnumerable<T>
{
    private IEnumerable<T> _enumerable;
    private bool _notEmpty;

    public Sequence()
    {
        Clear();
    }

    public void Add(T value)
    {
        _notEmpty = true;
        _enumerable = _enumerable.Concat(new[] { value });
    }

    public void Add(IEnumerable<T> values)
    {
        _notEmpty = true;
        _enumerable = _enumerable.Concat(values);
    }

    public void Clear()
    {
        _notEmpty = false;
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
        _enumerable = Array.Empty<T>();
#else
        _enumerable = ArrayEx.Empty<T>();
#endif
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _enumerable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _enumerable.GetEnumerator();
    }
}
