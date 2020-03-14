using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NStandard
{
    public static class Evaluator
    {
        public static readonly NumberEvaluator Numerical = new NumberEvaluator();
    }

    public abstract class Evaluator<TOperator, TOperand>
    {
        public abstract int GetOpLevel(TOperator op);
        public abstract Func<TOperand, TOperand, TOperand> GetOpFunction(TOperator op);
        private readonly Stack<TOperator> OpStack = new Stack<TOperator>();
        private readonly Stack<TOperand> OperandStack = new Stack<TOperand>();

        public bool TryEval(IEnumerable<TOperator> operators, IEnumerable<TOperand> operands, out TOperand result)
        {
            try
            {
                result = Eval(operators, operands);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public TOperand Eval(IEnumerable<TOperator> operators, IEnumerable<TOperand> operands)
        {
            if (operators.Count() != operands.Count()) throw new ArgumentException($"The count of `{nameof(operands)}` and `{nameof(operators)}` must be same.");

            foreach (var pair in Zipper.Create(operators, operands))
            {
                var op = pair.Item1;
                var operand = pair.Item2;

                if (OperandStack.Count > 0)
                {
                    if (OpStack.Count > 0)
                    {
                        while (OpStack.Count > 0 && GetOpLevel(op) >= GetOpLevel(OpStack.Peek()))
                        {
                            var right = OperandStack.Pop();
                            var left = OperandStack.Pop();
                            var tmpResult = GetOpFunction(OpStack.Pop())(left, right);
                            OperandStack.Push(tmpResult);
                        }
                    }
                    OpStack.Push(pair.Item1);
                }
                OperandStack.Push(pair.Item2);
            }

            var result = OperandStack.Pop();
            while (OpStack.Count > 0)
            {
                var left = OperandStack.Pop();
                result = GetOpFunction(OpStack.Pop())(left, result);
            }

            return result;
        }

    }
}
