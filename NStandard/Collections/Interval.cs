#if NET7_0_OR_GREATER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace NStandard.Collections
{
    public class Interval<T> : IEnumerable<Interval<T>.Range>, IEquatable<Interval<T>>,
        IAdditionOperators<Interval<T>, T, Interval<T>>,
        IAdditionOperators<Interval<T>, Interval<T>.Range, Interval<T>>,
        IAdditionOperators<Interval<T>, Interval<T>, Interval<T>>,
        ISubtractionOperators<Interval<T>, T, Interval<T>>,
        ISubtractionOperators<Interval<T>, Interval<T>.Range, Interval<T>>,
        ISubtractionOperators<Interval<T>, Interval<T>, Interval<T>>
        where T : IComparisonOperators<T, T, bool>, IBinaryInteger<T>
    {
        [DebuggerDisplay("({Start}, {End})")]
        public struct Range : IEquatable<Range>
        {
            public T Start
            {
                get; set;
            }
            public T End
            {
                get; set;
            }

            public Range(T start, T end)
            {
                Start = start;
                End = end;
            }

            public bool Contains(Range other)
            {
                return Start <= other.Start && other.End <= End;
            }

            public bool Equals(Range other)
            {
                return Start == other.Start && End == other.End;
            }

            public static implicit operator Range((T Start, T End) tuple)
            {
                return new Range(tuple.Start, tuple.End);
            }
        }

        private static T Previous(T value)
        {
            var _value = value;
            return --_value;
        }
        private static T Next(T value)
        {
            var _value = value;
            return ++_value;
        }

        public Interval()
        {
            _ranges = new();
        }
        public Interval(Interval<T> interval)
        {
            static int GetInitCapacity(int x)
            {
                for (var i = 1; i <= sizeof(int) * 8; i *= 2)
                {
                    x |= (x >> i);
                }
                return x < int.MaxValue ? x + 1 : int.MaxValue;
            }

            var count = interval._ranges.Count;
            if (count < 4)
            {
                _ranges = new();
            }
            else
            {
                var capacity = GetInitCapacity(count);
                _ranges = new(capacity);
            }
            _ranges.AddRange(interval._ranges);
        }

        private bool _normalized;
        private readonly List<Range> _ranges;
        public IReadOnlyCollection<Range> Items => _ranges;

        private int RangeComparison(Range x, Range y)
        {
            return x.Start < y.Start ? -1 : x.Start > y.Start ? 1 : 0;
        }

        public bool Contains(Interval<T> other)
        {
            if (!_normalized) Normalize();
            if (!other._normalized) other.Normalize();

            var first = _ranges.GetEnumerator();
            var second = other._ranges.GetEnumerator();

            if (!second.MoveNext()) return true;
            if (!first.MoveNext()) return false;

            for (; ; )
            {
                if (first.Current.End < second.Current.Start)
                {
                    if (!first.MoveNext()) return false;
                    continue;
                }

                if (!first.Current.Contains(second.Current)) return false;
                if (!second.MoveNext()) return true;
            }
        }

        public void Add(T value) => Add(new Range(value, value));
        public void Add(Range range)
        {
            _ranges.Add(range);
            _normalized = false;
        }

        public void Add(IEnumerable<Range> ranges)
        {
            foreach (var range in ranges)
            {
                _ranges.Add(range);
            }
            _normalized = false;
        }

        public void Add(Interval<T> interval)
        {
            foreach (var range in interval)
            {
                _ranges.Add(range);
            }
            _normalized = false;
        }

        public void Subtract(T value) => Subtract(new Range(value, value));
        public void Subtract(Range range)
        {
            if (!_normalized) Normalize();

            for (var i = 0; i < _ranges.Count;)
            {
                var current = _ranges[i];

                if (range.Start <= current.Start)
                {
                    if (range.End < current.Start)
                    {
                        i++;
                        continue;
                    }

                    if (range.End < current.End)
                    {
                        _ranges[i] = new Range(Next(range.End), current.End);
                        return;
                    }
                    else
                    {
                        _ranges.RemoveAt(i);
                        if (range.End == current.End)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    if (range.End < current.End)
                    {
                        _ranges[i] = new Range(current.Start, Previous(range.Start));
                        _ranges.Add(new Range(Next(range.End), current.End));
                        _normalized = false;
                        return;
                    }
                    else if (range.Start <= current.End)
                    {
                        _ranges[i] = new Range(current.Start, Previous(range.Start));
                        i++;

                        if (range.End == current.End)
                        {
                            return;
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public void Subtract(IEnumerable<Range> ranges)
        {
            Subtract(new Interval<T> { ranges });
        }

        public void Subtract(Interval<T> interval)
        {
            if (!interval._normalized) Normalize();

            foreach (var range in interval._ranges)
            {
                Subtract(range);
            }
        }

        public void Normalize()
        {
            _ranges.Sort(RangeComparison);
            for (var i = 0; i < _ranges.Count - 1;)
            {
                var current = _ranges[i];
                var follow = _ranges[i + 1];

                if (Previous(follow.Start) <= current.End)
                {
                    var start = current.Start < follow.Start ? current.Start : follow.Start;
                    var end = current.End > follow.End ? current.End : follow.End;
                    _ranges[i] = new Range(current.Start, end);
                    _ranges.RemoveAt(i + 1);
                }
                else
                {
                    i++;
                }
            }
            _normalized = true;
        }

        public IEnumerator<Range> GetEnumerator()
        {
            if (!_normalized) Normalize();
            return _ranges.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals(Interval<T> other)
        {
            if (other is null) return false;

            if (!_normalized) Normalize();
            if (!other._normalized) other.Normalize();

            if (Items.Count != other.Items.Count) return false;

            var first = Items.GetEnumerator();
            var second = other.Items.GetEnumerator();

            while (first.MoveNext())
            {
                second.MoveNext();
                if (!first.Current.Equals(second.Current)) return false;
            }

            return true;
        }

        public static Interval<T> operator +(Interval<T> left, T right)
        {
            return new Interval<T>(left) { right };
        }

        public static Interval<T> operator +(Interval<T> left, Range right)
        {
            return new Interval<T>(left) { right };
        }

        public static Interval<T> operator +(Interval<T> left, Interval<T> right)
        {
            return new Interval<T>(left) { right };
        }

        public static Interval<T> operator -(Interval<T> left, T right)
        {
            var instance = new Interval<T>(left);
            instance.Subtract(right);
            return instance;
        }

        public static Interval<T> operator -(Interval<T> left, Range right)
        {
            var instance = new Interval<T>(left);
            instance.Subtract(right);
            return instance;
        }

        public static Interval<T> operator -(Interval<T> left, Interval<T> right)
        {
            var instance = new Interval<T>(left);
            instance.Subtract(right);
            return instance;
        }

    }

}
#endif
