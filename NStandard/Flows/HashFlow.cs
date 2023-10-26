using NStandard.Security;

namespace NStandard.Flows;

public static class HashFlow
{
    public static byte[] MD5(byte[] bytes) => Md5Hasher.Hash(bytes);
    public static byte[] SHA1(byte[] bytes) => Sha1Hasher.Hash(bytes);
}
