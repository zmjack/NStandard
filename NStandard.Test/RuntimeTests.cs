using System;
using Xunit;

namespace NStandard.Test
{
    public class RuntimeTests
    {
        [Fact]
        public unsafe void AddressOfTest()
        {
            var str = "abc";
            var ptr = Native.AddressOf(str);
            var length = BitConverter.ToInt32(Native.ReadMemory(ptr, sizeof(int)), 0);
            var pStrPart = ptr + sizeof(int);

            Assert.Equal(3, length);
            Assert.Equal("abc", str);

            Native.WriteMemory(pStrPart, "A".Bytes());
            Assert.Equal("Abc", str);
        }

        [Fact]
        public void AreSameTest()
        {
            var s1 = "abc";
            var s2 = "abc";
            Assert.True(Native.AreSame(s1, s2));

            s2 = "ABC";
            Assert.False(Native.AreSame(s1, s2));
        }

    }
}
