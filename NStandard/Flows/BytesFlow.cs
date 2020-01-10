using NStandard.Converts;
using System;

namespace NStandard.Flows
{
    public static class BytesFlow
    {
        public static IFlow<byte[], string> Base64 = new Flow<byte[], string>(Convert.ToBase64String);
        public static IFlow<byte[], string> HexString = new Flow<byte[], string>(BytesConvert.ToHexString);
        public static IFlow<byte[], string> UrlSafeBase64 = new Flow<byte[], string, string>(Convert.ToBase64String, StringConvert.ConvertBase64ToUrlSafeBase64);

        public static IFlow<string, byte[]> FromBase64 = new Flow<string, byte[]>(Convert.FromBase64String);
        public static IFlow<string, byte[]> FromHexString = new Flow<string, byte[]>(StringConvert.FromHexString);
        public static IFlow<string, byte[]> FromUrlSafeBase64 = new Flow<string, string, byte[]>(StringConvert.ConvertUrlSafeBase64ToBase64, Convert.FromBase64String);
    }

}
