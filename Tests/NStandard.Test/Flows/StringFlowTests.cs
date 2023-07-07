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
            var a = "ABC".Pipe(x => StringFlow.Bytes(x, Encoding.UTF8));
            "ABC".Bytes(Encoding.UTF8).Pipe(BytesFlow.Base58);
        }

        [Fact]
        public void Base58Test()
        {
            Assert.Equal("4EeR6TRBXpSiquHneip55ZwG3", "NStandard".Bytes().Pipe(BytesFlow.Base58));
            Assert.Equal("NStandard", "4EeR6TRBXpSiquHneip55ZwG3".Pipe(StringFlow.BytesFromBase58).String());
        }

        [Fact]
        public void Base64Test()
        {
            Assert.Equal("QUI+Q0Q/RQ==", "AB>CD?E".Bytes(Encoding.UTF8).Pipe(BytesFlow.Base64));
            Assert.Equal("QUI-Q0Q_RQ", "AB>CD?E".Bytes(Encoding.UTF8).Pipe(BytesFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?E", "QUI+Q0Q/RQ==".Pipe(StringFlow.BytesFromBase64).String(Encoding.UTF8));
            Assert.Equal("AB>CD?E", "QUI-Q0Q_RQ".Pipe(StringFlow.BytesFromUrlSafeBase64).String(Encoding.UTF8));

            Assert.Equal("QUI+Q0Q/RUY=", "AB>CD?EF".Bytes(Encoding.UTF8).Pipe(BytesFlow.Base64));
            Assert.Equal("QUI-Q0Q_RUY", "AB>CD?EF".Bytes(Encoding.UTF8).Pipe(BytesFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?EF", "QUI+Q0Q/RUY=".Pipe(StringFlow.BytesFromBase64).String(Encoding.UTF8));
            Assert.Equal("AB>CD?EF", "QUI-Q0Q_RUY".Pipe(StringFlow.BytesFromUrlSafeBase64).String(Encoding.UTF8));

            Assert.Equal("QUI+Q0Q/RUZH", "AB>CD?EFG".Bytes(Encoding.UTF8).Pipe(BytesFlow.Base64));
            Assert.Equal("QUI-Q0Q_RUZH", "AB>CD?EFG".Bytes(Encoding.UTF8).Pipe(BytesFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?EFG", "QUI+Q0Q/RUZH".Pipe(StringFlow.BytesFromBase64).String(Encoding.UTF8));
            Assert.Equal("AB>CD?EFG", "QUI-Q0Q_RUZH".Pipe(StringFlow.BytesFromUrlSafeBase64).String(Encoding.UTF8));
        }

        [Fact]
        public void HexTest()
        {
            Assert.Equal("41424378797a313233", "ABCxyz123".Bytes(Encoding.UTF8).Pipe(BytesFlow.HexString));
            Assert.Equal("ABCxyz123", "41424378797a313233".Pipe(StringFlow.BytesFromHexString).String(Encoding.UTF8));
        }

    }
}
