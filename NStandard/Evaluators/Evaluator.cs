using System;
using System.Collections.Generic;
using System.Linq;

namespace NStandard.Evaluators
{
    public static class Evaluator
    {
        public static readonly NumericalEvaluator Numerical = new();
    }

    public abstract class Evaluator<TOperand, TOperator> where TOperator : class
    {
        protected abstract Dictionary<TOperator, int> OpLevels { get; }
        protected abstract Dictionary<TOperator, BinaryOpFunc<TOperand>> OpFunctions { get; }
#if NET35 || NET40 || NET45 || NET451 || NET46
        protected virtual Dictionary<Tuple<TOperator, TOperator>, SingleOpFunc<TOperand>> BracketFunctions { get; } = new Dictionary<Tuple<TOperator, TOperator>, SingleOpFunc<TOperand>>();
#else
        protected virtual Dictionary<(TOperator, TOperator), SingleOpFunc<TOperand>> BracketFunctions { get; } = new Dictionary<(TOperator, TOperator), SingleOpFunc<TOperand>>();
#endif

        public bool TryEval(IEnumerable<TOperator> operators, IEnumerable<TOperand> operands, out TOperand result)
        {
            try
            {
                result = Eval(operands, operators);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public TOperand Eval(IEnumerable<TOperand> operands, IEnumerable<TOperator> operators)
        {
            var operandCount = operands.Count();

            if (operators.Count() != operandCount - 1) throw new ArgumentException($"The count of `{nameof(operators)}` must be 1 less than `{nameof(operands)}`.");

            var openBrackets = BracketFunctions.Keys.Select(x => x.Item1).ToArray();
            var closeBrackets = BracketFunctions.Keys.Select(x => x.Item2).ToArray();
            var closeToOpenBrackets = BracketFunctions.Keys.ToDictionary(x => x.Item2, x => x.Item1);

            var operandStack = new Stack<TOperand>();
            var opStack = new Stack<TOperator>();
            var skipOperand = false;

            TOperand GetFinalResult()
            {
                var result = operandStack.Pop();
                while (opStack.Count > 0)
                {
                    var op = opStack.Pop();
                    var left = operandStack.Pop();

                    if (openBrackets.Contains(op)) throw new ArgumentException($"Unclosed bracket( {op} ).");

                    result = OpFunctions[op](left, result);
                }
                return result;
            }

            void HandleClose(TOperator closeBracket)
            {
                var openBracket = closeToOpenBrackets[closeBracket];
                var result = operandStack.Pop();
                bool closed = false;
                while (opStack.Count > 0)
                {
                    var op = opStack.Pop();
                    if (op.Equals(openBracket))
                    {
                        operandStack.Pop();
                        closed = true;
                        break;
                    }
                    else
                    {
                        if (openBrackets.Contains(op)) throw new ArgumentException($"Unexpected bracket( {op} ).");

                        var left = operandStack.Pop();
                        result = OpFunctions[op](left, result);
                    }
                }

                if (!closed) throw new ArgumentException($"Unopened bracket( {closeBracket} ).");

#if NET35 || NET40 || NET45 || NET451 || NET46
                var func = BracketFunctions[Tuple.Create(openBracket, closeBracket)];
                if (func != null) result = func(result);
#else
                var func = BracketFunctions[(openBracket, closeBracket)];
                if (func != null) result = func(result);
#endif
                operandStack.Push(result);
                skipOperand = true;
            }

            foreach (var pair in Zipper.Create(operands, operators.Concat(new[] { (TOperator)null })))
            {
                var operand = pair.Item1;
                if (!skipOperand)
                    operandStack.Push(operand);
                else skipOperand = false;

                var op = pair.Item2;
                if (op is null) break;

                if (openBrackets.Contains(op))
                {
                    opStack.Push(op);
                    continue;
                }
                if (closeBrackets.Contains(op))
                {
                    HandleClose(op);
                    continue;
                }

                if (opStack.Count > 0)
                {
                    var opLevel = OpLevels.ContainsKey(op) ? OpLevels[op] : int.MaxValue;
                    for (var prevOp = opStack.Peek(); ; prevOp = opStack.Peek())
                    {
                        if (openBrackets.Contains(prevOp)) break;

                        var prevOpLevel = OpLevels.ContainsKey(prevOp) ? OpLevels[prevOp] : int.MaxValue;

                        // opLevel is less than or equal to prevOpLevel
                        if (opLevel >= prevOpLevel)
                        {
                            var right = operandStack.Pop();
                            var left = operandStack.Pop();
                            var tmpResult = OpFunctions[opStack.Pop()](left, right);
                            operandStack.Push(tmpResult);

                            if (opStack.Count == 0) break;
                        }
                        else break;
                    }
                }
                opStack.Push(op);
            }

            var result = GetFinalResult();
            return result;
        }

    }
}
