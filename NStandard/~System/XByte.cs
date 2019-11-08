using System.ComponentModel;
using System.Text;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XByte
    {
        /// <summary>
        /// Decodes all the bytes in the specified byte(UTF-8) array into a string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string String(this byte[] @this) => String(@this, Encoding.UTF8);

        /// <summary>
        /// Decodes all the bytes in the specified byte array into a string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string String(this byte[] @this, string encoding) => Encoding.GetEncoding(encoding).GetString(@this);

        /// <summary>
        /// Decodes all the bytes in the specified byte array into a string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string String(this byte[] @this, Encoding encoding) => encoding.GetString(@this);
    }
}
