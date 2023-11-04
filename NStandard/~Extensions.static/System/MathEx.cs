using System;
using System.ComponentModel;

namespace NStandard;

public static partial class MathEx
{
    /// <summary>
    /// The choice of m things from a set of n things without replacement and where the order matters.
    /// </summary>
    /// <param name="choice">The m value.</param>
    /// <param name="total">The n value.</param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Permut instead. (Signature has been adjusted.)")]
    public static int Permutation(int choice, int total)
    {
        if (choice < 0) throw new ArgumentException("The choice must be greater than zero.", nameof(choice));
        if (total < choice) throw new ArgumentException("The total must be greater than or equal to the choice.", nameof(choice));

        var ret = 1;
        for (int i = total; i > total - choice; i--)
        {
            ret *= i;
        }
        return ret;
    }

    /// <summary>
    /// The choice of m things from a set of n things without replacement and where order does not matter.
    /// </summary>
    /// <param name="choice">The m value.</param>
    /// <param name="total">The n value.</param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Combin instead. (Signature has been adjusted.)")]
    public static int Combination(int choice, int total)
    {
        if (choice < 0) throw new ArgumentException("The choice must be greater than zero.", nameof(choice));
        if (total < choice) throw new ArgumentException("The total must be greater than or equal to the choice.", nameof(choice));

        if (choice == 0 || choice == total) return 1;
        else return Permutation(choice, total) / Permutation(choice, choice);
    }
}
