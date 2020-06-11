using System.IO;
using System.Security.Cryptography;

namespace NStandard.Security
{
    public static class Md5Provider
    {
        public static byte[] ComputeHash(Stream inputStream) => MD5.Create().ComputeHash(inputStream);
        public static byte[] ComputeHash(byte[] buffer, int offset, int count) => MD5.Create().ComputeHash(buffer, offset, count);
        public static byte[] ComputeHash(byte[] buffer) => MD5.Create().ComputeHash(buffer);
    }
}
