using NStandard.Security;

namespace NStandard.Flows
{
    public static class HashFlow
    {
        public static IFlow<byte[], byte[]> MD5 = new Flow<byte[], byte[]>(Md5Provider.ComputeHash);
        public static IFlow<byte[], byte[]> SHA1 = new Flow<byte[], byte[]>(Sha1Provider.ComputeHash);
    }
}
