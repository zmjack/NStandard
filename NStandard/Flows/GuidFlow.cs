using System;

namespace NStandard.Flows;

public static class GuidFlow
{
    public static string HexString(Guid guid) => BytesFlow.HexString(guid.ToByteArray());
    public static string Base58(Guid guid) => BytesFlow.Base58(guid.ToByteArray());
    public static string Base64(Guid guid) => BytesFlow.Base64(guid.ToByteArray());
    public static string UrlSafeBase64(Guid guid) => BytesFlow.UrlSafeBase64(guid.ToByteArray());

}
