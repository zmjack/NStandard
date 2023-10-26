using System.Net;
using Xunit;

namespace NStandard.Flows.Test;

public class IPAddressFlowTests
{
    [Fact]
    public void Test1()
    {
        Assert.Equal(0x7f000001, IPAddress.Parse("127.0.0.1").Pipe(IPAddressFlow.Int64));
        Assert.Equal(0x7f000002, IPAddress.Parse("127.0.0.2").Pipe(IPAddressFlow.Int64));

        Assert.Equal("127.0.0.1", ((long)0x7f000001).Pipe(IPAddressFlow.IPAddress).ToString());
        Assert.Equal("127.0.0.2", ((long)0x7f000002).Pipe(IPAddressFlow.IPAddress).ToString());
    }

}
