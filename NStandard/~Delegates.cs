namespace NStandard
{
    public delegate TOperand UnaryOpFunc<TOperand>(TOperand operand);
    public delegate TOperand BinaryOpFunc<TOperand>(TOperand left, TOperand right);
}
