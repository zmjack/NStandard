using NStandard.Converts;
using System;

namespace NStandard.Flows
{
    public static class BytesFlow
    {
        public static IFlow<byte[], string> Base58 = new Flow<byte[], string>(x => ConvertEx.ToBase58String(x));
        public static IFlow<byte[], string> Base64 = new Flow<byte[], string>(x => Convert.ToBase64String(x));
        public static IFlow<byte[], string> HexString = new Flow<byte[], string>(x => BytesConvert.ToHexString(x));
        public static IFlow<byte[], string> UrlSafeBase64 = new Flow<byte[], string>(x => StringConvert.ConvertBase64ToUrlSafeBase64(Convert.ToBase64String(x)));

        public static IFlow<string, byte[]> FromBase58 = new Flow<string, byte[]>(ConvertEx.FromBase58String);
        public static IFlow<string, byte[]> FromBase64 = new Flow<string, byte[]>(Convert.FromBase64String);
        public static IFlow<string, byte[]> FromHexString = new Flow<string, byte[]>(StringConvert.FromHexString);
        public static IFlow<string, byte[]> FromUrlSafeBase64 = new Flow<string, byte[]>(x => Convert.FromBase64String(StringConvert.ConvertUrlSafeBase64ToBase64(x)));
    }

}
