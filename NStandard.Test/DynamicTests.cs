using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class DynamicTests
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(416, Dynamic.OpAddChecked(400, 16));
            Assert.True(Dynamic.OpGreaterThan(400, 16));
        }

    }
}
