using System;
using System.Collections.Generic;
using System.Linq;

namespace NStandard
{
    public static class Evaluator
    {
        public static readonly NumberEvaluator Numerical = new NumberEvaluator();
    }

    public abstract class Evaluator<TOperator, TOperand> where TOperator : class
    {
        protected abstract Dictionary<TOperator, int> OpLevels { get; }
        protected abstract Dictionary<TOperator, Func<TOperand, TOperand, TOperand>> OpFunctions { get; }
#if NET35 || NET40 || NET45 || NET451 || NET46
        protected abstract Dictionary<Tuple<TOperator, TOperator>, Func<TOperand, TOperand>> BracketFunctions { get; }
#else
        protected abstract Dictionary<(TOperator Item1, TOperator Item2), Func<TOperand, TOperand>> BracketFunctions { get; }
#endif
        public readonly TOperator[] OpenBrackets;
        public readonly TOperator[] CloseBrackets;
        public readonly Dictionary<TOperator, TOperator> CloseToOpenBrackets;

        public Evaluator()
        {
            OpenBrackets = BracketFunctions.Keys.Select(x => x.Item1).ToArray();
            CloseBrackets = BracketFunctions.Keys.Select(x => x.Item2).ToArray();
            CloseToOpenBrackets = BracketFunctions.Keys.ToDictionary(x => x.Item2, x => x.Item1);
        }

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

            var operandStack = new Stack<TOperand>();
            var opStack = new Stack<TOperator>();
            var bracketStack = new Stack<TOperator>();
            var skipOperand = false;

            TOperand GetFinalResult()
            {
                var result = operandStack.Pop();
                while (opStack.Count > 0)
                {
                    var op = opStack.Pop();
                    var left = operandStack.Pop();
                    result = OpFunctions[op](left, result);
                }
                return result;
            }

            void HandleClose(TOperator closeBracket)
            {
                var openBracket = CloseToOpenBrackets[closeBracket];

                var result = operandStack.Pop();
                while (opStack.Count > 0)
                {
                    var op = opStack.Pop();
                    if (op.Equals(openBracket))
                    {
                        operandStack.Pop();
                        break;
                    }

                    var left = operandStack.Pop();
                    result = OpFunctions[op](left, result);
                }
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

                if (OpenBrackets.Contains(op))
                {
                    opStack.Push(op);
                    continue;
                }
                if (CloseBrackets.Contains(op))
                {
                    HandleClose(op);
                    continue;
                }

                if (opStack.Count > 0)
                {
                    var opLevel = OpLevels[op];
                    for (var prevOp = opStack.Peek(); ; prevOp = opStack.Peek())
                    {
                        if (OpenBrackets.Contains(prevOp)) break;

                        var prevOpLevel = OpLevels[prevOp];
                        if (opLevel >= prevOpLevel)         // opLevel is less than or equal to prevOpLevel
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
