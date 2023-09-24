namespace NStandard
{
    public delegate TOperand UnaryFunc<TOperand>(TOperand operand);
    public delegate TOperand BinaryFunc<TOperand>(TOperand left, TOperand right);
}
