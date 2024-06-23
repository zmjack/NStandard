using System.Reflection;

namespace NStandard.Reflection;

public class TypeMethodReflector
{
    public readonly MethodInfo MethodInfo;

    public TypeMethodReflector(MethodInfo methodInfo)
    {
        MethodInfo = methodInfo;
    }

    public object? Invoke(object obj, params object[] parameters) => MethodInfo.Invoke(obj, parameters);
}
