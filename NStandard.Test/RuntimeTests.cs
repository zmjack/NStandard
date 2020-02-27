using System;
using System.Runtime.InteropServices;
using Xunit;

namespace NStandard.Test
{
    public class RuntimeTests
    {
        [Fact]
        public unsafe void AddressOfTest()
        {
            var s = "abc";
            var p = Runtime.AddressOf(s);
            var pspart = p + IntPtr.Size + sizeof(int);

            var partBytes = new char[s.Length];
            Marshal.Copy(pspart, partBytes, 0, s.Length);

            Assert.Equal("abc", new string(partBytes));
        }

        [Fact]
        public void AreSameTest1()
        {
            var s1 = "abc";
            var s2 = "abc";

            Assert.True(Runtime.AreSame(s1, s2));

            s2 = "ABC";
            Assert.False(Runtime.AreSame(s1, s2));
        }

        [Fact]
        public void AreSameTest2()
        {
            var i1 = 123;
            var i2 = 123;

            Assert.False(Runtime.AreSame(i1, i2));
        }

    }
}
