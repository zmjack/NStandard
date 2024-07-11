﻿using System;
using System.ComponentModel;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class UInt32Extensions
{
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
