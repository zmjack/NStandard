using System;
using System.Text;
using Xunit;

namespace NStandard.Test;

public class RuntimeTests
{
    [Fact]
    public void AddressOfClassTest()
    {
        var str = "abc";
        var ptr = Native.AddressOf(str, true);
        var length = BitConverter.ToInt32(Native.ReadMemory(ptr, sizeof(int)), 0);
        var pStrPart = ptr + sizeof(int);

        Assert.Equal(3, length);
        Assert.Equal("abc", str);

        Native.WriteMemory(pStrPart, Encoding.Default.GetBytes("A"));
        Assert.Equal("Abc", str);
    }

    [Fact]
    public unsafe void AddressOfStructTest()
    {
        var astruct = new IntStrcut { Value = 255 };
        var ptr = Native.AddressOf(ref astruct);
        Assert.Equal((IntPtr)(&astruct), ptr);
    }

    [Fact]
    public unsafe void AddressOfWrappedStructTest()
    {
        var astruct = new IntStrcut { Value = 255 };
        var ptr = Native.AddressOf(ref astruct);
        var wrappedPtr = Native.AddressOf((object)astruct, false);
        Assert.Equal((IntPtr)(&astruct), ptr);
        Assert.NotEqual((IntPtr)(&astruct), wrappedPtr);
    }

    [Fact]
    public void AreSameTest()
    {
        var s1 = "abc";
        var s2 = "abc";
        Assert.Same(s1, s2);

        s2 = "ABC";
        Assert.NotSame(s1, s2);
    }

}
