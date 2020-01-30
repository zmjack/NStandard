﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NStandard
{
    internal class DimensionIndexCalculator
    {
        internal readonly int[] Lengths;
        internal readonly long LinearLength;
        private readonly int[] Divisors;

        internal DimensionIndexCalculator(Array arr)
        {
            var rank = arr.Rank;
            Lengths = new int[rank].Let(i => arr.GetLength(i));
            Divisors = new int[rank].Then(x => x[rank - 1] = 1);
            LinearLength = Lengths.Aggregate(1L, (x, y) => x * y);

            for (int di = rank - 2; di >= 0; di--)
                Divisors[di] = Divisors[di + 1] * Lengths[di + 1];
        }

        internal int GetDimensionIndex(int unidimensionalIndex, int dimension)
        {
            return unidimensionalIndex / Divisors[dimension] % Lengths[dimension];
        }

    }
}