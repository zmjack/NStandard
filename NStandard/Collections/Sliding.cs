using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NStandard.Collections
{
    public class Sliding<T> : IEnumerable<T[]>
    {
        private readonly Enumerator _enumerator;

        public Sliding(IEnumerable<T> source, int capacity, bool sharedCache)
        {
            _enumerator = new(source, capacity, sharedCache);
        }

        public IEnumerator<T[]> GetEnumerator() => _enumerator;
        IEnumerator IEnumerable.GetEnumerator() => _enumerator;

        private class Enumerator : FixedSizeQueue<T>, IEnumerator<T[]>
        {
            private readonly IEnumerator<T> _sourceEnumerator;
            private readonly bool _sharedCache;

            public T[] Current
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

            public Enumerator(IEnumerable<T> source, int capacity, bool sharedCache) : base(capacity)
            {
                _sourceEnumerator = source.GetEnumerator();
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

}
