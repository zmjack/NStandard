using Xunit;

namespace NStandard.Test;

public class AppDomainExtensionsTests
{
    [Fact]
    public void Test1()
    {
        var coreLib = AppDomain.CurrentDomain.GetCoreLibAssembly();
        var sr = coreLib.GetType("System.SR").GetDeclaredMethodViaQualifiedName("System.String GetResourceString(System.String)");
        var resourceString = sr.Invoke(null, ["Argument_EnumTypeDoesNotMatch"]);

        Assert.Equal("The argument type, '{0}', is not the same as the enum type '{1}'.", resourceString);
    }

}
