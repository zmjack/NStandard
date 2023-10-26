using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NStandard;

public static class MethodInfoEx
{
    /// <summary>
    /// Get MethodInfo from a method.
    /// </summary>
    /// <param name="methodSelector"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static MethodInfo GetMethodInfo(Expression<Func<Delegate>> methodSelector)
    {
        var unary = methodSelector.Body as UnaryExpression;
        if (unary is null) throw new ArgumentException(nameof(methodSelector), $"The method selector must point to a method.");

        var operand = unary.Operand as MethodCallExpression;
        var constant = operand.Object as ConstantExpression;
        var method = constant.Value as MethodInfo;

        return method;
    }
}
