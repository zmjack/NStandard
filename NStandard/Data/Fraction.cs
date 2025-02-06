using System.Diagnostics;

namespace NStandard.Data;

[DebuggerDisplay("{ToString()}")]
public record struct Fraction(int Numerator, int Denominator = 1)
{
    public static readonly Fraction Zero = new(0, 1);
    public static readonly Fraction One = new(1, 1);

    public bool IsZero => Numerator == 0 && Denominator != 0;

    private static int Gcd(int first, int second)
    {
        while (second != 0)
        {
            int temp = second;
            second = first % second;
            first = temp;
        }
        return first;
    }

    private static bool Negative(int first, int second) => (first ^ second) < 0;

    public float GetSingle() => (float)Numerator / Denominator;
    public double GetDouble() => (double)Numerator / Denominator;
    public decimal GetDecimal() => (decimal)Numerator / Denominator;

    public static Fraction GetSimplify(int numerator, int denominator)
    {
        if (numerator == 0) return Zero;

        var flag = Negative(numerator, denominator);
        numerator = Math.Abs(numerator);
        denominator = Math.Abs(denominator);

        var gcd = Gcd(numerator, denominator);
        numerator = flag ? -numerator : numerator;

        if (gcd <= 1) return new(numerator, denominator);
        return new(numerator / gcd, denominator / gcd);
    }

    public Fraction Simplify() => GetSimplify(Numerator, Denominator);

    public static Fraction operator -(Fraction right)
    {
        return GetSimplify(-right.Numerator, right.Denominator);
    }
    public static Fraction operator +(Fraction left, Fraction right)
    {
        return GetSimplify(
            left.Numerator * right.Denominator + right.Numerator * left.Denominator,
            left.Denominator * right.Denominator
        );
    }
    public static Fraction operator -(Fraction left, Fraction right)
    {
        return GetSimplify(
            left.Numerator * right.Denominator - right.Numerator * left.Denominator,
            left.Denominator * right.Denominator
        );
    }
    public static Fraction operator *(Fraction left, Fraction right)
    {
        return GetSimplify(
            left.Numerator * right.Numerator,
            left.Denominator * right.Denominator
        );
    }
    public static Fraction operator /(Fraction left, Fraction right)
    {
        return GetSimplify(
            left.Numerator * right.Denominator,
            left.Denominator * right.Numerator
        );
    }
    public static implicit operator Fraction(int value) => new(value, 1);

    public Fraction Pow(int exponent)
    {
        if (exponent < 0) throw new ArgumentException("The exponent must be non-negative.", nameof(exponent));

        if (exponent == 0) return One;
        if (exponent == 1) return this;

        var n = Numerator;
        var d = Denominator;
        for (int i = 1; i < exponent; i++)
        {
            n *= Numerator;
            d *= Denominator;
        }
        return new(n, d);
    }

    public override string ToString()
    {
        var flag = Negative(Numerator, Denominator);
        var numerator = Math.Abs(Numerator);
        var denominator = Math.Abs(Denominator);

        if (numerator == 0) return "0";

        numerator = flag ? -numerator : numerator;
        if (denominator == 1)
        {
            return numerator.ToString();
        }
        else return $"{numerator} / {denominator}";
    }
}
