using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NStandard
{
    public class NumericalRTEvaluator : Evaluator<double, string>
    {
        protected override Dictionary<string, int> OpLevels { get; } = new Dictionary<string, int>
        {
            ["*"] = 3,
            ["/"] = 3,
            ["%"] = 3,
            ["+"] = 4,
            ["-"] = 4,
        };
        protected override Dictionary<string, BinaryOpFunc<double>> OpFunctions { get; } = new Dictionary<string, BinaryOpFunc<double>>
        {
            ["*"] = (left, right) => left * right,
            ["/"] = (left, right) => left / right,
            ["%"] = (left, right) => left % right,
            ["+"] = (left, right) => left + right,
            ["-"] = (left, right) => left - right,
        };

#if NET35 || NET40 || NET45 || NET451 || NET46
        protected override Dictionary<Tuple<string, string>, SingleOpFunc<double>> BracketFunctions { get; } = new Dictionary<Tuple<string, string>, SingleOpFunc<double>>
        {
            [Tuple.Create("(", ")")] = null,
        };
#else
        protected override Dictionary<(string Item1, string Item2), SingleOpFunc<double>> BracketFunctions { get; } = new Dictionary<(string Item1, string Item2), SingleOpFunc<double>>
        {
            [("(", ")")] = null,
        };
#endif

        public double Eval(string exp)
        {
            var parts = exp.Resolve(new Regex(@"^(?:\s*(|\d+|\d+.\d+|0x[\da-fA-F]+|0[0-7]+)\s*(\+|-|\*|/|%|\(|\)|$))+\s*"));
            var operators = parts[2].Where(x => x != "").ToArray();
            var operands = parts[1].Take(operators.Length + 1).Select(s =>
            {
                if (s.IsNullOrWhiteSpace()) return default;

                return s switch
                {
                    string _ when s.StartsWith("0x") => Convert.ToInt64(s, 16),
                    string _ when s.StartsWith("0") => Convert.ToInt64(s, 8),
                    _ => Convert.ToDouble(s),
                };
            }).ToArray();
            return Eval(operands, operators);
        }
    }
}
