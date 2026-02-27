using NStandard.Design;
using Xunit;

namespace NStandard.Analyzer.Test;

public partial class RoundTests
{
    [RoundFeature]
    public partial struct RoundModel
    {
        [Round(1)]
        public partial float Single { get; set; }

        [Round(1, MidpointRounding.ToEven)]
        public partial double Double { get; set; }

        [Round(1, MidpointRounding.AwayFromZero)]
        public partial decimal Decimal { get; set; }

        [Round(1)]
        public partial float? NullableSingle { get; set; }

        [Round(1, MidpointRounding.ToEven)]
        public partial double? NullableDouble { get; set; }

        [Round(1, MidpointRounding.AwayFromZero)]
        public partial decimal? NullableDecimal { get; set; }
    }

    [Fact]
    public void RoundTest()
    {
        var model = new RoundModel()
        {
            Single = 0.25f,
            Double = 0.25d,
            Decimal = 0.25m,
            NullableSingle = 0.25f,
            NullableDouble = 0.25d,
            NullableDecimal = 0.25m,
        };
        Assert.Equal(0.2f, model.Single);
        Assert.Equal(0.2d, model.Double);
        Assert.Equal(0.3m, model.Decimal);
        Assert.Equal(0.2f, model.NullableSingle);
        Assert.Equal(0.2d, model.NullableDouble);
        Assert.Equal(0.3m, model.NullableDecimal);
    }
}
