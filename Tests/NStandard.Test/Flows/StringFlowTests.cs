using NStandard.Flows;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class StringFlowTests
    {
        [Fact]
        public void BytesTest()
        {
            var a = "ABC".For(StringFlow.Bytes, Encoding.UTF8);
            "ABC".Bytes(Encoding.UTF8).For(BytesFlow.Base58);
        }

        [Fact]
        public void Base58Test()
        {
            Assert.Equal("zpsEBKbce3iT", "NStandard".Bytes().For(BytesFlow.Base58));
            Assert.Equal("NStandard", "zpsEBKbce3iT".For(StringFlow.BytesFromBase58).String());
        }

        [Fact]
        public void Base64Test()
        {
            Assert.Equal("QUI+Q0Q/RQ==", "AB>CD?E".Bytes().For(BytesFlow.Base64));
            Assert.Equal("QUI-Q0Q_RQ", "AB>CD?E".Bytes().For(BytesFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?E", "QUI+Q0Q/RQ==".For(StringFlow.BytesFromBase64).String());
            Assert.Equal("AB>CD?E", "QUI-Q0Q_RQ".For(StringFlow.BytesFromUrlSafeBase64).String());

            Assert.Equal("QUI+Q0Q/RUY=", "AB>CD?EF".Bytes().For(BytesFlow.Base64));
            Assert.Equal("QUI-Q0Q_RUY", "AB>CD?EF".Bytes().For(BytesFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?EF", "QUI+Q0Q/RUY=".For(StringFlow.BytesFromBase64).String());
            Assert.Equal("AB>CD?EF", "QUI-Q0Q_RUY".For(StringFlow.BytesFromUrlSafeBase64).String());

            Assert.Equal("QUI+Q0Q/RUZH", "AB>CD?EFG".Bytes().For(BytesFlow.Base64));
            Assert.Equal("QUI-Q0Q_RUZH", "AB>CD?EFG".Bytes().For(BytesFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?EFG", "QUI+Q0Q/RUZH".For(StringFlow.BytesFromBase64).String());
            Assert.Equal("AB>CD?EFG", "QUI-Q0Q_RUZH".For(StringFlow.BytesFromUrlSafeBase64).String());
        }

        [Fact]
        public void HexTest()
        {
            Assert.Equal("41424378797a313233", "ABCxyz123".Bytes().For(BytesFlow.HexString));
            Assert.Equal("ABCxyz123", "41424378797a313233".For(StringFlow.BytesFromHexString).String());
        }

    }
}
