using NStandard.Flows;
using System;
using Xunit;

namespace NStandard.Test
{
    public class GuidFlowTests
    {
        [Fact]
        public void Test1()
        {
            var guid = Guid.Parse("d19eaa91-337a-4853-9e93-52dfb50b3e31");

            Assert.Equal("kaqe0XozU0iek1LftQs-MQ", guid.Flow(GuidFlow.ShortString));
            Assert.Equal(guid, "kaqe0XozU0iek1LftQs-MQ".Flow(GuidFlow.FromShortString));

            Assert.Equal("91aa9ed17a3353489e9352dfb50b3e31", guid.Flow(GuidFlow.HexString));
            Assert.Equal(guid, "91aa9ed17a3353489e9352dfb50b3e31".Flow(GuidFlow.FromHexString));
        }

    }
}
