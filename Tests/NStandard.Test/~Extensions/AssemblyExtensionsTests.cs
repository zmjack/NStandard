using System;
using System.Reflection;
using Xunit;

namespace NStandard.Test;

public class AssemblyExtensionsTests
{
    public interface IA { }

    public class A : IA { }
    public class B : A { }
    [Mark]
    public class C : B { }

    public class MarkAttribute : Attribute { }

    [Fact]
    public void Test1()
    {
        var assembly = Assembly.GetExecutingAssembly();

        Assert.Equal([typeof(B)], assembly.GetTypesWhichExtends<A>(false));
        Assert.Equal([typeof(B), typeof(C)], assembly.GetTypesWhichExtends<A>(true));

        Assert.Equal([typeof(A), typeof(B), typeof(C)], assembly.GetTypesWhichImplements<IA>());
        Assert.Equal([typeof(C)], assembly.GetTypesWhichMarkedAs<MarkAttribute>());
    }

}
