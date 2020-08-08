using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class StaticPointTests
    {
        [Fact]
        public void Test1()
        {
            var numbers = new[] { 11, 22, 33 };
            for (int i = 0; i < 3; i++)
            {
                var point = StaticPoint.Save("2108C2FA-4491-415A-8F1E-98C33EEC3358", numbers[i]);
                Assert.Equal(0 + 11 * i, point.OldValue);
                Assert.Equal(numbers[i], point.CurrentValue);
            }
        }

    }
}
