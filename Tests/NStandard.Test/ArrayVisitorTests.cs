using NStandard.Collections;
using System;
using Xunit;

namespace NStandard.Test
{
    public class ArrayVisitorTests
    {
        [Fact]
        public void Test()
        {
            var src = new string[4, 4].Let(i => i.ToString());
            var visitor = new ArrayVisitor<string>(src);
            visitor.SetValue("0", 15);
            Assert.Equal("0", src[3, 3]);
        }
    }
}
