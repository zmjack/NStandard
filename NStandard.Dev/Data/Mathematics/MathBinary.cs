using System.Diagnostics;

namespace NStandard.Data.Mathematics;

[DebuggerDisplay("{GetText()}")]
public class MathBinary : IMathFunction
{
    public MathNodeType NodeType { get; }
    public IMathFunction Left { get; }
    public IMathFunction Right { get; }

    public static readonly MathNodeType[] ValidNodes = [MathNodeType.Add, MathNodeType.Subtract, MathNodeType.Multiply, MathNodeType.Divide];

    public MathBinary(MathNodeType type, IMathFunction left, IMathFunction right)
    {
        if (type == MathNodeType.Add) NodeType = MathNodeType.Add;
        else if (type == MathNodeType.Subtract) NodeType = MathNodeType.Subtract;
        else if (type == MathNodeType.Multiply) NodeType = MathNodeType.Multiply;
        else if (type == MathNodeType.Divide) NodeType = MathNodeType.Divide;
        else throw new NotImplementedException();

        Left = left;
        Right = right;
    }

    public string GetText()
    {
        return NodeType switch
        {
            MathNodeType.Add => $"{Left.GetText()} + {Right.GetText()}",
            MathNodeType.Subtract => $"{Left.GetText()} - {Right.GetText()}",
            MathNodeType.Multiply => $"{Left.GetText()} · {Right.GetText()}",
            MathNodeType.Divide => $"{Left.GetText()} / {Right.GetText()}",
            _ => throw new NotImplementedException(),
        };
    }

    public override string ToString()
    {
        return GetText();
    }
}
