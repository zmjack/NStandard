using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NStandard;

internal class IndicesStepper : IEnumerable<int[]>
{
    public int Rank { get; set; }
    public int[] Lengths { get; set; }
    public int Offset { get; set; }

    public IndicesStepper(int offset, int[] lengths)
    {
        if (lengths.Any(x => x <= 0)) throw new ArgumentException("All lengths must be greater than 0.", nameof(lengths));

        Offset = offset;
        Rank = lengths.Length;
        Lengths = lengths;
    }

    public IEnumerator<int[]> GetEnumerator()
    {
        var current = new int[Rank];
        var index = Rank - 1;
        var value = Offset;
        current[index] = value;

        Normalize(index, value);
        yield return current;

        bool MoveNext()
        {
            var index = Rank - 1;
            var value = current[index] + 1;
            current[index] = value;
            return Normalize(index, value);
        }

        bool Normalize(int index, int value)
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
