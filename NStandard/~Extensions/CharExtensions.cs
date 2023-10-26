using System.ComponentModel;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class CharExtensions
{
    /// <summary>
    /// Gets the length of the specified char as Ascii.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static int GetLengthA(this char @this) => @this < 0x100 ? 1 : 2;

    /// <summary>
    /// Converts the value of a Unicode character to its lowercase equivalent.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static char ToLower(this char @this) => char.ToLower(@this);

    /// <summary>
    /// Converts the value of a Unicode character to its uppercase equivalent.
    /// </summary>
    /// <param name="thsi"></param>
    /// <returns></returns>
    public static char ToUpper(this char @this) => char.ToUpper(@this);

}
