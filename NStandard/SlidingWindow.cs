using System;
using System.Collections;
using System.Collections.Generic;

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

        public static IEnumerable<SlidingWindow<T>> Slide<T>(IEnumerable<T> enumerable, int lIndex, int uIndex)
        {
            var window = new SlidingWindow<T>(lIndex, uIndex);
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
        private readonly int _storeMaxIndex;
        private int _fillCount;
        public int Capacity { get; private set; }
        public int Length => Capacity;
        public bool IsFilled { get; private set; }
        public int LIndex { get; }
        public int UIndex { get; }

        public SlidingWindow(int capacity)
        {
            if (capacity < 2) throw new ArgumentException("The capacity must be greater than 2.", nameof(capacity));

            Capacity = capacity;
            _store = new T[capacity];
            _storeMaxIndex = capacity - 1;
            LIndex = 0;
            UIndex = _storeMaxIndex;
        }

        public SlidingWindow(int lIndex, int uIndex)
        {
            if (lIndex > uIndex) throw new ArgumentException($"'{nameof(lIndex)}' must be less than {nameof(uIndex)}.");

            var capacity = uIndex - lIndex + 1;
            Capacity = capacity;
            _store = new T[capacity];
            _storeMaxIndex = capacity - 1;
            LIndex = lIndex;
            UIndex = uIndex;
        }

        public void Fill(T obj)
        {
            var startIndex = IsFilled ? 0 : _storeMaxIndex - _fillCount;
            for (int i = startIndex; i < _storeMaxIndex; i++)
            {
                _store[i] = _store[i + 1];
            }
            _store[_storeMaxIndex] = obj;

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

        public T this[int i]
        {
            get
            {
                if (LIndex <= i && i <= UIndex) return _store[i - LIndex];
                else throw new IndexOutOfRangeException($"Index out of range ({LIndex} to {UIndex}).");
            }
        }
    }

}
