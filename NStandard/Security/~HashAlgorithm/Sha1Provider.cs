using System.IO;
using System.Security.Cryptography;

namespace NStandard.Security
{
    public static class Sha1Provider
    {
        public static byte[] ComputeHash(Stream inputStream) => SHA1.Create().ComputeHash(inputStream);
        public static byte[] ComputeHash(byte[] buffer, int offset, int count) => SHA1.Create().ComputeHash(buffer, offset, count);
        public static byte[] ComputeHash(byte[] buffer) => SHA1.Create().ComputeHash(buffer);
    }
}
