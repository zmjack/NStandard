using System.Net;
using Xunit;

namespace NStandard.Test;

public class IPAddressExtensionsTests
{
    [Fact]
    public void IPv4Test()
    {
        var uint32 = IPAddress.Parse("255.0.255.0").ToUInt32();
        Assert.Equal(0xFF00FF00, uint32);

        var str = IPAddressEx.Create(uint32).ToString();
        Assert.Equal("255.0.255.0", str);
    }

#if NET7_0_OR_GREATER
    [Fact]
    public void IPv6Test2()
    {
        var uint128 = IPAddress.Parse("FFFF:0000:FFFF:0000:FFFF:0000:FFFF:0000").ToUInt128();
        Assert.Equal(new UInt128(0xFFFF0000FFFF0000, 0xFFFF0000FFFF0000), uint128);

        var str = IPAddressEx.Create(uint128).ToString();
        Assert.Equal("ffff:0:ffff:0:ffff:0:ffff:0", str);
    }
#endif
}
