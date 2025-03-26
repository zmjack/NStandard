using System.Linq.Expressions;
using System.Reflection;

namespace NStandard.Static.Reflection;

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
        var unary = (methodSelector.Body as UnaryExpression) ?? throw new ArgumentException("The method selector must point to a method.", nameof(methodSelector));
        var operand = (unary.Operand as MethodCallExpression) ?? throw new ArgumentException("The operand must be MethodCallExpression.");
        var constant = (operand.Object as ConstantExpression) ?? throw new ArgumentException("The operand object must be ConstantExpression.");
        var method = (constant.Value as MethodInfo) ?? throw new ArgumentException("The value of operand object must be MethodInfo.");
        return method;
    }
}
