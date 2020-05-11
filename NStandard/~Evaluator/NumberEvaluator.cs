using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NStandard
{
    public class NumberEvaluator : Evaluator<string, double>
    {
        protected override Dictionary<string, int> OpLevels { get; } = new Dictionary<string, int>
        {
            ["*"] = 3,
            ["/"] = 3,
            ["%"] = 3,
            ["+"] = 4,
            ["-"] = 4,
        };
        protected override Dictionary<string, Func<double, double, double>> OpFunctions { get; } = new Dictionary<string, Func<double, double, double>>
        {
            ["*"] = (left, right) => left * right,
            ["/"] = (left, right) => left / right,
            ["%"] = (left, right) => left % right,
            ["+"] = (left, right) => left + right,
            ["-"] = (left, right) => left - right,
        };

#if NET35 || NET40 || NET45 || NET451 || NET46
        protected override Dictionary<Tuple<string, string>, Func<double, double>> BracketFunctions { get; } = new Dictionary<Tuple<string, string>, Func<double, double>>
        {
            [Tuple.Create("(", ")")] = n => n,
        };
#else
        protected override Dictionary<(string Item1, string Item2), Func<double, double>> BracketFunctions { get; } = new Dictionary<(string Item1, string Item2), Func<double, double>>
        {
            [("(", ")")] = n => n,
        };
#endif

        public double Eval(string exp)
        {
            //TODO: Maybe use scan to optimize it.
            var parts = exp.Resolve(new Regex(@"^(?:\s*(|\d+|\d+.\d+)\s*(\+|-|\*|/|%|\(|\)|$))+\s*"));
            var operators = parts[2].Where(x => x != "").ToArray();
            var operands = parts[1].Take(operators.Length + 1).Select(s => double.TryParse(s, out var ret) ? ret : default).ToArray();
            return Eval(operands, operators);
        }
    }
}
