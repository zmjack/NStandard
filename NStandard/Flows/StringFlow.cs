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
        public static IFlow<string, string> Base58 = new Flow<string, string>(x => ConvertEx.ToBase58String(Encoding.UTF8.GetBytes(x)));
        public static IFlow<string, string> Base64 = new Flow<string, string>(x => Convert.ToBase64String(Encoding.UTF8.GetBytes(x)));
        public static IFlow<string, string> HexString = new Flow<string, string>(x => BytesConvert.ToHexString(Encoding.UTF8.GetBytes(x)));
        public static IFlow<string, string> UrlSafeBase64 = new Flow<string, string>(x => StringConvert.ConvertBase64ToUrlSafeBase64(Convert.ToBase64String(Encoding.UTF8.GetBytes(x))));

        public static IFlow<string, string> FromBase58 = new Flow<string, string>(x => Encoding.UTF8.GetString(ConvertEx.FromBase58String(x)));
        public static IFlow<string, string> FromBase64 = new Flow<string, string>(x => Encoding.UTF8.GetString(Convert.FromBase64String(x)));
        public static IFlow<string, string> FromHexString = new Flow<string, string>(x => Encoding.UTF8.GetString(StringConvert.FromHexString(x)));
        public static IFlow<string, string> FromUrlSafeBase64 = new Flow<string, string>(x => Encoding.UTF8.GetString(Convert.FromBase64String(StringConvert.ConvertUrlSafeBase64ToBase64(x))));

#if NETSTANDARD2_0
        public static IFlow<string, string> UrlEncode = new Flow<string, string>(WebUtility.UrlEncode);
        public static IFlow<string, string> HtmlEncode = new Flow<string, string>(WebUtility.HtmlEncode);

        public static IFlow<string, string> UrlDecode = new Flow<string, string>(WebUtility.UrlDecode);
        public static IFlow<string, string> HtmlDecode = new Flow<string, string>(WebUtility.HtmlDecode);
#else
        public static IFlow<string, string> UrlEncode = new Flow<string, string>(HttpUtility.UrlEncode);
        public static IFlow<string, string> HtmlEncode = new Flow<string, string>(HttpUtility.HtmlEncode);

        public static IFlow<string, string> UrlDecode = new Flow<string, string>(HttpUtility.UrlDecode);
        public static IFlow<string, string> HtmlDecode = new Flow<string, string>(HttpUtility.HtmlDecode);
#endif
    }

}
