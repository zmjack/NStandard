using System.Diagnostics;

namespace TypeSharp;

[DebuggerDisplay("{IndentValue} = {Value} * {Space}")]
public struct Indent
{
    private int _space;
    public int Space
    {
        readonly get => _space;
        set => _space = value < 0 ? 0 : value;
    }

    private int _value;
    public int Value
    {
        readonly get => _value;
        set => _value = value < 0 ? 0 : value;
    }

    public Indent(int value, int space)
    {
        Value = value;
        Space = space;
    }

    public static Indent operator +(Indent @this, int value)
    {
        return new(@this.Value + value, @this.Space);
    }
    public static Indent operator -(Indent @this, int value)
    {
        return new(@this.Value - value, @this.Space);
    }

    public static Indent operator ++(Indent @this) => @this + 1;
    public static Indent operator --(Indent @this) => @this - 1;

    public int IndentValue => Value * Space;

    private static string GetText(int indentValue)
    {
        return indentValue switch
        {
            <= 0 => "",
            1 => " ",
            2 => "  ",
            3 => "   ",
            4 => "    ",
            _ => $"{GetText(4)}{GetText(indentValue - 4)}",
        };
    }

    public override string ToString()
    {
        return GetText(IndentValue);
    }
}
