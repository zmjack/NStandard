using Xunit;

namespace NStandard.Runtime.Test;

public class DotNetFrameworkTests
{
    [Fact]
    public void Test1()
    {
        var fws = DotNetFramework.Parse("net451").Compatibility.Select(x => x.ToString()).ToArray();
        Assert.Equal(
        [
            "net451", "netstandard1.2",
            "net45", "netstandard1.1", "netstandard1.0",
            "net403", "net40", "net35", "net20", "net11"
        ], fws);
    }

    [Fact]
    public void NotDeclaredTFMTest()
    {
        Assert.Throws<NotSupportedException>(() =>
        {
            DotNetFramework.Parse("netcoreapp??").Compatibility.Select(x => x.ToString()).ToArray();
        });
    }

}
