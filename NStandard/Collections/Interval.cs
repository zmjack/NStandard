﻿#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET45_OR_GREATER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
#if NET7_0_OR_GREATER
using System.Numerics;
#else
using static NStandard.Dynamic;
#endif

namespace NStandard.Collections;

public class Interval<T> : IEnumerable<Interval<T>.Range>, IEquatable<Interval<T>>
#if NET7_0_OR_GREATER
    ,
    IAdditionOperators<Interval<T>, T, Interval<T>>,
    IAdditionOperators<Interval<T>, Interval<T>.Range, Interval<T>>,
    IAdditionOperators<Interval<T>, Interval<T>, Interval<T>>,
    ISubtractionOperators<Interval<T>, T, Interval<T>>,
    ISubtractionOperators<Interval<T>, Interval<T>.Range, Interval<T>>,
    ISubtractionOperators<Interval<T>, Interval<T>, Interval<T>>
    where T : IComparisonOperators<T, T, bool>, IDecrementOperators<T>, IIncrementOperators<T>
#endif
{
    [DebuggerDisplay("({Start}, {End})")]
    public struct Range(T start, T end) : IEquatable<Range>
    {
        public T Start
        {
            get; set;
        } = start;
        public T End
        {
            get; set;
        } = end;

        public bool Contains(Range other)
        {
#if NET7_0_OR_GREATER
            return Start <= other.Start && other.End <= End;
#else 
            return OpLessThanOrEqual(Start, other.Start) && OpLessThanOrEqual(other.End, End);
#endif
        }

        public bool Equals(Range other)
        {
#if NET7_0_OR_GREATER
            return Start == other.Start && End == other.End;
#else
            return OpEqual(Start, other.Start) && OpEqual(End, other.End);
#endif
        }

#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET47_OR_GREATER
        public static implicit operator Range((T Start, T End) tuple)
        {
            return new Range(tuple.Start, tuple.End);
        }

        public override bool Equals(object? obj)
        {
            return obj is Range range && Equals(range);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
#endif
    }

    private bool _normalized;
    private readonly List<Range> _ranges;
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET451_OR_GREATER
    public IReadOnlyCollection<Range> Ranges => _ranges;
#else
    public ICollection<Range> Ranges => _ranges;
#endif

    public int Count => Ranges.Count;

    public Interval()
    {
        _ranges = [];
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
            _ranges = [];
        }
        else
        {
            var capacity = GetInitCapacity(count);
            _ranges = new(capacity);
        }
        _ranges.AddRange(interval._ranges);
    }

    private static T Previous(T value)
    {
        var _value = value;
#if NET7_0_OR_GREATER
        return --_value;
#else
        return OpDecrement(value);
#endif
    }
    private static T Next(T value)
    {
        var _value = value;
#if NET7_0_OR_GREATER
        return ++_value;
#else
        return OpIncrement(value);
#endif
    }

    private int CompareRange(Range x, Range y)
    {
#if NET7_0_OR_GREATER
        return x.Start < y.Start ? -1 : x.Start > y.Start ? 1 : 0;
#else
        return OpLessThan(x.Start, y.Start) ? -1 : OpGreaterThan(x.Start, y.Start) ? 1 : 0;
#endif
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
#if NET7_0_OR_GREATER
            if (first.Current.End < second.Current.Start)
#else
            if (OpLessThan(first.Current.End, second.Current.Start))
#endif
            {
                if (!first.MoveNext()) return false;
                continue;
            }

            if (!first.Current.Contains(second.Current)) return false;
            if (!second.MoveNext()) return true;
        }
    }

    public void Add(T value)
    {
        Add(new Range(value, value));
    }

    public void Add(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            Add(new Range(value, value));
        }
    }

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

    public void Subtract(T value)
    {
        Subtract(new Range(value, value));
    }

    public void Subtract(IEnumerable<T> values)
    {
        foreach (var value in values)
        {
            Subtract(new Range(value, value));
        }
    }

    public void Subtract(Range range)
    {
        if (!_normalized) Normalize();

        for (var i = 0; i < _ranges.Count;)
        {
            var current = _ranges[i];

#if NET7_0_OR_GREATER
            if (range.Start <= current.Start)
#else
            if (OpLessThanOrEqual(range.Start, current.Start))
#endif
            {
#if NET7_0_OR_GREATER
                if (range.End < current.Start)
#else
                if (OpLessThan(range.End, current.Start))
#endif
                {
                    i++;
                    continue;
                }

#if NET7_0_OR_GREATER
                if (range.End < current.End)
#else
                if (OpLessThan(range.End, current.End))
#endif
                {
                    _ranges[i] = new Range(Next(range.End), current.End);
                    return;
                }
                else
                {
                    _ranges.RemoveAt(i);
#if NET7_0_OR_GREATER
                    if (range.End == current.End)
#else
                    if (OpEqual(range.End, current.End))
#endif
                    {
                        return;
                    }
                }
            }
            else
            {
#if NET7_0_OR_GREATER
                if (range.End < current.End)
#else
                if (OpLessThan(range.End, current.End))
#endif
                {
                    _ranges[i] = new Range(current.Start, Previous(range.Start));
                    _ranges.Add(new Range(Next(range.End), current.End));
                    _normalized = false;
                    return;
                }
#if NET7_0_OR_GREATER
                else if (range.Start <= current.End)
#else
                else if (OpLessThanOrEqual(range.Start, current.End))
#endif
                {
                    _ranges[i] = new Range(current.Start, Previous(range.Start));
                    i++;

#if NET7_0_OR_GREATER
                    if (range.End == current.End)
#else
                    if (OpEqual(range.End, current.End))
#endif
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
        _ranges.Sort(CompareRange);
        for (var i = 0; i < _ranges.Count - 1;)
        {
            var current = _ranges[i];
            var follow = _ranges[i + 1];

#if NET7_0_OR_GREATER
            if (Previous(follow.Start) <= current.End)
#else
            if (OpLessThanOrEqual(Previous(follow.Start), current.End))
#endif
            {

#if NET7_0_OR_GREATER
                var start = current.Start < follow.Start ? current.Start : follow.Start;
                var end = current.End > follow.End ? current.End : follow.End;
#else
                var start = OpLessThan(current.Start, follow.Start) ? current.Start : follow.Start;
                var end = OpGreaterThan(current.End, follow.End) ? current.End : follow.End;
#endif
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

    public bool Equals(Interval<T>? other)
    {
        if (other is null) return false;

        if (!_normalized) Normalize();
        if (!other._normalized) other.Normalize();

        if (Ranges.Count != other.Ranges.Count) return false;

        var first = Ranges.GetEnumerator();
        var second = other.Ranges.GetEnumerator();

        while (first.MoveNext())
        {
            second.MoveNext();
            if (!Equals(first.Current, second.Current)) return false;
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

    public override bool Equals(object? obj)
    {
        return obj is Interval<T> interval && Equals(interval);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
#endif
