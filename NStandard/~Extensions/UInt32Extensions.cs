﻿using System;
using System.ComponentModel;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class UInt32Extensions
{
    /// <summary>
    /// Returns whether the specified number is odd.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    [Obsolete("Use (@this & 1) == 1 instead.")]
    public static bool IsOdd(this uint @this) => (@this & 1) == 1;

    /// <summary>
    /// Returns whether the specified number is even.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    [Obsolete("Use (@this & 1) == 0 instead.")]
    public static bool IsEven(this uint @this) => (@this & 1) == 0;

    /// <summary>
    /// Gets the positive integer modulus. (Unlike the operator %, this method always returns a positive number)
    /// </summary>
    /// <param name="this"></param>
    /// <param name="mod"></param>
    /// <returns></returns>
    public static uint Mod(this uint @this, uint mod)
    {
        if (@this < 0)
            return @this % mod + mod;
        else return @this % mod;
    }

}
