using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NStandard
{
    public static class SlidingWindow
    {
        public static IEnumerable<SlidingWindow<T>> Slide<T>(IEnumerable<T> enumerable, int capacity)
        {
            var window = new SlidingWindow<T>(capacity);
            foreach (var item in enumerable)
            {
                window.Fill(item);
                if (window.IsFilled) yield return window;
            }
        }
    }

    public class SlidingWindow<T> : ISliceLength<T[]>, IEnumerable<T>
    {
        private readonly T[] _store;
        private readonly int _maxIndex;
        private int _fillCount;
        public int Length { get; private set; }
        public bool IsFilled => _fillCount == Length;

        public SlidingWindow(int capacity)
        {
            if (capacity < 2) throw new ArgumentException("The capacity must be greater than 2.", nameof(capacity));

            _store = new T[capacity];
            Length = capacity;
            _maxIndex = capacity - 1;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Fill(T obj)
        {
            for (int i = 0; i < _maxIndex; i++)
            {
                _store[i] = _store[i + 1];
            }
            _store[_maxIndex] = obj;

            if (_fillCount < Length) _fillCount++;
        }

        public T[] Slice(int start, int length)
        {
            if (start > 0 || length > Length) throw new ArgumentException("Non-positive number required.", nameof(start));

            var ret = new T[length];
            Array.Copy(_store, start + _maxIndex, ret, 0, length);
            return ret;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var value in _store) yield return value;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var value in _store) yield return value;
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
