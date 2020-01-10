using System;

namespace NStandard.Converts
{
    public static class StringConvert
    {
        /// <summary>
        /// Converts the specified string, which encodes binary data as url safe base-64 digits, to
        ///     an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string ConvertBase64ToUrlSafeBase64(string base64)
        {
            return base64.Replace("/", "_").Replace("+", "-").TrimEnd('=');
        }

        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        ///     that is encoded with url safe base-64 digits.
        /// </summary>
        /// <param name="urlBase64"></param>
        /// <returns></returns>
        public static string ConvertUrlSafeBase64ToBase64(string urlBase64)
        {
            var padding = "=".Repeat((urlBase64.Length % 4).For(_ =>
            {
                switch (_)
                {
                    case 0: return 0;
                    case 2: return 2;
                    case 3: return 1;
                    default: throw new FormatException("The input is not a valid Base-64 string");
                }
            }));

            return urlBase64.Replace("_", "/").Replace("-", "+").For(_ => $"{_}{padding}");
        }

        /// <summary>
        /// Converts the specified string, which encodes binary data as base-64 digits, to
        ///     an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] Base64Bytes(string @this) => Convert.FromBase64String(@this);

        /// <summary>
        /// Converts the specified string, which encodes binary data as url safe base-64 digits, to
        ///     an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] UrlSafeBase64Bytes(string @this) => Convert.FromBase64String(ConvertUrlSafeBase64ToBase64(@this));

        /// <summary>
        /// Converts the specified string, which encodes binary data as hex digits, to
        ///     an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static byte[] FromHexString(string @this) => FromHexString(@this, "");

        /// <summary>
        /// Converts the specified string, which encodes binary data as hex digits, to
        ///     an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static byte[] FromHexString(string @this, string separator = "")
        {
            if (@this.IsNullOrEmpty()) return new byte[0];

            var hexString = @this;
            if (!separator.IsNullOrEmpty())
                hexString = hexString.Replace(separator, "");

            var length = hexString.Length;
            if (length.IsOdd())
                throw new FormatException("The specified string's length must be even.");

            var ret = new byte[length / 2];
            for (int i = 0; i < ret.Length; i++)
                ret[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

            return ret;
        }

    }
}
