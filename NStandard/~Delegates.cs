namespace NStandard
{
    public delegate TOperand SingleOpFunc<TOperand>(TOperand operand);
    public delegate TOperand BinaryOpFunc<TOperand>(TOperand left, TOperand right);
}
