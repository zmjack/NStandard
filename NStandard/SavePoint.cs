using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NStandard
{
    public class SavePoint<T> : ISliceLength<T[]>
    {
        private readonly T[] _store;
        private readonly int _maxIndex;
        public int Length { get; private set; }

        public SavePoint(int capacity)
        {
            if (capacity < 2) throw new ArgumentException("The capacity must be greater than 2.", nameof(capacity));

            _store = new T[capacity];
            Length = capacity;
            _maxIndex = capacity - 1;
        }

        public void Save(T obj)
        {
            for (int i = 0; i < _maxIndex; i++)
            {
                _store[i] = _store[i + 1];
            }
            _store[_maxIndex] = obj;
        }

        public T[] Slice(int start, int length)
        {
            if (start > 0 || length > Length) throw new ArgumentException("Non-positive number required.", nameof(start));

            var ret = new T[length];
            Array.Copy(_store, start + _maxIndex, ret, 0, length);
            return ret;
        }

        public T[] ToArray()
        {
            var ret = new T[Length];
            Array.Copy(_store, 0, ret, 0, Length);
            return ret;
        }

        public T this[int i]
        {
            get
            {
                if (i > 0) throw new ArgumentException("Non-positive number required.", nameof(i));

                return _store[i + _maxIndex];
            }
        }
    }

}
