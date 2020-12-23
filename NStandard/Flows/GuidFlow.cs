using System;

namespace NStandard.Flows
{
    public static class GuidFlow
    {
        public static string HexString(Guid guid) => guid.ToByteArray().For(BytesFlow.HexString);
        public static string Base58(Guid guid) => guid.ToByteArray().For(BytesFlow.Base58);
        public static string Base64(Guid guid) => guid.ToByteArray().For(BytesFlow.Base64);
        public static string UrlSafeBase64(Guid guid) => guid.ToByteArray().For(BytesFlow.UrlSafeBase64);

    }

}
