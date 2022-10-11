using System;
using System.Linq;

namespace NStandard.Collections
{
    public class ArrayVisitor<T>
    {
        private static string SourceTypeNotSame(Type specifiedElementType) => $"Any element of source must be ${specifiedElementType}.";
        private static string StartIndicesLengthMustBeLessThanRank() => $"The length of start indices must be less than rank.";
        private static string FlattenedIndexMustBeGreaterThanZero() => $"The flattened index must be greater than 0.";
        private static string FlattenedIndexMustBeLessThanSequenceLength(int sequenceLength) => $"The flattened index must be less than {sequenceLength}.";

        public Array Source { get; }
        public int[] Lengths { get; }
        public int SequenceLength { get; }

        public ArrayVisitor(Array source)
        {
            var elementType = source.GetType().GetElementType();
            var specifiedElementType = typeof(T);
            if (!elementType.IsType(specifiedElementType)) throw new ArgumentException(SourceTypeNotSame(specifiedElementType), nameof(source));

            Source = source;
            Lengths = source.GetLengths();
            SequenceLength = source.GetSequenceLength();
        }

        private int[] GetIndices(int flattenedIndex)
        {
            if (flattenedIndex < 0) throw new ArgumentException(FlattenedIndexMustBeGreaterThanZero(), nameof(flattenedIndex));

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

        public void SetValue(T value, int flattenedIndex)
        {
            if (flattenedIndex < 0) throw new ArgumentException(FlattenedIndexMustBeGreaterThanZero(), nameof(flattenedIndex));
            if (flattenedIndex >= SequenceLength) throw new ArgumentException(FlattenedIndexMustBeLessThanSequenceLength(SequenceLength), nameof(flattenedIndex));

            var indeces = GetIndices(flattenedIndex);
            Source.SetValue(value, indeces);
        }

        public T GetValue(int flattenedIndex)
        {
            if (flattenedIndex < 0) throw new ArgumentException(FlattenedIndexMustBeGreaterThanZero(), nameof(flattenedIndex));
            if (flattenedIndex >= SequenceLength) throw new ArgumentException(FlattenedIndexMustBeLessThanSequenceLength(SequenceLength), nameof(flattenedIndex));

            var indeces = GetIndices(flattenedIndex);
            return (T)Source.GetValue(indeces);
        }

        public void Assign(Array destination, int[] startIndices, T[] source, int sourceIndex, int length)
        {
            if (startIndices.Length >= Source.Rank) new ArgumentException(StartIndicesLengthMustBeLessThanRank(), nameof(startIndices));


        }

    }
}
