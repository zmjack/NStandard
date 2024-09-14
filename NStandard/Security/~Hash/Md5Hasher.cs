using System.Security.Cryptography;

namespace NStandard.Security;

public static class Md5Hasher
{
    public static byte[] Hash(Stream inputStream) => MD5.Create().ComputeHash(inputStream);
    public static byte[] Hash(byte[] buffer, int offset, int count) => MD5.Create().ComputeHash(buffer, offset, count);
    public static byte[] Hash(byte[] buffer) => MD5.Create().ComputeHash(buffer);
}
