using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

#if !NET35
namespace NStandard
{
    public class NumericalEvaluator : Evaluator<string, Expression>
    {
        protected override Dictionary<string, int> OpLevels { get; } = new Dictionary<string, int>
        {
            ["*"] = 3,
            ["/"] = 3,
            ["%"] = 3,
            ["+"] = 4,
            ["-"] = 4,
        };
        protected override Dictionary<string, Func<Expression, Expression, Expression>> OpFunctions { get; } = new Dictionary<string, Func<Expression, Expression, Expression>>
        {
            ["*"] = (left, right) => Expression.MultiplyChecked(left, right),
            ["/"] = (left, right) => Expression.Divide(left, right),
            ["%"] = (left, right) => Expression.Modulo(left, right),
            ["+"] = (left, right) => Expression.AddChecked(left, right),
            ["-"] = (left, right) => Expression.SubtractChecked(left, right),
        };

#if NET35 || NET40 || NET45 || NET451 || NET46
        protected override Dictionary<Tuple<string, string>, Func<Expression, Expression>> BracketFunctions { get; } = new Dictionary<Tuple<string, string>, Func<Expression, Expression>>
        {
            [Tuple.Create("(", ")")] = n => n,
        };
#else
        protected override Dictionary<(string Item1, string Item2), Func<Expression, Expression>> BracketFunctions { get; } = new Dictionary<(string Item1, string Item2), Func<Expression, Expression>>
        {
            [("(", ")")] = n => n,
        };
#endif

        public Expression Build(string exp, out ParameterExpression[] parameters)
        {
            var paramList = new List<ParameterExpression>();
            var parts = exp.Resolve(new Regex(@"^(?:\s*(|\d+|\d+.\d+|0x[\da-fA-F]+|0[0-7]+|\$\w+)\s*(\+|-|\*|/|%|\(|\)|$))+\s*"));
            var operators = parts[2].Where(x => x != "").ToArray();
            var operands = parts[1].Take(operators.Length + 1).Select(s =>
            {
                if (s.IsNullOrWhiteSpace()) return default;

                Expression ret;

                if (s.StartsWith("0x")) ret = Expression.Constant(Convert.ToInt64(s, 16));
                else if (s.StartsWith("0")) ret = Expression.Constant(Convert.ToInt64(s, 8));
                else if (s.StartsWith("$"))
                {
                    var param = Expression.Parameter(typeof(double), s.Substring(1));
                    paramList.Add(param);
                    ret = param;
                }
                else ret = Expression.Constant(Convert.ToDouble(s));

                return ret;
            }).ToArray();

            var expression = Eval(operands, operators);
            parameters = paramList.ToArray();
            return expression;
        }

        public Delegate Compile(string exp)
        {
            var expression = Build(exp, out var parameters);
            var lambda = Expression.Lambda(expression, parameters);
            var target = lambda.Compile();
            return target;
        }

        public TDelegate Compile<TDelegate>(string exp) where TDelegate : Delegate
        {
            var expression = Build(exp, out var parameters);
            var lambda = Expression.Lambda<TDelegate>(expression, parameters);
            var target = lambda.Compile();
            return target;
        }

    }
}
#endif
