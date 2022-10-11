using NStandard.Collections;
using System;
using Xunit;

namespace NStandard.Test
{
    public class ArrayVisitorTests
    {
        [Fact]
        public void TypeNotSameTest()
        {
            var src = new string[4, 4].Let((i0, i1) => $"{i0}, {i1}");
            Assert.Throws<ArgumentException>(() => new ArrayVisitor<int>(src));
        }

        [Fact]
        public void Test()
        {
            var src = new string[4, 4].Let((i0, i1) => $"{i0}, {i1}");
            var visitor = new ArrayVisitor<string>(src);
            visitor.SetValue("-", 15);
            Assert.Equal("-", src[3, 3]);
        }
    }
}
