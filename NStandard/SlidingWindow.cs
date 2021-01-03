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
        public int Capacity { get; private set; }
        public bool IsFilled { get; private set; }
        public int Length => Capacity;

        public SlidingWindow(int capacity)
        {
            if (capacity < 2) throw new ArgumentException("The capacity must be greater than 2.", nameof(capacity));

            _store = new T[capacity];
            Capacity = capacity;
            _maxIndex = capacity - 1;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Fill(T obj)
        {
            var lastIndex = IsFilled ? _maxIndex : _fillCount;

            for (int i = lastIndex; i >= 1; i--)
            {
                _store[i - 1] = _store[i];
            }
            _store[_maxIndex] = obj;

            if (!IsFilled)
            {
                _fillCount++;
                if (_fillCount == Capacity) IsFilled = true;
            }
        }

        public T[] Slice(int start, int length)
        {
            var ret = new T[length];
            Array.Copy(_store, start, ret, 0, length);
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

        public T this[int i] => _store[i];
    }

}
