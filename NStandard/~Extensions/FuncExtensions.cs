using System;
using System.ComponentModel;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class FuncExtensions
{
    /// <summary>
    /// Takes two functions and returns a function.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TCurrent"></typeparam>
    /// <typeparam name="TRet"></typeparam>
    public static Func<T, TRet> Compose<T, TCurrent, TRet>(this Func<T, TCurrent> @this, Func<TCurrent, TRet> pipe)
    {
        return x => pipe(@this(x));
    }
}
