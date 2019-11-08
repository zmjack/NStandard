using System.ComponentModel;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XChar
    {
        /// <summary>
        /// Gets the length of the specified char as Ascii.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int GetLengthA(this char @this) => @this < 0x100 ? 1 : 2;

    }
}
