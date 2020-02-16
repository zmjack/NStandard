using System;
using System.Collections.Generic;
using System.Text;
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
