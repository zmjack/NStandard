using System;
using System.Linq;
using System.Text;

namespace NStandard.Converts
{
    public class BytesConvert
    {
        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        ///     that is encoded with base-64 digits.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string Base64(byte[] bytes) => Convert.ToBase64String(bytes);

        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        ///     that is encoded with url safe base-64 digits.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string UrlSafeBase64(byte[] bytes) => StringConvert.ConvertBase64ToUrlSafeBase64(Convert.ToBase64String(bytes));

        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        ///     that is encoded with hex digits.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes) => HexString(bytes, "");

        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        ///     that is encoded with hex digits.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string HexString(byte[] bytes, string separator) => string.Join(separator, bytes.Select(x => x.ToString("x2")));

        /// <summary>
        /// Decodes all the bytes in the specified byte(UTF-8) array into a string.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string String(byte[] bytes) => String(bytes, Encoding.UTF8);

        /// <summary>
        /// Decodes all the bytes in the specified byte array into a string.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string String(byte[] bytes, string encoding) => Encoding.GetEncoding(encoding).GetString(bytes);

        /// <summary>
        /// Decodes all the bytes in the specified byte array into a string.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string String(byte[] bytes, Encoding encoding) => encoding.GetString(bytes);
    }
}
