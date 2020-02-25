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
            var result416 = Dynamic.OpAddChecked(400, 16);
            Assert.Equal(416, result416);
        }

    }
}
