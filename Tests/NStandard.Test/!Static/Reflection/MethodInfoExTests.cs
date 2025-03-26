using NStandard.Static.Reflection;
using Xunit;

namespace NStandard.Test;

public class MethodInfoExTests
{
    [Fact]
    public void Test()
    {
        var method1 = MethodInfoEx.GetMethodInfo(() => MethodInfoEx.GetMethodInfo);
        var method2 = MethodInfoEx.GetMethodInfo(() => MethodInfoEx.GetMethodInfo);
        Assert.Equal(method1, method2);
    }

}
