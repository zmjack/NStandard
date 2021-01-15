using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace NStandard.Evaluators
{
    public class NumericalEvaluator : Evaluator<Expression, string>
    {
        private static readonly MethodInfo DictionaryGetItemMethod = typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)).GetMethod("get_Item");
        private static readonly MethodInfo DictionaryContainsKeyMethod = typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)).GetMethod("ContainsKey");

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
        protected override Dictionary<(string, string), SingleOpFunc<Expression>> BracketFunctions { get; } = new Dictionary<(string, string), SingleOpFunc<Expression>>
        {
            [("(", ")")] = null,
        };
#endif

        public void Resolve(string exp, out Expression[] operands, out string[] operators, ParameterExpression dictionary)
        {
            // Similar to NumericalRTEvaluator.Resolve

            var operatorsPart = OpFunctions.Keys
                .Concat(BracketFunctions.Keys.Select(x => x.Item1))
                .Concat(BracketFunctions.Keys.Select(x => x.Item2))
                .OrderByDescending(x => x.Length)
                .Select(x => x.RegexReplace(new Regex(@"([\[\]\-\.\^\$\{\}\?\+\*\|\(\)])"), "\\$1"))
                .Join("|");

            // Different from NumericalRTEvaluator.Resolve
            var resolveRegex = new Regex($@"^(?:\s*(\d+|\d+\.\d+|\-\d+|\-\d+\.\d+|0x[\da-fA-F]+|0[0-7]+|\$\{{\w+\}}|)\s*({operatorsPart}|$))+\s*$");

            if (exp.TryResolve(resolveRegex, out var parts))
            {
                operators = parts[2].Where(x => x != "").ToArray();
                operands = parts[1].Take(operators.Length + 1).Select(s =>
                {
                    if (s.IsNullOrWhiteSpace()) return default;

                    Expression ret;

                    if (s.StartsWith("0x")) ret = Expression.Constant((double)Convert.ToInt64(s, 16));
                    else if (s.StartsWith("0")) ret = Expression.Constant((double)Convert.ToInt64(s, 8));
                    else if (s.StartsWith("${") && s.EndsWith("}"))
                    {
                        var name = Expression.Constant(s.Substring(2, s.Length - 3));

                        if (dictionary == null) throw new ArgumentException($"Parameter({name}) is not allowed.");

                        ret = Expression.Condition(
                            Expression.Call(dictionary, DictionaryContainsKeyMethod, name),
                            Expression.Call(dictionary, DictionaryGetItemMethod, name),
                            Expression.Constant(0d));
                    }
                    else ret = Expression.Constant(Convert.ToDouble(s));

                    return ret;
                }).ToArray();
            }
            else throw new ArgumentException($"Invalid expression string( {exp} ).");
        }

        public Expression Build(string exp)
        {
            Resolve(exp, out var operands, out var operators, null);
            var expression = Eval(operands, operators);
            return expression;
        }
        public Expression BuildParameterized(string exp, out ParameterExpression dictionary)
        {
            dictionary = Expression.Parameter(typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)), "p");
            Resolve(exp, out var operands, out var operators, dictionary);
            var expression = Eval(operands, operators);
            return expression;
        }

        public double Eval(string exp) => Compile(exp)();

        public Func<double> Compile(string exp)
        {
            var expression = Build(exp);
            var lambda = Expression.Lambda<Func<double>>(expression);
            var target = lambda.Compile();
            return target;
        }

        public Func<IDictionary<string, double>, double> CompileParameterized(string exp)
        {
            var expression = BuildParameterized(exp, out var dictionary);
            var lambda = Expression.Lambda<Func<IDictionary<string, double>, double>>(expression, dictionary);
            var target = lambda.Compile();
            return target;
        }

    }
}
