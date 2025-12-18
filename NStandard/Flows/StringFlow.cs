using NStandard.Converts;
using NStandard.Static;
using System.Text;
#if NET6_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET451_OR_GREATER
using System.Net;
#else
using System.Web;
#endif

namespace NStandard.Flows;

public static class StringFlow
{
    public static byte[] Bytes(string? str, Encoding encoding) => encoding.GetBytes(str!);

    public static byte[] BytesFromBase58(string? str) => ConvertEx.FromBase58String(str!);
    public static byte[] BytesFromBase64(string? str) => Convert.FromBase64String(str!);
    public static byte[] BytesFromHexString(string? str) => StringConvert.FromHexString(str!);
    public static byte[] BytesFromUrlSafeBase64(string? str) => Convert.FromBase64String(StringConvert.ConvertUrlSafeBase64ToBase64(str!));

    public static Guid GuidFromHexString(string? str) => new(BytesFromHexString(str));
    public static Guid GuidFromBase58(string? str) => new(BytesFromBase58(str));
    public static Guid GuidFromBase64(string? str) => new(BytesFromBase64(str));
    public static Guid GuidFromUrlSafeBase64(string? str) => new(BytesFromUrlSafeBase64(str));

#if NET6_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET451_OR_GREATER
    public static string? UrlEncode(string? str) => WebUtility.UrlEncode(str);
    public static string? UrlDecode(string? str) => WebUtility.UrlDecode(str);
    public static string? UrlEncode(string? str, Encoding e)
    {
        var bytes = e.GetBytes(str!);
        return Encoding.Default.GetString(WebUtility.UrlEncodeToBytes(bytes, 0, bytes.Length));
    }
    public static string? UrlDecode(string? str, Encoding e)
    {
        var bytes = Encoding.Default.GetBytes(str!);
        return e.GetString(WebUtility.UrlDecodeToBytes(bytes, 0, bytes.Length));
    }

    public static string? HtmlEncode(string? str) => WebUtility.HtmlEncode(str!);
    public static string? HtmlDecode(string? str) => WebUtility.HtmlDecode(str!);
#else
    public static string UrlEncode(string str) => HttpUtility.UrlEncode(str);
    public static string UrlDecode(string str) => HttpUtility.UrlDecode(str);
    public static string UrlEncode(string str, Encoding e) => HttpUtility.UrlEncode(str, e);
    public static string UrlDecode(string str, Encoding e) => HttpUtility.UrlDecode(str, e);

    public static string HtmlEncode(string str) => HttpUtility.HtmlEncode(str);
    public static string HtmlDecode(string str) => HttpUtility.HtmlDecode(str);
#endif
}
