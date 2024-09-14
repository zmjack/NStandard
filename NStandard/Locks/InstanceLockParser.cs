using NStandard;
using NStandard.Flows;
using System.Linq.Expressions;
using System.Threading;

namespace NStandard.Locks;

public class InstanceLockParser<TInstance>
{
    public string LockName { get; }
    public Expression<Func<TInstance, object>>[] Flags { get; protected set; }
    protected Func<TInstance, object>[] FlagLambdas { get; }

    public InstanceLockParser(string lockName, params Expression<Func<TInstance, object>>[] flags)
    {
        LockName = lockName;

        var isAllExpressionValid = flags.All(x =>
        {
            switch (x.Body.NodeType)
            {
                case ExpressionType.Convert:
                    return (x.Body as UnaryExpression)!.Operand.Type.IsBasicType();
                case ExpressionType.MemberAccess:
                    return x.Body.Type.IsBasicType();
                case ExpressionType.Constant:
                    return (x.Body as ConstantExpression)!.Type.IsBasicType();
                default: return false;
            }
        });

        if (!isAllExpressionValid)
            throw new ArgumentException("Every expression's return type must be basic type.");

        Flags = flags;
        FlagLambdas = Flags.Select(x => x.Compile()).ToArray();
    }

    public virtual Lock Parse(TInstance instance)
    {
        return new Lock(string.Intern(
            $"[{StringFlow.UrlEncode(LockName)}<{typeof(TInstance).FullName}>]:" +
            $"{FlagLambdas.Select(f => StringFlow.UrlEncode(f(instance).ToString())).Join(" ")}"));
    }

    public virtual Lock ParseThreadLock(TInstance instance)
    {
        return new Lock(string.Intern(
            $"[({Thread.CurrentThread.ManagedThreadId}){StringFlow.UrlEncode(LockName)}<{typeof(TInstance).FullName}>]:" +
            $"{FlagLambdas.Select(x => StringFlow.UrlEncode(x(instance).ToString())).Join(" ")}"));
    }

}
