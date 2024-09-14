using NStandard.Converts;

namespace NStandard.Flows;

public static class BytesFlow
{
    public static string Base58(byte[] bytes) => ConvertEx.ToBase58String(bytes);
    public static string Base64(byte[] bytes) => Convert.ToBase64String(bytes);
    public static string HexString(byte[] bytes) => BytesConvert.ToHexString(bytes);
    public static string UrlSafeBase64(byte[] bytes) => StringConvert.ConvertBase64ToUrlSafeBase64(Convert.ToBase64String(bytes));

}
