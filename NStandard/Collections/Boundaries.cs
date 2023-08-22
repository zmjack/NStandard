#if NET7_0_OR_GREATER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace NStandard
{
    [Obsolete("This feature is experimental and may change in the future.")]
    public class Int32Boundaries : Boundaries<int>
    {
        public Int32Boundaries() { }
        internal override int Previous(int value) => value - 1;
        internal override int Next(int value) => value + 1;

        public static Int32Boundaries Create(IOrderedEnumerable<int> numbers)
        {
            var boundaries = new Int32Boundaries();
            foreach (var item in InnerGetBoundaries(numbers, boundaries.Next))
            {
                boundaries.Add(item);
            }
            return boundaries;
        }
    }

    [Obsolete("This feature is experimental and may change in the future.")]
    public class UInt32Boundaries : Boundaries<uint>
    {
        public UInt32Boundaries() { }
        internal override uint Previous(uint value) => value - 1;
        internal override uint Next(uint value) => value + 1;

        public static UInt32Boundaries Create(IOrderedEnumerable<uint> numbers)
        {
            var boundaries = new UInt32Boundaries();
            foreach (var item in InnerGetBoundaries(numbers, boundaries.Next))
            {
                boundaries.Add(item);
            }
            return boundaries;
        }
    }

    [Obsolete("This feature is experimental and may change in the future.")]
    public class Int64Boundaries : Boundaries<long>
    {
        public Int64Boundaries() { }
        internal override long Previous(long value) => value - 1;
        internal override long Next(long value) => value + 1;

        public static Int64Boundaries Create(IOrderedEnumerable<long> numbers)
        {
            var boundaries = new Int64Boundaries();
            foreach (var item in InnerGetBoundaries(numbers, boundaries.Next))
            {
                boundaries.Add(item);
            }
            return boundaries;
        }
    }

    [Obsolete("This feature is experimental and may change in the future.")]
    public class UInt64Boundaries : Boundaries<ulong>
    {
        public UInt64Boundaries() { }
        internal override ulong Previous(ulong value) => value - 1;
        internal override ulong Next(ulong value) => value + 1;

        public static UInt64Boundaries Create(IOrderedEnumerable<ulong> numbers)
        {
            var boundaries = new UInt64Boundaries();
            foreach (var item in InnerGetBoundaries(numbers, boundaries.Next))
            {
                boundaries.Add(item);
            }
            return boundaries;
        }
    }

    [Obsolete("This feature is experimental and may change in the future.")]
    public abstract class Boundaries<T> : IEnumerable<(T Start, T End)>
        where T : IComparisonOperators<T, T, bool>
    {
        private readonly List<T> _list = new();

        internal abstract T Previous(T value);
        internal abstract T Next(T value);

        protected Boundaries() { }

        protected static IEnumerable<(T Start, T End)> InnerGetBoundaries(IOrderedEnumerable<T> numbers, Func<T, T> next)
        {
            if (numbers.Any())
            {
                bool hasStart = false;

                T start = default;
                T end = default;
                var enumerator = numbers.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    var current = enumerator.Current;
                    if (!hasStart)
                    {
                        hasStart = true;
                        start = current;
                        end = current;
                    }
                    else if (current.Equals(end))
                    {
                        continue;
                    }
                    else if (current.Equals(next(end)))
                    {
                        end = current;
                    }
                    else
                    {
                        yield return (start, end);
                        start = current;
                        end = current;
                    }
                }

                yield return (start, end);
            }
        }

        public void Add((T Start, T End) item)
        {
            if (item.Start > item.End) return;

            if (_list.Any())
            {
                int startLIndex = 0;
                int endLIndex = _list.Count - 1;
                bool? startInside = null, endInside = null;

                foreach (var (index, chunk) in _list.Chunk(2).AsIndexValuePairs())
                {
                    if (startInside is null && chunk[0] <= item.Start && item.Start <= chunk[1])
                    {
                        startLIndex = index * 2;
                        startInside = true;
                    }

                    if (endInside is null && chunk[0] <= item.End && item.End <= chunk[1])
                    {
                        endLIndex = index * 2;
                        endInside = true;
                    }
                }

                foreach (var (index, chunk) in _list.Slide(2, true).AsIndexValuePairs())
                {
                    if ((index & 1) == 0) continue;

                    if (startInside is null && chunk[0] <= item.Start && item.Start <= chunk[1])
                    {
                        startLIndex = index;
                        startInside = false;
                    }

                    if (endInside is null && chunk[0] <= item.End && item.End <= chunk[1])
                    {
                        endLIndex = index;
                        endInside = false;
                    }
                }

                if (startInside == true)
                {
                    if (endInside == true)
                    {
                        _list.RemoveRange(startLIndex + 1, endLIndex - startLIndex);
                    }
                    else if (endInside == false)
                    {
                        _list[endLIndex] = item.End;
                        _list.RemoveRange(startLIndex + 1, endLIndex - startLIndex - 1);
                    }
                    else
                    {
                        _list[endLIndex] = item.End;
                        _list.RemoveRange(startLIndex + 1, endLIndex - startLIndex - 1);
                    }
                }
                else if (startInside == false)
                {
                    if (endInside == true)
                    {
                        _list[endLIndex] = item.Start;
                        _list.RemoveRange(startLIndex + 1, endLIndex - startLIndex - 1);
                    }
                    else if (endInside == false)
                    {
                        _list[endLIndex] = item.End;
                        _list[endLIndex - 1] = item.Start;
                        _list.RemoveRange(startLIndex + 1, endLIndex - startLIndex - 2);
                    }
                    else
                    {
                        _list[endLIndex] = item.End;
                        _list[endLIndex - 1] = item.Start;
                        _list.RemoveRange(startLIndex + 1, endLIndex - startLIndex - 2);
                    }
                }
                else
                {
                    if (endInside == true)
                    {
                        _list[endLIndex] = item.Start;
                        _list.RemoveRange(0, endLIndex);
                    }
                    else if (endInside == false)
                    {
                        _list[endLIndex] = item.End;
                        _list[endLIndex - 1] = item.Start;
                        _list.RemoveRange(0, endLIndex - 1);
                    }
                    else
                    {
                        var first = _list.First();
                        if (item.End < first)
                        {
                            if (Next(item.End).Equals(first))
                            {
                                _list[0] = item.Start;
                            }
                            else
                            {
                                _list.Add(item.Start);
                                _list.Add(item.End);
                                _list.Sort();
                            }
                        }
                        else
                        {
                            var last = _list[endLIndex];
                            if (item.Start > last)
                            {
                                if (Previous(item.Start).Equals(last))
                                {
                                    _list[endLIndex] = item.End;
                                }
                                else
                                {
                                    _list.Add(item.Start);
                                    _list.Add(item.End);
                                }
                            }
                            else
                            {
                                _list.Clear();
                                _list.Add(item.Start);
                                _list.Add(item.End);
                            }
                        }
                    }
                }
            }
            else
            {
                _list.Add(item.Start);
                _list.Add(item.End);
            }
        }

        public void Subtract((T Start, T End) item)
        {
            if (item.Start > item.End) return;

            if (_list.Any())
            {
                int startLIndex = -1;
                int endLIndex = _list.Count - 1;
                bool? startInside = null, endInside = null;

                foreach (var (index, chunk) in _list.Chunk(2).AsIndexValuePairs())
                {
                    if (startInside is null && chunk[0] <= item.Start && item.Start <= chunk[1])
                    {
                        startLIndex = index * 2;
                        startInside = true;
                    }

                    if (endInside is null && chunk[0] <= item.End && item.End <= chunk[1])
                    {
                        endLIndex = index * 2;
                        endInside = true;
                    }
                }

                foreach (var (index, chunk) in _list.Slide(2, true).AsIndexValuePairs())
                {
                    if ((index & 1) == 0) continue;

                    if (startInside is null && chunk[0] <= item.Start && item.Start <= chunk[1])
                    {
                        startLIndex = index;
                        startInside = false;
                    }

                    if (endInside is null && chunk[0] <= item.End && item.End <= chunk[1])
                    {
                        endLIndex = index;
                        endInside = false;
                    }
                }

                if (startInside == true && endInside == true && startLIndex == endLIndex)
                {
                    var removeCount = 0;
                    if (_list[startLIndex] == item.Start)
                    {
                        removeCount++;
                    }
                    else
                    {
                        _list.Add(Previous(item.Start));
                    }

                    if (_list[endLIndex + 1] == item.End)
                    {
                        removeCount++;
                    }
                    else
                    {
                        _list.Add(Next(item.End));
                    }

                    _list.RemoveRange(startLIndex, removeCount);
                    _list.Sort();
                }
                else
                {
                    if (startInside == true)
                    {
                        var from = startLIndex;
                        var adjust = 0;

                        if (endInside == true)
                        {
                            if (_list[startLIndex] == item.Start)
                            {
                                adjust++;
                            }
                            else
                            {
                                adjust--;
                                from += 2;
                                _list[startLIndex + 1] = Previous(item.Start);
                            }

                            if (_list[endLIndex + 1] == item.End)
                            {
                                adjust++;
                            }
                            else
                            {
                                adjust--;
                                _list[endLIndex] = Next(item.End);
                            }

                            _list.RemoveRange(from, endLIndex - startLIndex + adjust);
                        }
                        else if (endInside == false)
                        {
                            if (_list[startLIndex] == item.Start)
                            {
                                adjust++;
                            }
                            else
                            {
                                adjust--;
                                from += 2;
                                _list[startLIndex + 1] = Previous(item.Start);
                            }

                            _list.RemoveRange(from, endLIndex - startLIndex + adjust);
                        }
                        else
                        {
                            if (_list[startLIndex] == item.Start)
                            {
                                adjust++;
                            }
                            else
                            {
                                adjust--;
                                from += 2;
                                _list[startLIndex + 1] = Previous(item.Start);
                            }

                            _list.RemoveRange(from, endLIndex - startLIndex + adjust);
                        }
                    }
                    else if (startInside == false)
                    {
                        var from = startLIndex + 1;
                        var adjust = 0;

                        if (endInside == true)
                        {
                            if (_list[endLIndex + 1] == item.End)
                            {
                                adjust++;
                            }
                            else
                            {
                                adjust--;
                                _list[endLIndex] = Next(item.End);
                            }

                            _list.RemoveRange(from, endLIndex - startLIndex + adjust);
                        }
                        else if (endInside == false)
                        {
                            _list.RemoveRange(from, endLIndex - startLIndex);
                        }
                        else
                        {
                            _list.RemoveRange(from, endLIndex - startLIndex);
                        }
                    }
                    else
                    {
                        var from = startLIndex + 1;
                        var adjust = 0;

                        if (endInside == true)
                        {
                            if (_list[endLIndex + 1] == item.End)
                            {
                                adjust++;
                            }
                            else
                            {
                                adjust--;
                                _list[endLIndex] = Next(item.End);
                            }

                            _list.RemoveRange(from, endLIndex - startLIndex + adjust);
                        }
                        else if (endInside == false)
                        {
                            _list.RemoveRange(from, endLIndex - startLIndex);
                        }
                        else
                        {
                            var first = _list.First();
                            var last = _list[endLIndex];

                            if (item.Start < first && last < item.End)
                            {
                                _list.Clear();
                            }
                        }
                    }
                }
            }
            else
            {
            }
        }

        private IEnumerable<(T Start, T End)> GetEnumerable()
        {
            foreach (var chunk in _list.Chunk(2))
            {
                yield return (chunk[0], chunk[1]);
            }
        }

        public IEnumerator<(T Start, T End)> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        public static Boundaries<T> operator +(Boundaries<T> @this, (T Start, T End) other)
        {
            @this.Add(other);
            return @this;
        }

        public static Boundaries<T> operator -(Boundaries<T> @this, (T Start, T End) other)
        {
            @this.Subtract(other);
            return @this;
        }
    }
}
#endif
