using System.Diagnostics;
using System.Text;

namespace NStandard.Data.Mathematics;

[DebuggerDisplay("{GetText()}")]
public class MathConstant : IMathFunction
{
    public static MathConstant Parameter(int power)
    {
        var values = new Fraction[power + 1];
        values[power] = 1;
        for (int i = 0; i < power; i++)
        {
            values[i] = Fraction.Zero;
        }
        return new MathConstant(values);
    }

    public static IMathFunction Inverse(MathConstant mfunc)
    {
        if (mfunc.Rank == 1)
        {
            var x1 = mfunc.Values[1];
            var c = mfunc.Values[0];
            return new MathConstant([-c / x1, 1 / x1]);
        }
        else if (mfunc.Rank == 2)
        {
            var x2 = mfunc[2];
            var x1 = mfunc[1];
            var c = mfunc[0];

            return
                new MathBinary(MathNodeType.Subtract,
                    new MathBinary(MathNodeType.Multiply,
                        new MathConstant([1 / x2]),
                        new MathSqrt(new MathConstant([x1 * x1 / 4 - x2 * c, x2]), 2)
                    ),
                    new MathConstant([x1 / (2 * x2)])
                );
        }
        else throw new NotImplementedException();
    }

    public MathNodeType NodeType => MathNodeType.Constant;
    public Fraction[] Values { get; set; }
    public int Rank => Values.Length - 1;

    public Fraction this[int index] => Values[index];

    public MathConstant(Fraction[] values)
    {
        Values = new Fraction[values.Length];
        Array.Copy(values, Values, values.Length);
    }

    public static MathConstant operator <<(MathConstant left, int count)
    {
        var values = new Fraction[left.Values.Length + count];
        Array.Copy(left.Values, 0, values, count, left.Values.Length);
        for (int i = 0; i < left.Values.Length - count; i++)
        {
            values[i] = Fraction.Zero;
        }
        return new MathConstant(values);
    }

    public static MathConstant operator *(Fraction left, MathConstant right)
    {
        return right * left;
    }
    public static MathConstant operator *(MathConstant left, Fraction right)
    {
        var values = new Fraction[left.Values.Length];
        for (int i = 0; i < left.Values.Length; i++)
        {
            values[i] = left.Values[i] * right;
        }
        return new MathConstant(values);
    }
    public static MathConstant operator *(MathConstant left, MathConstant right)
    {
        MathConstant mfunc = new(left.Values);
        for (int i = 0; i < right.Values.Length; i++)
        {
            mfunc += ((left * right[i]) << i);
        }
        return mfunc;
    }

    public static MathConstant operator /(MathConstant left, Fraction right)
    {
        var values = new Fraction[left.Values.Length];
        for (int i = 0; i < left.Values.Length; i++)
        {
            values[i] = left.Values[i] / right;
        }
        return new MathConstant(values);
    }

    public static MathConstant operator +(Fraction left, MathConstant right)
    {
        return new MathConstant([left]) + right;
    }
    public static MathConstant operator +(MathConstant left, Fraction right)
    {
        return left + new MathConstant([right]);
    }
    public static MathConstant operator +(MathConstant left, MathConstant right)
    {
        var left_length = left.Values.Length;
        var right_length = right.Values.Length;

        var min = left.Values.Length;
        var max = right.Values.Length;
        var leftMax = left_length > right_length;
        if (leftMax)
        {
            var tmp = min;
            min = max;
            max = tmp;
        }

        var values = new Fraction[max];
        for (int i = 0; i < min; i++)
        {
            values[i] = left.Values[i] + right.Values[i];
        }

        if (leftMax)
        {
            for (int i = min; i < max; i++)
            {
                values[i] = left.Values[i];
            }
        }
        else
        {
            for (int i = min; i < max; i++)
            {
                values[i] = right.Values[i];
            }
        }
        return new MathConstant(values);
    }

    public static MathConstant operator -(Fraction left, MathConstant right)
    {
        return new MathConstant([left]) + right;
    }
    public static MathConstant operator -(MathConstant left, Fraction right)
    {
        return left - new MathConstant([right]);
    }
    public static MathConstant operator -(MathConstant left, MathConstant right)
    {
        var left_length = left.Values.Length;
        var right_length = right.Values.Length;

        var min = left.Values.Length;
        var max = right.Values.Length;
        var leftMax = left_length > right_length;
        if (leftMax)
        {
            var tmp = min;
            min = max;
            max = tmp;
        }

        var values = new Fraction[max];
        for (int i = 0; i < min; i++)
        {
            values[i] = left.Values[i] - right.Values[i];
        }

        if (leftMax)
        {
            for (int i = min; i < max; i++)
            {
                values[i] = left.Values[i];
            }
        }
        else
        {
            for (int i = min; i < max; i++)
            {
                values[i] = right.Values[i];
            }
        }
        return new MathConstant(values);
    }

    private static string GetLaTexString(Fraction fraction)
    {
        var negative = (fraction.Numerator < 0) ^ (fraction.Denominator < 0);
        var numerator = Math.Abs(fraction.Numerator);
        var denominator = Math.Abs(fraction.Denominator);

        if (numerator == 0) return "0";

        numerator = negative ? -numerator : numerator;
        if (denominator == 1)
        {
            return numerator.ToString();
        }
        else return $"\\frac{{{numerator}}}{{{denominator}}}";
    }

    public string GetText()
    {
        var builder = new StringBuilder();
        var enumerator = Values.Reverse().GetEnumerator();

        void Append(Fraction value, int power)
        {
            var str = GetLaTexString(value);
            if (value.Numerator == value.Denominator)
            {
                builder.Append(power switch
                {
                    0 => $"{str}",
                    1 => $"x",
                    _ => $"x^{{{power}}}",
                });
            }
            else
            {
                builder.Append(power switch
                {
                    0 => $"{str}",
                    1 => $"{str}·x",
                    _ => $"{str}·x^{{{power}}}",
                });
            }
        }

        var rank = Rank;
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            if (current.IsZero)
            {
                rank--;
                continue;
            }

            Append(current, rank);
            rank--;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;
                if (current.IsZero)
                {
                    rank--;
                    continue;
                }

                if (current.Numerator < 0)
                {
                    builder.Append($" - ");
                    Append(-current, rank);
                }
                else if (current.Numerator > 0)
                {
                    builder.Append(" + ");
                    Append(current, rank);
                }
                rank--;
            }
        }

        return builder.ToString();
    }

    public override string ToString()
    {
        return GetText();
    }
}
