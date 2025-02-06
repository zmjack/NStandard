namespace NStandard.Data.Mathematics;

public interface IMathFunction
{
    public MathNodeType NodeType { get; }
    public string GetText();
}
