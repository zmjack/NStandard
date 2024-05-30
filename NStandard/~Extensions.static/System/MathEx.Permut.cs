﻿// <auto-generated/>
using System;

namespace NStandard;

public static partial class MathEx
{
    /// <summary>
    /// Returns the number of permutations for a given number of items that can be selected.
    /// </summary>
    /// <param name="number"> The number of items. </param>
    /// <param name="chosen"> The number of items in each permutation. </param>
    /// <returns></returns>
    public static int Permut(int number, int chosen)
    {
        if (chosen < 0) throw new ArgumentException("The choice must be greater than zero.", nameof(chosen));
        if (number < chosen) throw new ArgumentException("The total must be greater than or equal to the choice.", nameof(chosen));

        int ret = 1;
        checked
        {
            for (int i = number; i > number - chosen; i--)
            {
                ret *= i;
            }
        }
        return ret;
    }

    /// <summary>
    /// Returns the number of permutations for a given number of items that can be selected.
    /// </summary>
    /// <param name="number"> The number of items. </param>
    /// <param name="chosen"> The number of items in each permutation. </param>
    /// <returns></returns>
    public static long Permut(long number, long chosen)
    {
        if (chosen < 0) throw new ArgumentException("The choice must be greater than zero.", nameof(chosen));
        if (number < chosen) throw new ArgumentException("The total must be greater than or equal to the choice.", nameof(chosen));

        long ret = 1;
        checked
        {
            for (long i = number; i > number - chosen; i--)
            {
                ret *= i;
            }
        }
        return ret;
    }
}

