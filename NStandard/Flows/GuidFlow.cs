using System;

namespace NStandard.Flows
{
    public static class GuidFlow
    {
        public static IFlow<Guid, string> ShortString = new Flow<Guid, string>(x => x.ToByteArray().Flow(BytesFlow.UrlSafeBase64));
        public static IFlow<string, Guid> FromShortString = new Flow<string, Guid>(x => new Guid(x.Flow(BytesFlow.FromUrlSafeBase64)));
        public static IFlow<Guid, string> HexString = new Flow<Guid, string>(x => x.ToByteArray().Flow(BytesFlow.HexString));
        public static IFlow<string, Guid> FromHexString = new Flow<string, Guid>(x => new Guid(x.Flow(BytesFlow.FromHexString)));
    }

}
