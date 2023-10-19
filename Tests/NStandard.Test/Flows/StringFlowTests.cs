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
            "ABC".Pipe(Encoding.UTF8.GetBytes).Pipe(BytesFlow.Base58);
        }

        [Fact]
        public void Base58Test()
        {
            Assert.Equal("4EeR6TRBXpSiquHneip55ZwG3", "NStandard".Pipe(Encoding.Unicode.GetBytes).Pipe(BytesFlow.Base58));
            Assert.Equal("NStandard", "4EeR6TRBXpSiquHneip55ZwG3".Pipe(StringFlow.BytesFromBase58).Pipe(Encoding.Unicode.GetString));
        }

        [Fact]
        public void Base64Test()
        {
            Assert.Equal("QUI+Q0Q/RQ==", "AB>CD?E".Pipe(Encoding.UTF8.GetBytes).Pipe(BytesFlow.Base64));
            Assert.Equal("QUI-Q0Q_RQ", "AB>CD?E".Pipe(Encoding.UTF8.GetBytes).Pipe(BytesFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?E", "QUI+Q0Q/RQ==".Pipe(StringFlow.BytesFromBase64).Pipe(Encoding.UTF8.GetString));
            Assert.Equal("AB>CD?E", "QUI-Q0Q_RQ".Pipe(StringFlow.BytesFromUrlSafeBase64).Pipe(Encoding.UTF8.GetString));

            Assert.Equal("QUI+Q0Q/RUY=", "AB>CD?EF".Pipe(Encoding.UTF8.GetBytes).Pipe(BytesFlow.Base64));
            Assert.Equal("QUI-Q0Q_RUY", "AB>CD?EF".Pipe(Encoding.UTF8.GetBytes).Pipe(BytesFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?EF", "QUI+Q0Q/RUY=".Pipe(StringFlow.BytesFromBase64).Pipe(Encoding.UTF8.GetString));
            Assert.Equal("AB>CD?EF", "QUI-Q0Q_RUY".Pipe(StringFlow.BytesFromUrlSafeBase64).Pipe(Encoding.UTF8.GetString));

            Assert.Equal("QUI+Q0Q/RUZH", "AB>CD?EFG".Pipe(Encoding.UTF8.GetBytes).Pipe(BytesFlow.Base64));
            Assert.Equal("QUI-Q0Q_RUZH", "AB>CD?EFG".Pipe(Encoding.UTF8.GetBytes).Pipe(BytesFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?EFG", "QUI+Q0Q/RUZH".Pipe(StringFlow.BytesFromBase64).Pipe(Encoding.UTF8.GetString));
            Assert.Equal("AB>CD?EFG", "QUI-Q0Q_RUZH".Pipe(StringFlow.BytesFromUrlSafeBase64).Pipe(Encoding.UTF8.GetString));
        }

        [Fact]
        public void HexTest()
        {
            Assert.Equal("41424378797a313233", "ABCxyz123".Pipe(Encoding.UTF8.GetBytes).Pipe(BytesFlow.HexString));
            Assert.Equal("ABCxyz123", "41424378797a313233".Pipe(StringFlow.BytesFromHexString).Pipe(Encoding.UTF8.GetString));
        }

    }
}
