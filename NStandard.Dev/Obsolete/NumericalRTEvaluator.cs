using NStandard.Evaluators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NStandard.Obsolete.Evaluators
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
        protected override Dictionary<(string, string), UnaryOpFunc<double>> BracketFunctions { get; } = new Dictionary<(string, string), UnaryOpFunc<double>>
        {
            [("(", ")")] = null,
        };
#endif
        private readonly string[] RegexSpecialLetters = { "[", "]", "-", ".", "^", "$", "{", "}", "?", "+", "*", "|", "(", ")" };

        public void Resolve(string exp, out double[] operands, out string[] operators)
        {
            // Similar to NumericalEvaluator.Resolve

            var operatorsPart = OpFunctions.Keys
                .Concat(BracketFunctions.Keys.Select(x => x.Item1))
                .Concat(BracketFunctions.Keys.Select(x => x.Item2))
                .OrderByDescending(x => x.Length)
                .Select(x => x.RegexReplace(new Regex(@"([\[\]\-\.\^\$\{\}\?\+\*\|\(\)])"), "\\$1"))
                .Join("|");

            // Different from NumericalEvaluator.Resolve
            var resolveRegex = new Regex($@"^(?:\s*(\d+|\d+\.\d+|\-\d+|\-\d+\.\d+|0x[\da-fA-F]+|0[0-7]+|)\s*({operatorsPart}|$))+\s*$");

            if (exp.TryResolve(resolveRegex, out var parts))
            {
                operators = parts[2].Where(x => x != "").ToArray();
                operands = parts[1].Take(operators.Length + 1).Select(s =>
                {
                    if (s.IsNullOrWhiteSpace()) return default;

                    double ret;

                    if (s.StartsWith("0x")) ret = Convert.ToInt64(s, 16);
                    else if (s.StartsWith("0")) ret = Convert.ToInt64(s, 8);
                    else ret = Convert.ToDouble(s);

                    return ret;
                }).ToArray();
            }
            else throw new ArgumentException($"Invalid expression string( {exp} ).");
        }

        public double Eval(string exp)
        {
            Resolve(exp, out var operands, out var operators);
            var result = Eval(operands, operators);
            return result;
        }
    }
}
