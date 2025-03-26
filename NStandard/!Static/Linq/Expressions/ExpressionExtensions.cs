using System.Linq.Expressions;
using System.Reflection;

namespace NStandard.Static.Linq.Expressions;

public static class ExpressionEx
{
    /// <summary>
    /// Get setter expression.
    /// </summary>
    public static Expression<Action<T, TProperty>> GetSetterExpression<T, TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression.Body is MemberExpression member)
        {
            var property = (PropertyInfo)member.Member;
            var setMethod = property.GetSetMethod() ?? throw new NotSupportedException($"SetMethod not found. ({expression})");
            var param = Expression.Parameter(typeof(T), "x");
            var prop = Expression.Parameter(typeof(TProperty), "p");
            var setter =
                Expression.Lambda<Action<T, TProperty>>(
                    Expression.Call(param, setMethod, prop),
                    param,
                    prop
                );
            return setter;
        }
        else throw new NotSupportedException("Only MemberExpression is supported.");
    }
}
