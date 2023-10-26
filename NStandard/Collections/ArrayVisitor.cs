using System;
using System.Collections.Generic;

namespace NStandard.Collections;

public class ArrayVisitor
{
    private static ArgumentException Exception_FlattenedIndexMustBeGreaterThanZero(string paramName) => new($"The flattened index must be greater than 0.", paramName);
    private static ArgumentException Exception_FlattenedIndexMustBeLessThanSequenceLength(int sequenceLength, string paramName) => new($"The flattened index must be less than {sequenceLength}.", paramName);

    public Array Source { get; }
    public int[] Lengths { get; }
    public int SequenceLength { get; }

    public ArrayVisitor(Array source)
    {
        Source = source;
        Lengths = source.GetLengths();
        SequenceLength = source.GetSequenceLength();
    }

    private int[] GetIndices(int flattenedIndex)
    {
        if (flattenedIndex < 0) throw Exception_FlattenedIndexMustBeGreaterThanZero(nameof(flattenedIndex));

        var rank = Source.Rank;
        var indexArray = new int[rank];

        for (int i = rank - 1; i >= 0; i--)
        {
            var length = Lengths[i];
            indexArray[i] = flattenedIndex % length;
            flattenedIndex /= length;
        }
        return indexArray;
    }

    public int GetFlattenedIndex(int[] indices)
    {
        int flattenedIndex = 0;
        int i = 0;
        foreach (var index in indices)
        {
            flattenedIndex *= Lengths[i];
            flattenedIndex += index;
            i++;
        }
        return flattenedIndex;
    }

    public void SetValue(object value, int flattenedIndex)
    {
        if (flattenedIndex < 0) throw Exception_FlattenedIndexMustBeGreaterThanZero(nameof(flattenedIndex));
        if (flattenedIndex >= SequenceLength) throw Exception_FlattenedIndexMustBeLessThanSequenceLength(SequenceLength, nameof(flattenedIndex));

        var indeces = GetIndices(flattenedIndex);
        Source.SetValue(value, indeces);
    }

    public object GetValue(int flattenedIndex)
    {
        if (flattenedIndex < 0) throw Exception_FlattenedIndexMustBeGreaterThanZero(nameof(flattenedIndex));
        if (flattenedIndex >= SequenceLength) throw Exception_FlattenedIndexMustBeLessThanSequenceLength(SequenceLength, nameof(flattenedIndex));

        var indeces = GetIndices(flattenedIndex);
        return Source.GetValue(indeces);
    }

    public IEnumerable<object> GetValues()
    {
        var stepper = new IndicesStepper(0, Lengths);
        foreach (var indeces in stepper)
        {
            yield return Source.GetValue(indeces);
        }
    }

    public void Assign(int offset, Array source, int sourceIndex, int length)
    {
        var visitor = new ArrayVisitor(source);
        for (int i = 0; i < length; i++)
        {
            SetValue(visitor.GetValue(sourceIndex + i), offset + i);
        }
    }
}

public class ArrayVisitor<T> : ArrayVisitor
{
    public ArrayVisitor(Array source) : base(source)
    {
    }

    public void SetValue(T value, int flattenedIndex)
    {
        base.SetValue(value, flattenedIndex);
    }

    public new T GetValue(int flattenedIndex)
    {
        return (T)base.GetValue(flattenedIndex);
    }
}
