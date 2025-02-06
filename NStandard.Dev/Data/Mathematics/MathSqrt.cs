using System.Diagnostics;

namespace NStandard.Data.Mathematics;

[DebuggerDisplay("{GetText()}")]
public class MathSqrt : IMathFunction
{
    public MathNodeType NodeType => MathNodeType.Sqrt;

    public IMathFunction Constant { get; }
    public int Value { get; }

    public MathSqrt(IMathFunction constant, int value)
    {
        Constant = constant;
        Value = value;
    }

    public static MathBinary operator -(MathSqrt left, IMathFunction right)
    {
        return new MathBinary(MathNodeType.Subtract, left, right);
    }

    public string GetText()
    {
        if (Value == 2)
        {
            return $"\\sqrt{{{Constant.GetText()}}}";
        }
        else
        {
            return $"\\sqrt[{-Value}]{{{Constant.GetText()}}}";
        }
    }
    public override string ToString()
    {
        return GetText();
    }
}
