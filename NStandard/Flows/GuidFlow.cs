using System;

namespace NStandard.Flows
{
    public static class GuidFlow
    {
        public static IFlow<Guid, string> HexString = new Flow<Guid, string>(x => x.ToByteArray().Flow(BytesFlow.HexString));
        public static IFlow<Guid, string> Base58 = new Flow<Guid, string>(x => x.ToByteArray().Flow(BytesFlow.Base58));
        public static IFlow<Guid, string> Base64 = new Flow<Guid, string>(x => x.ToByteArray().Flow(BytesFlow.Base64));
        public static IFlow<Guid, string> UrlSafeBase64 = new Flow<Guid, string>(x => x.ToByteArray().Flow(BytesFlow.UrlSafeBase64));

        public static IFlow<string, Guid> FromHexString = new Flow<string, Guid>(x => new Guid(x.Flow(BytesFlow.FromHexString)));
        public static IFlow<string, Guid> FromBase58 = new Flow<string, Guid>(x => new Guid(x.Flow(BytesFlow.FromBase58)));
        public static IFlow<string, Guid> FromBase64 = new Flow<string, Guid>(x => new Guid(x.Flow(BytesFlow.FromBase64)));
        public static IFlow<string, Guid> FromUrlSafeBase64 = new Flow<string, Guid>(x => new Guid(x.Flow(BytesFlow.FromUrlSafeBase64)));

    }

}
