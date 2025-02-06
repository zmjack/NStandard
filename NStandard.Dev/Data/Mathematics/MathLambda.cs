using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace NStandard.Data.Mathematics;

[DebuggerDisplay("{GetText()}")]
public class MathLambda : IMathFunction
{
    public MathNodeType NodeType => MathNodeType.Lambda;
    public IMathFunction Function { get; }

    public MathLambda(IMathFunction function)
    {
        Function = function;
    }

    public string GetText()
    {
        return Function.GetText();
    }

    private ConstantExpression GetFractionExpression(Fraction fraction)
    {
        return Expression.Constant((decimal)fraction.Numerator / fraction.Denominator, typeof(decimal));
    }

    private static readonly MethodInfo _pow = typeof(Math).GetMethod("Pow")!;
    private static Expression GetPowExpression(Expression exp, int p)
    {
        return
            Expression.Convert(
                Expression.Call(null, _pow, [
                    Expression.Convert(exp, typeof(double)),
                    Expression.Convert(Expression.Constant(p), typeof(double)),
                ]),
                typeof(decimal)
            );
    }
    private static readonly MethodInfo _sqrt = typeof(Math).GetMethod("Sqrt")!;

    private Expression Build(IMathFunction function, ParameterExpression[] parameters)
    {
        var x = parameters[0]!;
        if (function.NodeType == MathNodeType.Constant)
        {
            var constant = (function as MathConstant)!;

            Expression? exp = null;
            var enumerator = constant.Values.GetEnumerator();

            var p = -1;
            while (enumerator.MoveNext())
            {
                p++;
                var current = (Fraction)enumerator.Current;
                if (current.IsZero) continue;

                if (p == 0)
                {
                    exp = GetFractionExpression(current);
                }
                else
                {
                    exp =
                        Expression.Multiply(
                            GetFractionExpression(current),
                            GetPowExpression(x, p)
                        );
                }

                while (enumerator.MoveNext())
                {
                    p++;
                    current = (Fraction)enumerator.Current;
                    if (current.IsZero) continue;

                    if (p == 1)
                    {
                        exp = Expression.Add(
                            exp,
                            Expression.Multiply(
                                GetFractionExpression(current),
                                x
                            )
                        );
                    }
                    else
                    {
                        exp = Expression.Add(
                            exp,
                            Expression.Multiply(
                                GetFractionExpression(current),
                                GetPowExpression(x, p)
                            )
                        );
                    }
                }
            }
            return exp ?? Expression.Constant(0);
        }
        else if (MathBinary.ValidNodes.Contains(function.NodeType))
        {
            var func = (function as MathBinary)!;
            var left = Build(func.Left, parameters);
            var right = Build(func.Right, parameters);

            return func.NodeType switch
            {
                MathNodeType.Add => Expression.Add(left, right),
                MathNodeType.Subtract => Expression.Subtract(left, right),
                MathNodeType.Multiply => Expression.Multiply(left, right),
                MathNodeType.Divide => Expression.Divide(left, right),
                _ => throw new NotImplementedException(),
            };
        }
        else if (function.NodeType == MathNodeType.Sqrt)
        {
            var func = (function as MathSqrt)!;
            return
                Expression.Convert(
                    Expression.Call(null, _sqrt,
                    [
                        Expression.Convert(Build(func.Constant, parameters), typeof(double))
                    ]),
                    typeof(decimal)
                );
        }
        else throw new NotImplementedException();
    }

    public Expression<Func<decimal, decimal>> Lambda()
    {
        var x = Expression.Parameter(typeof(decimal), "x");
        return Expression.Lambda<Func<decimal, decimal>>(Build(Function, [x]), [x]);
    }

    public Func<decimal, decimal> Compile()
    {
        return Lambda().Compile();
    }

    public override string ToString()
    {
        return GetText();
    }
}
