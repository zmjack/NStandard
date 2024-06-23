using System;
using System.Reflection;

namespace NStandard.Reflection;

public class MethodReflector(MethodInfo methodInfo, object? declaringObj)
{
    public readonly MethodInfo MethodInfo = methodInfo;
    public object? DeclaringObject = declaringObj;

    public object? Call(params object[] parameters)
    {
        return MethodInfo is null
            ? throw new ArgumentNullException("The method can not be null")
            : MethodInfo.Invoke(DeclaringObject, parameters);
    }
}
