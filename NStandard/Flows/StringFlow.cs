using NStandard.Converts;
using System;
using System.Text;
#if NETSTANDARD2_0
using System.Net;
#else
using System.Web;
#endif

namespace NStandard.Flows
{
    public static class StringFlow
    {
        public static byte[] Bytes(string str, Encoding encoding) => encoding.GetBytes(str);

        public static byte[] BytesFromBase58(string str) => ConvertEx.FromBase58String(str);
        public static byte[] BytesFromBase64(string str) => Convert.FromBase64String(str);
        public static byte[] BytesFromHexString(string str) => StringConvert.FromHexString(str);
        public static byte[] BytesFromUrlSafeBase64(string str) => Convert.FromBase64String(StringConvert.ConvertUrlSafeBase64ToBase64(str));

        public static Guid GuidFromHexString(string str) => new Guid(str.For(BytesFromHexString));
        public static Guid GuidFromBase58(string str) => new Guid(str.For(BytesFromBase58));
        public static Guid GuidFromBase64(string str) => new Guid(str.For(BytesFromBase64));
        public static Guid GuidFromUrlSafeBase64(string str) => new Guid(str.For(BytesFromUrlSafeBase64));

#if NETSTANDARD2_0
        public static string UrlEncode(string str) => WebUtility.UrlEncode(str);
        public static string UrlDecode(string str) => WebUtility.UrlDecode(str);
        public static string HtmlEncode(string str) => WebUtility.HtmlEncode(str);
        public static string HtmlDecode(string str) => WebUtility.HtmlDecode(str);
#else
        public static string UrlEncode(string str) => HttpUtility.UrlEncode(str);
        public static string UrlDecode(string str) => HttpUtility.UrlDecode(str);
        public static string HtmlEncode(string str) => HttpUtility.HtmlEncode(str);
        public static string HtmlDecode(string str) => HttpUtility.HtmlDecode(str);
#endif
    }

}
