using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace NStandard.Obsolete.Evaluators
{
    public class NumericalEvaluator : Evaluator<Expression, string>
    {
        private static readonly MethodInfo MathPowMethod = typeof(Math).GetMethod("Pow");
        private static readonly MethodInfo MathFloorMethod = typeof(Math).GetMethod("Floor", new[] { typeof(double) });
        private static readonly MethodInfo DictionaryGetItemMethod = typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)).GetMethod("get_Item");
        private static readonly MethodInfo DictionaryContainsKeyMethod = typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)).GetMethod("ContainsKey");
        private static readonly MethodInfo DoubleIsNaNMethod = typeof(double).GetMethod("IsNaN");

        protected override Dictionary<string, int> OpLevels { get; } = new Dictionary<string, int>
        {
            ["**"] = 1,
            ["//"] = 3,
            ["*"] = 3,
            ["/"] = 3,
            ["%"] = 3,
            ["+"] = 4,
            ["-"] = 4,
            [">"] = 6,
            [">="] = 6,
            ["<"] = 6,
            ["<="] = 6,
            ["=="] = 7,
            ["!="] = 7,
            ["and"] = 11,
            ["or"] = 12,
            ["??"] = 13,
            ["?"] = 14,
            [":"] = 15,
        };
        protected override Dictionary<string, BinaryOpFunc<Expression>> OpFunctions { get; } = new Dictionary<string, BinaryOpFunc<Expression>>
        {
            ["**"] = (left, right) => Expression.Call(MathPowMethod, left, right),
            ["//"] = (left, right) => Expression.Call(MathFloorMethod, Expression.Divide(left, right)),
            ["*"] = (left, right) => Expression.MultiplyChecked(left, right),
            ["/"] = (left, right) => Expression.Divide(left, right),
            ["%"] = (left, right) => Expression.Modulo(left, right),
            ["+"] = (left, right) => Expression.AddChecked(left, right),
            ["-"] = (left, right) => Expression.SubtractChecked(left, right),
            [">"] = (left, right) => Expression.Condition(Expression.GreaterThan(left, right), Expression.Constant(1d), Expression.Constant(0d)),
            [">="] = (left, right) => Expression.Condition(Expression.GreaterThanOrEqual(left, right), Expression.Constant(1d), Expression.Constant(0d)),
            ["<"] = (left, right) => Expression.Condition(Expression.LessThan(left, right), Expression.Constant(1d), Expression.Constant(0d)),
            ["<="] = (left, right) => Expression.Condition(Expression.LessThanOrEqual(left, right), Expression.Constant(1d), Expression.Constant(0d)),
            ["=="] = (left, right) => Expression.Condition(Expression.Equal(left, right), Expression.Constant(1d), Expression.Constant(0d)),
            ["!="] = (left, right) => Expression.Condition(Expression.NotEqual(left, right), Expression.Constant(1d), Expression.Constant(0d)),
            ["and"] = (left, right) => Expression.Condition(Expression.Equal(left, Expression.Constant(0d)), left, right),
            ["or"] = (left, right) => Expression.Condition(Expression.NotEqual(left, Expression.Constant(0d)), left, right),
            ["??"] = (left, right) => Expression.Condition(Expression.Call(DoubleIsNaNMethod, left), right, left),
            // Slower code
            //["?"] = (left, right) => Expression.Call(ForMethod, left, Any.Create(() =>
            //{
            //    var param = Expression.Parameter(typeof(double), "x");
            //    var exp = Expression.Condition(Expression.Equal(param, Expression.Constant(0d)), Expression.Constant(double.NaN), right);
            //    return Expression.Lambda<Func<double, double>>(exp, param);
            //})),
            ["?"] = (left, right) => Expression.Condition(Expression.Equal(left, Expression.Constant(0d)), Expression.Constant(double.NaN), right),
            // Slower code
            //[":"] = (left, right) => Expression.Call(ForMethod, left, Any.Create(() =>
            //{
            //    var param = Expression.Parameter(typeof(double), "x");
            //    var exp = Expression.Condition(Expression.Call(DoubleIsNaNMethod, param), right, param);
            //    return Expression.Lambda<Func<double, double>>(exp, param);
            //})),
            [":"] = (left, right) => Expression.Condition(Expression.Call(DoubleIsNaNMethod, left), right, left),
        };
        protected Regex ResolveRegex { get; }

        public NumericalEvaluator()
        {
            var operatorsPart = OpFunctions.Keys
                .Concat(BracketFunctions.Keys.Select(x => x.Item1))
                .Concat(BracketFunctions.Keys.Select(x => x.Item2))
                .OrderByDescending(x => x.Length)
                .Select(x => x.RegexReplace(new Regex(@"([\[\]\-\.\^\$\{\}\?\+\*\|\(\)])"), "\\$1"))
                .Join("|");
            ResolveRegex = new Regex($@"^(?:\s*(\d+|\d+\.\d+|\-\d+|\-\d+\.\d+|0x[\da-fA-F]+|0[0-7]+|NaN|\$\{{[\w ]+\}}|)\s*({operatorsPart}|$))+\s*$");
        }

#if NET35 || NET40 || NET45 || NET451 || NET452 || NET46
        protected override Dictionary<Tuple<string, string>, UnaryOpFunc<Expression>> BracketFunctions { get; } = new Dictionary<Tuple<string, string>, UnaryOpFunc<Expression>>
        {
            [Tuple.Create("(", ")")] = null,
        };
#else
        protected override Dictionary<(string, string), UnaryOpFunc<Expression>> BracketFunctions { get; } = new Dictionary<(string, string), UnaryOpFunc<Expression>>
        {
            [("(", ")")] = null,
        };
#endif

        public void Resolve<TParameterObj>(string exp, out Expression[] operands, out string[] operators, ParameterExpression parameterObj)
        {
            exp ??= "NaN";
            if (exp.TryResolve(ResolveRegex, out var parts))
            {
                operators = parts[2].Where(x => x != "").ToArray();
                operands = parts[1].Take(operators.Length + 1).Select<string, Expression>(s =>
                {
                    if (s.IsNullOrWhiteSpace()) return Expression.Constant(0d);

                    if (s.Contains(".")) return Expression.Constant(Convert.ToDouble(s));
                    else
                    {
                        if (s == "NaN") return Expression.Constant(double.NaN);
                        else if (s.StartsWith("0x")) return Expression.Constant((double)Convert.ToInt64(s, 16));
                        else if (s.StartsWith("0")) return Expression.Constant((double)Convert.ToInt64(s, 8));
                        else if (s.StartsWith("${") && s.EndsWith("}"))
                        {
                            var key = s.Substring(2, s.Length - 3);
                            var name = Expression.Constant(key);
                            if (parameterObj == null) throw new ArgumentException($"No dictionary or object found.");

                            if (typeof(TParameterObj) == typeof(IDictionary<string, double>))
                            {
                                return
                                    Expression.Condition(
                                        Expression.Call(parameterObj, DictionaryContainsKeyMethod, name),
                                        Expression.Call(parameterObj, DictionaryGetItemMethod, name),
                                        Expression.Constant(0d));
                            }
                            else
                            {
                                var propertyOrField = Expression.PropertyOrField(parameterObj, key);
                                if (propertyOrField.Type == typeof(double)) return propertyOrField;
                                if (propertyOrField.Type == typeof(bool))
                                {
                                    return
                                        Expression.Condition(
                                            Expression.Equal(propertyOrField, Expression.Constant(true)),
                                            Expression.Constant(1d), Expression.Constant(0d));
                                }
                                else if (propertyOrField.Type == typeof(bool?))
                                {
                                    return
                                        Expression.Condition(
                                            Expression.Equal(propertyOrField, Expression.Constant(null)),
                                            Expression.Constant(double.NaN),
                                            Expression.Condition(
                                                Expression.Equal(Expression.Convert(propertyOrField, typeof(bool)), Expression.Constant(true)),
                                                Expression.Constant(1d), Expression.Constant(0d)));
                                }
                                else
                                {
                                    if (propertyOrField.Type.IsNullable())
                                    {
                                        return
                                            Expression.Condition(
                                                Expression.Equal(propertyOrField, Expression.Constant(null)),
                                                Expression.Constant(double.NaN),
                                                Expression.Convert(propertyOrField, typeof(double)));
                                    }
                                    else return Expression.Convert(propertyOrField, typeof(double));
                                }
                            }
                        }
                        else return Expression.Constant(Convert.ToDouble(s));
                    }
                }).ToArray();
            }
            else throw new ArgumentException($"Invalid expression string({exp}).");
        }

        public Expression Build(string exp)
        {
            Resolve<object>(exp, out var operands, out var operators, null);
            var expression = Eval(operands, operators);
            return expression;
        }
        public Expression BuildParameterized(string exp, out ParameterExpression dictionary)
        {
            dictionary = Expression.Parameter(typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)), "p");
            Resolve<IDictionary<string, double>>(exp, out var operands, out var operators, dictionary);
            var expression = Eval(operands, operators);
            return expression;
        }
        public Expression BuildParameterized<TParameter>(string exp, out ParameterExpression parameter)
        {
            parameter = Expression.Parameter(typeof(TParameter), "p");
            Resolve<TParameter>(exp, out var operands, out var operators, parameter);
            var expression = Eval(operands, operators);
            return expression;
        }

        public double Eval(string exp) => Compile(exp)();
        public double Eval(string exp, IDictionary<string, double> dict) => CompileParameterized(exp)(dict);

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

        public Func<TParameter, double> CompileParameterized<TParameter>(string exp)
        {
            var expression = BuildParameterized<TParameter>(exp, out var parameter);
            var lambda = Expression.Lambda<Func<TParameter, double>>(expression, parameter);
            var target = lambda.Compile();
            return target;
        }

    }
}
