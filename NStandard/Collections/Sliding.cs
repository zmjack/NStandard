using System;
using System.Collections;
using System.Collections.Generic;

namespace NStandard.Collections;

public class Sliding<T> : IEnumerable<T?[]>
{
    private readonly Enumerator _enumerator;

    public Sliding(IEnumerable<T?> source, int capacity, bool sharedCache)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
#else
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
#endif
        _enumerator = new(source, capacity, sharedCache);
    }

    public Sliding(IEnumerable<T?> source, int capacity, bool sharedCache, SlidingMode mode, T? padPeft, T? padRight)
    {
#if NET8_0_OR_GREATER
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
#else
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
#endif
        _enumerator = new(source, capacity, sharedCache, mode, padPeft, padRight);
    }

    public IEnumerator<T?[]> GetEnumerator() => _enumerator;
    IEnumerator IEnumerable.GetEnumerator() => _enumerator;

    private class Enumerator : FixedSizeQueue<T>, IEnumerator<T?[]>
    {
        private readonly IEnumerator<T?> _sourceEnumerator;
        private readonly bool _sharedCache;

        public T?[] Current
        {
            get
            {
                if (_sharedCache) return _store;
                else
                {
                    var current = new T[Capacity];
                    Array.Copy(_store, current, _store.Length);
                    return current;
                }
            }
        }
        object IEnumerator.Current => _store;

        public Enumerator(IEnumerable<T?> source, int capacity, bool sharedCache) : base(capacity)
        {
            _sourceEnumerator = source.GetEnumerator();
            _sharedCache = sharedCache;
        }

        public Enumerator(IEnumerable<T?> source, int capacity, bool sharedCache, SlidingMode mode, T? padLeft, T? padRight) : base(capacity)
        {
            IEnumerator<T?> Build()
            {
                if (mode.HasFlag(SlidingMode.PadLeft))
                {
                    for (var i = 0; i < capacity - 1; i++)
                    {
                        yield return padLeft;
                    }
                }

                foreach (var item in source)
                {
                    yield return item;
                }

                if (mode.HasFlag(SlidingMode.PadRight))
                {
                    for (var i = 0; i < capacity - 1; i++)
                    {
                        yield return padRight;
                    }
                }
            }

            _sourceEnumerator = Build();
            _sharedCache = sharedCache;
        }

        public bool MoveNext()
        {
            while (_sourceEnumerator.MoveNext())
            {
                Enqueue(_sourceEnumerator.Current);
                if (IsFilled) { return true; }
            }
            return false;
        }

        public void Reset()
        {
            _sourceEnumerator.Reset();
            IsFilled = false;
        }

        public void Dispose()
        {
        }
    }
}
