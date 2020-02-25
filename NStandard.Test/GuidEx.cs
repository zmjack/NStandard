using System;
using Xunit;

namespace NStandard.Test
{
    public class GuidExTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(Guid.Empty.ToString(), GuidEx.EMPTY_STRING);
        }

    }
}
