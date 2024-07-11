using System;
using System.ComponentModel;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class UInt64Extensions
{
    /// <summary>
    /// Gets the positive integer modulus. (Unlike the operator %, this method always returns a positive number)
    /// </summary>
    /// <param name="this"></param>
    /// <param name="mod"></param>
    /// <returns></returns>
    public static ulong Mod(this ulong @this, ulong mod)
    {
        if (@this < 0)
            return @this % mod + mod;
        else return @this % mod;
    }

}
