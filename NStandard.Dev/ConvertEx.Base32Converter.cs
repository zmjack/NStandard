using NStandard.IO;

namespace NStandard
{
    public static partial class ConvertExDesign
    {
        private static class Base32Converter
        {
            private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            private static readonly int[] Map =
            [
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, 26, 27, 28, 29, 30, 31,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1,  1,  2,  3,  4,  5,  6,  7,   8,  9, 10, 11, 12, 13, 14, 15,
                16, 17, 18, 19, 20, 21, 22, 23,  24, 25, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1,  -1, -1, -1, -1, -1, -1, -1, -1,
            ];

            public static unsafe string ToBase32String(byte[] bytes)
            {
                using var stream = new BitStream(bytes, false);

                // Allocate enough space in big-endian base32 representation.
                var groupSize = bytes.Length / 5 + (bytes.Length % 5 > 0 ? 1 : 0);
                var bitSize = 40 * groupSize;
                var size = bitSize / 5;
                var c32 = new char[size];

                // Process the bytes.
                var buffer = new byte[4];
                var invalid = size;
                for (int i = 0; i < size; i++)
                {
                    var read = stream.Read(buffer, 0, 5);
                    if (read > 0)
                    {
                        var index = BitConverter.ToInt32(buffer, 0);
                        c32[i] = CHARS[index];
                    }
                    else
                    {
                        invalid = i;
                        break;
                    }
                }

                // Process the paddings.
                for (int i = invalid; i < size; i++)
                {
                    c32[i] = '=';
                }

                return new string(c32);
            }

            public static byte[] FromBase32String(string str)
            {
                throw new NotSupportedException();
            }
        }
    }
}
