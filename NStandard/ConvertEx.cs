using System;
using System.Text;

namespace NStandard
{
    public static class ConvertEx
    {
        private const string BASE58_CHARS = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        private static readonly int[] Base58Map =
        {
            -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
            -1,  0,  1,  2,  3,  4,  5,  6,   7,  8, -1, -1, -1, -1, -1, -1,
            -1,  9, 10, 11, 12, 13, 14, 15,  16, -1, 17, 18, 19, 20, 21, -1,
            22, 23, 24, 25, 26, 27, 28, 29,  30, 31, 32, -1, -1, -1, -1, -1,
            -1, 33, 34, 35, 36, 37, 38, 39,  40, 41, 42, 43, -1, 44, 45, 46,
            47, 48, 49, 50, 51, 52, 53, 54,  55, 56, 57, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
            -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
        };

        /// <summary>
        /// Returns an object of the specified type and whose value is equivalent to the specified object.
        ///     (Enhance: Support Nullable types.)
        /// </summary>
        /// <param name="value">An object that implements the System.IConvertible interface.</param>
        /// <param name="conversionType">The type of object to return.</param>
        public static object ChangeType(object value, Type conversionType)
        {
            if (conversionType.IsNullable())
            {
                if (value is null) return null;
                else return Convert.ChangeType(value, conversionType.GetGenericArguments()[0]);
            }
            else return Convert.ChangeType(value, conversionType);
        }

        public static string ToBase58String(byte[] bytes)
        {
            //Refer: https://github.com/bitcoin/bitcoin/blob/master/src/base58.cpp

            // Skip & count leading zeroes.
            int zeroCount = 0;
            int length = 0;
            foreach (var ch in bytes)
            {
                if (ch == 0) zeroCount++;
                else break;
            }

            // Allocate enough space in big-endian base58 representation.
            var size = (bytes.Length - zeroCount) * 138 / 100 + 1;  // log(256) / log(58)
            var b58 = new byte[size];

            // Process the bytes.
            foreach (var ch in bytes)
            {
                int carry = ch;
                int len = 0;

                // Apply "b58 = b58 * 256 + ch"
                for (int i = b58.Length - 1; (carry != 0 || len < length) && i >= 0; i--, len++)
                {
                    carry += 256 * b58[i];
                    b58[i] = (byte)(carry % 58);
                    carry /= 58;
                }

                length = len;
            }

            var sb = new StringBuilder();
            sb.Append("1".Repeat(zeroCount));
            for (int i = size - length; i < b58.Length; i++)
            {
                // Skip leading zeroes in base58 result.
                if (b58[i] == 0) continue;

                for (int j = i; j < b58.Length; j++)
                    sb.Append(BASE58_CHARS[b58[j]]);
                break;
            }

            return sb.ToString();
        }

        public static byte[] FromBase58String(string str)
        {
            //Refer: https://github.com/bitcoin/bitcoin/blob/master/src/base58.cpp

            str = str.Replace(" ", "");

            // Skip and count leading '1's.
            int zeroCount = 0;
            int length = 0;
            foreach (var ch in str)
            {
                if (ch == '1') zeroCount++;
                else break;
            }

            // Allocate enough space in big-endian base256 representation.
            int size = str.Length * 733 / 1000 + 1; // log(58) / log(256), rounded up.
            var b256 = new byte[size];

            // Process the characters.
            foreach (var ch in str)
            {
                // Decode base58 character
                int carry = Base58Map[ch];
                if (carry == -1) throw new FormatException("Invalid Base58 character.");

                int len = 0;
                for (int i = b256.Length - 1; (carry != 0 || len < length) && i >= 0; i--, len++)
                {
                    carry += 58 * b256[i];
                    b256[i] = (byte)(carry % 256);
                    carry /= 256;
                }

                length = len;
            }

            var bytes = new byte[zeroCount + length];
            for (int i = size - length, bi = zeroCount; i < b256.Length; i++, bi++)
            {
                bytes[bi] = b256[i];
            }

            return bytes;
        }

    }
}
