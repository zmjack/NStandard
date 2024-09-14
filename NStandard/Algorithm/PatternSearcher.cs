namespace NStandard.Algorithm;

/// <summary>
/// Sequence searcher using KMP algorithm.
/// </summary>
public class PatternSearcher<T>
{
    private static ArgumentException NullOrEmptyStringException(string paramName) => new("The pattern string can not be null or empty.", paramName);
    private static ArgumentOutOfRangeException CountOverflowException(string paramName) => new("Count must be positive and count must refer to a location within the string/array/collection.", paramName);
    private static ArgumentOutOfRangeException InvalidStartIndexException(string paramName) => new("Index was out of range. Must be non-negative and less than or equal to the size of the collection.", paramName);

    private readonly T[] _pattern;
    public T[] Pattern => _pattern;

    private readonly int[] _nexts;

    public PatternSearcher(T[] pattern)
    {
        if (!pattern.Any()) throw NullOrEmptyStringException(nameof(pattern));

        _pattern = pattern;

        var length = _pattern.Length;

        var scan = 0;
        var nexts = new int[length];
        nexts[0] = 0;
        for (int i = 1; i < length;)
        {
            if (Equals(_pattern[scan], _pattern[i]))
            {
                scan++;
                nexts[i] = scan;
                i++;
            }
            else
            {
                if (scan == 0)
                {
                    nexts[i] = 0;
                    i++;
                }
                else
                {
                    scan = nexts[scan - 1];
                }
            }
        }

        _nexts = nexts;
    }

    public int Match(T[] array, int startIndex, int count)
    {
        if (startIndex < 0) throw InvalidStartIndexException(nameof(startIndex));

        var stopIndex = startIndex + count;
        if (stopIndex > array.Length) throw CountOverflowException(nameof(count));

        var length = Pattern.Length;
        for (int si = startIndex, pi = 0; si < stopIndex;)
        {
            if (Equals(array[si], _pattern[pi]))
            {
                si++;
                pi++;
            }
            else
            {
                if (pi > 0) pi = _nexts[pi - 1];
                else si++;
            }

            if (pi == length)
            {
                return si - pi;
            }
        }
        return -1;
    }

    public IEnumerable<int> Matches(T[] array, int startIndex, int count)
    {
        if (startIndex < 0) throw InvalidStartIndexException(nameof(startIndex));

        var stopIndex = startIndex + count;
        if (stopIndex > array.Length) throw CountOverflowException(nameof(count));

        var length = Pattern.Length;
        for (int si = startIndex, pi = 0; si < stopIndex;)
        {
            if (Equals(array[si], _pattern[pi]))
            {
                si++;
                pi++;
            }
            else
            {
                if (pi > 0) pi = _nexts[pi - 1];
                else si++;
            }

            if (pi == length)
            {
                var index = si - pi;
                yield return index;

                si = index + 1;
                pi = 0;
            }
        }
    }
}
