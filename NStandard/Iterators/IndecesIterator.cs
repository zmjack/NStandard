using System.Collections;

namespace NStandard.Iterators;

public class IndecesIterator : IEnumerable<int[]>
{
    public int Rank { get; private set; }
    private int[] _lengths;
    public int[] Lengths
    {
        get => _lengths;
        set
        {
            _lengths = value;
            Rank = _lengths.Length;
        }
    }
    public int Skip { get; set; }

    public IndecesIterator(int[] lengths) : this(lengths, 0) { }
    public IndecesIterator(int[] lengths, int skip)
    {
        if (lengths is null) throw new ArgumentNullException(nameof(lengths));
        if (lengths.Any(x => x <= 0)) throw new ArgumentException("All lengths must be greater than 0.", nameof(lengths));

        Skip = skip;
        _lengths = lengths;
        Rank = lengths.Length;
    }

    public virtual bool Normalize(int index, int value, ref int[] current)
    {
        if (value >= Lengths[index])
        {
            if (index == 0) return false;

            var prevIndex = index - 1;
            current[index] = value % Lengths[index];
            current[prevIndex] += value / Lengths[index];
            return Normalize(prevIndex, current[prevIndex], ref current);
        }
        else return true;
    }

    public IEnumerator<int[]> GetEnumerator()
    {
        var current = new int[Rank];
        var index = Rank - 1;
        var value = Skip;
        current[index] = value;

        Normalize(index, value, ref current);
        yield return current;

        bool MoveNext()
        {
            var index = Rank - 1;
            var value = current[index] + 1;
            current[index] = value;
            return Normalize(index, value, ref current);
        }

        while (MoveNext())
        {
            yield return current;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
