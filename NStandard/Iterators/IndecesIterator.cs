using System.Collections;

namespace NStandard.Iterators;

public class IndecesIterator : IEnumerable<int[]>
{
    public int Dimensions { get; private set; }
    public int[] Lengths { get; private set; }
    public int Skip { get; private set; }
    private readonly int[] current;
    public int[] Current => current;

    public IndecesIterator(int[] lengths) : this(lengths, 0) { }
    public IndecesIterator(int[] lengths, int skip)
    {
        if (skip < 0) throw new ArgumentException("Skip must be greater than or equal to 0.", nameof(skip));
        if (lengths.Any(x => x <= 0)) throw new ArgumentException("All lengths must be greater than 0.", nameof(lengths));

        var dimensions = lengths.Length;
        Skip = skip;
        Dimensions = dimensions;
        Lengths = lengths;
        current = new int[dimensions];
    }

    protected virtual bool Normalize(int index, int value)
    {
        if (value >= Lengths[index])
        {
            if (index == 0) return false;
            var prevIndex = index - 1;
            current[index] = value % Lengths[index];
            current[prevIndex] += value / Lengths[index];
            return Normalize(prevIndex, current[prevIndex]);
        }
        else return true;
    }

    protected virtual bool MoveNext()
    {
        var index = Dimensions - 1;
        var value = current[index] + 1;
        current[index] = value;
        return Normalize(index, value);
    }

    public IEnumerator<int[]> GetEnumerator()
    {
        Array.Clear(current, 0, current.Length);
        var index = Dimensions - 1;
        var value = Skip;
        current[index] = value;

        if (Normalize(index, value))
        {
            yield return current;
            while (MoveNext())
            {
                yield return current;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
