using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace NStandard
{
    public class NumericalEvaluator : Evaluator<Expression, string>
    {
        protected override Dictionary<string, int> OpLevels { get; } = new Dictionary<string, int>
        {
            ["*"] = 3,
            ["/"] = 3,
            ["%"] = 3,
            ["+"] = 4,
            ["-"] = 4,
        };
        protected override Dictionary<string, BinaryOpFunc<Expression>> OpFunctions { get; } = new Dictionary<string, BinaryOpFunc<Expression>>
        {
            ["*"] = (left, right) => Expression.MultiplyChecked(left, right),
            ["/"] = (left, right) => Expression.Divide(left, right),
            ["%"] = (left, right) => Expression.Modulo(left, right),
            ["+"] = (left, right) => Expression.AddChecked(left, right),
            ["-"] = (left, right) => Expression.SubtractChecked(left, right),
        };

#if NET35 || NET40 || NET45 || NET451 || NET46
        protected override Dictionary<Tuple<string, string>, SingleOpFunc<Expression>> BracketFunctions { get; } = new Dictionary<Tuple<string, string>, SingleOpFunc<Expression>>
        {
            [Tuple.Create("(", ")")] = null,
        };
#else
        protected override Dictionary<(string Item1, string Item2), SingleOpFunc<Expression>> BracketFunctions { get; } = new Dictionary<(string Item1, string Item2), SingleOpFunc<Expression>>
        {
            [("(", ")")] = null,
        };
#endif
        private readonly string[] RegexSpecialLetters = { "[", "]", "-", ".", "^", "$", "{", "}", "?", "+", "*", "|", "(", ")" };

        public void Resolve(string exp, out Expression[] operands, out string[] operators, out ParameterExpression[] parameters)
        {
            var operatorsPart = OpFunctions.Keys
                .Concat(BracketFunctions.Keys.Select(x => x.Item1))
                .Concat(BracketFunctions.Keys.Select(x => x.Item2))
                .Select(x => RegexSpecialLetters.Contains(x) ? $@"\{x}" : x)
                .Join("|");

            var paramList = new List<ParameterExpression>();
            if (exp.TryResolve(new Regex($@"^(?:\s*(|\d+|\d+.\d+|0x[\da-fA-F]+|0[0-7]+|\$\w+)\s*({operatorsPart}|$))+\s*"), out var parts))
            {
                operators = parts[2].Where(x => x != "").ToArray();
                operands = parts[1].Take(operators.Length + 1).Select(s =>
                {
                    if (s.IsNullOrWhiteSpace()) return default;

                    Expression ret;

                    if (s.StartsWith("0x")) ret = Expression.Constant((double)Convert.ToInt64(s, 16));
                    else if (s.StartsWith("0")) ret = Expression.Constant((double)Convert.ToInt64(s, 8));
                    else if (s.StartsWith("$"))
                    {
                        var param = Expression.Parameter(typeof(double), s.Substring(1));
                        paramList.Add(param);
                        ret = param;
                    }
                    else ret = Expression.Constant(Convert.ToDouble(s));

                    return ret;
                }).ToArray();
                parameters = paramList.ToArray();
            }
            else throw new ArgumentException($"Invalid expression string({exp}).");
        }

        public Expression Build(string exp, out ParameterExpression[] parameters)
        {
            Resolve(exp, out var operands, out var operators, out parameters);
            var expression = Eval(operands, operators);
            return expression;
        }

        public double Eval(string exp) => Compile<Func<double>>(exp)();

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
