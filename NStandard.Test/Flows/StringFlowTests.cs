using NStandard.Flows;
using Xunit;

namespace NStandard.Test
{
    public class StringFlowTests
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("QUI+Q0Q/RQ==", "AB>CD?E".Flow(StringFlow.Base64));
            Assert.Equal("QUI-Q0Q_RQ", "AB>CD?E".Flow(StringFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?E", "QUI+Q0Q/RQ==".Flow(StringFlow.FromBase64));
            Assert.Equal("AB>CD?E", "QUI-Q0Q_RQ".Flow(StringFlow.FromUrlSafeBase64));

            Assert.Equal("QUI+Q0Q/RUY=", "AB>CD?EF".Flow(StringFlow.Base64));
            Assert.Equal("QUI-Q0Q_RUY", "AB>CD?EF".Flow(StringFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?EF", "QUI+Q0Q/RUY=".Flow(StringFlow.FromBase64));
            Assert.Equal("AB>CD?EF", "QUI-Q0Q_RUY".Flow(StringFlow.FromUrlSafeBase64));

            Assert.Equal("QUI+Q0Q/RUZH", "AB>CD?EFG".Flow(StringFlow.Base64));
            Assert.Equal("QUI-Q0Q_RUZH", "AB>CD?EFG".Flow(StringFlow.UrlSafeBase64));
            Assert.Equal("AB>CD?EFG", "QUI+Q0Q/RUZH".Flow(StringFlow.FromBase64));
            Assert.Equal("AB>CD?EFG", "QUI-Q0Q_RUZH".Flow(StringFlow.FromUrlSafeBase64));
        }

    }
}
