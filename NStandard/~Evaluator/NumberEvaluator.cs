using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NStandard
{
    public class NumberEvaluator : Evaluator<string, double>
    {
        public override Func<double, double, double> GetOpFunction(string op)
        {
            return op switch
            {
                "+" => (left, right) => left + right,
                "-" => (left, right) => left - right,
                "*" => (left, right) => left * right,
                "/" => (left, right) => left / right,
                "%" => (left, right) => left % right,
                _ => throw new NotSupportedException(),
            };
        }

        public override int GetOpLevel(string op)
        {
            return op switch
            {
                "*" => 3,
                "/" => 3,
                "%" => 3,
                "+" => 4,
                "-" => 4,
                _ => throw new NotSupportedException(),
            };
        }

        public double Eval(string exp)
        {
            var parts = exp.Resolve(new Regex(@"^\s*(?:(^|\+|\-|\*|/|%)\s*(\d+|\d+.\d+)\s*)+\s*$"));
            var operators = parts[1].Select(s => s.IsNullOrEmpty() ? default : s);
            var operands = parts[2].Select(s => double.Parse(s));

            return Eval(operators, operands);
        }
    }
}
