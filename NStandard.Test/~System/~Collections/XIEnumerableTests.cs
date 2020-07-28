using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NStandard.Test
{
    public class XIEnumerableTests
    {
        [Fact]
        public void GetWindowsTest1()
        {
            var numbers = new[] { 100, 200, 300, 400 };
            var result2 = numbers.GetWindows(2).Max(x => x.Values.Sum());
            Assert.Equal(700, result2);
        }

    }
}
