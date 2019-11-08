using NStandard.Converts;
using System;
using System.Net;
using System.Text;

namespace NStandard.Flows
{
    public static class StringFlow
    {
        public static IFlow<string, string> Base64 = new Flow<string, byte[], string>(Encoding.UTF8.GetBytes, Convert.ToBase64String);
        public static IFlow<string, string> HexString = new Flow<string, byte[], string>(Encoding.UTF8.GetBytes, BytesConvert.ToHexString);
        public static IFlow<string, string> UrlSafeBase64 = new Flow<string, byte[], string, string>(Encoding.UTF8.GetBytes, Convert.ToBase64String, StringConvert.ConvertBase64ToUrlSafeBase64);

        public static IFlow<string, string> FromBase64 = new Flow<string, byte[], string>(Convert.FromBase64String, Encoding.UTF8.GetString);
        public static IFlow<string, string> FromHexString = new Flow<string, byte[], string>(StringConvert.FromHexString, Encoding.UTF8.GetString);
        public static IFlow<string, string> FromUrlSafeBase64 = new Flow<string, string, byte[], string>(StringConvert.ConvertUrlSafeBase64ToBase64, Convert.FromBase64String, Encoding.UTF8.GetString);

        public static IFlow<string, string> UrlEncode = new Flow<string, string>(WebUtility.UrlEncode);
        public static IFlow<string, string> HtmlEncode = new Flow<string, string>(WebUtility.HtmlEncode);

        public static IFlow<string, string> UrlDecode = new Flow<string, string>(WebUtility.UrlDecode);
        public static IFlow<string, string> HtmlDecode = new Flow<string, string>(WebUtility.HtmlDecode);
    }

}
