using System.Security.Cryptography;

namespace NStandard.Security;

public static class Sha1Hasher
{
    public static byte[] Hash(Stream inputStream) => SHA1.Create().ComputeHash(inputStream);
    public static byte[] Hash(byte[] buffer, int offset, int count) => SHA1.Create().ComputeHash(buffer, offset, count);
    public static byte[] Hash(byte[] buffer) => SHA1.Create().ComputeHash(buffer);
}
