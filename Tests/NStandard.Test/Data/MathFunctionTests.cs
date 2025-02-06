using NStandard.Data.Mathematics;
using Xunit;

namespace NStandard.Data.Test;

public class MathFunctionTests
{
    static Func<int, MathConstant> x = MathConstant.Parameter;
    static Func<MathConstant, IMathFunction> inverse = MathConstant.Inverse;

    [Fact]
    public void Test()
    {
        var y = 2 * x(2) + 2 * x(1) + 1;
        Assert.Equal("2·x^{2} + 2·x + 1", y.ToString());
        var y_1 = inverse(y);
        Assert.Equal("\\frac{1}{2} · \\sqrt{2·x - 1} - \\frac{1}{2}", y_1.ToString());
    }

    [Fact]
    public void Temperature_CtoF_Test()
    {
        var f = (320 + 18 * x(1)) / 10;
        Assert.Equal("\\frac{9}{5}·x + 32", f.ToString());
        var c = inverse(f);
        Assert.Equal("\\frac{5}{9}·x - \\frac{160}{9}", c.ToString());
    }

    [Fact]
    public void Temperature_FtoC_Test()
    {
        var c = (10 * x(1) - 320) / 18;
        Assert.Equal("\\frac{5}{9}·x - \\frac{160}{9}", c.ToString());
        var f = inverse(c);
        Assert.Equal("\\frac{9}{5}·x + 32", f.ToString());
    }
}
