using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace NStandard.Evaluators;

public class NumericalEvaluator : EvaluatorBase
{
    private static readonly MethodInfo MathPowMethod = typeof(Math).GetMethod("Pow");
    private static readonly MethodInfo MathFloorMethod = typeof(Math).GetMethod("Floor", [typeof(double)]);
    private static readonly MethodInfo DoubleIsNaNMethod = typeof(double).GetMethod("IsNaN");

    public NumericalEvaluator(bool autoInitialize = true) : base(autoInitialize)
    {
    }

    protected override string DefaultExpression { get; } = "NaN";
    protected override string OperandRegexString { get; } = @"-?\d+\.\d+|-?\d+|0x[\da-fA-F]+|0[0-7]+|NaN";
    protected override Expression OperandToExpression(string operand)
    {
        return operand switch
        {
            "NaN" => Expression.Constant(double.NaN),
            string when operand.StartsWith("0x") => Expression.Constant((double)Convert.ToInt64(operand, 16)),
            string when operand.StartsWith("0") && !operand.Contains(".") => Expression.Constant((double)Convert.ToInt64(operand, 8)),
            _ => Expression.Constant(Convert.ToDouble(operand)),
        };
    }
    protected override string ParameterRegexString { get; } = @"\$\{\w+?\}";
    protected override Func<string, string> GetParameterName => s => s.Substring(2, s.Length - 3);

    protected override Dictionary<string, int> BinaryOpLevels { get; } = new()
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
    protected override Dictionary<string, UnaryFunc<Expression>> UnaryOpFunctions { get; } = new()
    {
        ["not"] = operand => Expression.Condition(Expression.Equal(operand, Expression.Constant(0d)), Expression.Constant(1d), Expression.Constant(0d)),
        ["-"] = operand => Expression.NegateChecked(operand),
        ["+"] = operand => operand,
    };
    protected override Dictionary<string, BinaryFunc<Expression>> BinaryOpFunctions { get; } = new()
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
        ["?"] = (left, right) => Expression.Condition(Expression.Equal(left, Expression.Constant(0d)), Expression.Constant(double.NaN), right),
        [":"] = (left, right) => Expression.Condition(Expression.Call(DoubleIsNaNMethod, left), right, left),
    };

    protected override Dictionary<Bracket, UnaryFunc<double>> BracketFunctions { get; } = new()
    {
        [new("(", ")")] = null,
        [new("abs(", ")")] = x => Math.Abs(x),
        [new("sqrt(", ")")] = x => Math.Sqrt(x),

        [new("ceil(", ")")] = x => Math.Ceiling(x),
        [new("floor(", ")")] = x => Math.Floor(x),

        [new("sin(", ")")] = x => Math.Sin(x),
        [new("cos(", ")")] = x => Math.Cos(x),
        [new("tan(", ")")] = x => Math.Tan(x),

        [new("asin(", ")")] = x => Math.Asin(x),
        [new("acos(", ")")] = x => Math.Acos(x),
        [new("atan(", ")")] = x => Math.Atan(x),

        [new("sinh(", ")")] = x => Math.Sinh(x),
        [new("cosh(", ")")] = x => Math.Cosh(x),
        [new("tanh(", ")")] = x => Math.Tanh(x),
    };
}
