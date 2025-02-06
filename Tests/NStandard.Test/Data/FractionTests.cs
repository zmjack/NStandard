using NStandard.Data;
using Xunit;

namespace NStandard.Test.Data;

public class FractionTests
{
    [Fact]
    public void PowTest()
    {
        var fraction = new Fraction(2, 3);
        Assert.Equal(Fraction.One, fraction.Pow(0));
        Assert.Equal(fraction, fraction.Pow(1));
        Assert.Equal(new(4, 9), fraction.Pow(2));
        Assert.Equal(new(8, 27), fraction.Pow(3));
    }
}
