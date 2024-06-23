using System;
using System.Text;

namespace NStandard;

public static partial class ArrayExtensions
{
    /// <summary>
    /// Decodes all the bytes in the specified byte(Default: UTF-8) array into a string.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    [Obsolete("Use Pipe(Encoding.Unicode.GetString) instead.", true)]
    public static string String(this byte[] @this) => String(@this, Encoding.Unicode);

    /// <summary>
    /// Decodes all the bytes in the specified byte array into a string.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    [Obsolete("Use Pipe(Encoding.GetEncoding(*).GetString) instead.", true)]
    public static string String(this byte[] @this, string encoding) => Encoding.GetEncoding(encoding).GetString(@this);

    /// <summary>
    /// Decodes all the bytes in the specified byte array into a string.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    [Obsolete("Use Pipe(Encoding.*.GetString) instead.", true)]
    public static string String(this byte[] @this, Encoding encoding) => encoding.GetString(@this);
}
