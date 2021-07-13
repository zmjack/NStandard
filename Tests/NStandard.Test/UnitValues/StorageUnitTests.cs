using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NStandard.UnitValues.Test
{
    public class StorageUnitTests
    {
        [Fact]
        public void Test1()
        {
            var a = new StorageValue(1, "mB");
            var b = StorageValue.Parse(".1 kB");
            Assert.Equal(1048678.4, (a + b).Format("B").Value);
            Assert.Equal(8389427.2, (a + b).Format("b").Value);
        }

    }
}
