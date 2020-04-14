using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class LoopTests
    {
        [Fact]
        public void Test1()
        {
            var iteratorValues = new Loop(2, 3).ToArray();
            Assert.Equal(new[]
            {
                new[] { 0, 0 }, new[] { 0, 1 }, new[] { 0, 2 },
                new[] { 1, 0 }, new[] { 1, 1 }, new[] { 1, 2 },
            }, iteratorValues);
        }

    }
}
