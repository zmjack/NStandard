using NStandard.Data;
using Xunit;

namespace NStandard.Test.Data;

public class MomentTests
{
    [Fact]
    public void DateTimeTest()
    {
        var time = new DateTime(2000, 3, 5, 16, 17, 18, 19, 20);

        Assert.Equal(new DateTime(2000, 3, 5, 16, 17, 18, 19, 0), Moment.From(time, MomentType.Millisecond).ToDateTime());
        Assert.Equal(new DateTime(2000, 3, 5, 16, 17, 18, 0, 0), Moment.From(time, MomentType.Second).ToDateTime());
        Assert.Equal(new DateTime(2000, 3, 5, 16, 17, 0, 0, 0), Moment.From(time, MomentType.Minute).ToDateTime());
        Assert.Equal(new DateTime(2000, 3, 5, 16, 0, 0, 0, 0), Moment.From(time, MomentType.Hour).ToDateTime());
        Assert.Equal(new DateTime(2000, 3, 5, 0, 0, 0, 0, 0), Moment.From(time, MomentType.Day).ToDateTime());
        Assert.Equal(new DateTime(2000, 3, 1, 0, 0, 0, 0, 0), Moment.From(time, MomentType.Month).ToDateTime());
        Assert.Equal(new DateTime(2000, 1, 1, 0, 0, 0, 0, 0), Moment.From(time, MomentType.Year).ToDateTime());

        Assert.Equal(new DateOnly(2000, 3, 5), Moment.From(time, MomentType.Millisecond).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 3, 5), Moment.From(time, MomentType.Second).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 3, 5), Moment.From(time, MomentType.Minute).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 3, 5), Moment.From(time, MomentType.Hour).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 3, 5), Moment.From(time, MomentType.Day).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 3, 1), Moment.From(time, MomentType.Month).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 1, 1), Moment.From(time, MomentType.Year).ToDateOnly());
    }

    [Fact]
    public void DateOnlyTest()
    {
        var time = new DateOnly(2000, 3, 5);

        Assert.Equal(new DateTime(2000, 3, 5, 0, 0, 0, 0, 0), Moment.From(time, MomentType.Millisecond).ToDateTime());
        Assert.Equal(new DateTime(2000, 3, 5, 0, 0, 0, 0, 0), Moment.From(time, MomentType.Second).ToDateTime());
        Assert.Equal(new DateTime(2000, 3, 5, 0, 0, 0, 0, 0), Moment.From(time, MomentType.Minute).ToDateTime());
        Assert.Equal(new DateTime(2000, 3, 5, 0, 0, 0, 0, 0), Moment.From(time, MomentType.Hour).ToDateTime());
        Assert.Equal(new DateTime(2000, 3, 5, 0, 0, 0, 0, 0), Moment.From(time, MomentType.Day).ToDateTime());
        Assert.Equal(new DateTime(2000, 3, 1, 0, 0, 0, 0, 0), Moment.From(time, MomentType.Month).ToDateTime());
        Assert.Equal(new DateTime(2000, 1, 1, 0, 0, 0, 0, 0), Moment.From(time, MomentType.Year).ToDateTime());

        Assert.Equal(new DateOnly(2000, 3, 5), Moment.From(time, MomentType.Millisecond).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 3, 5), Moment.From(time, MomentType.Second).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 3, 5), Moment.From(time, MomentType.Minute).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 3, 5), Moment.From(time, MomentType.Hour).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 3, 5), Moment.From(time, MomentType.Day).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 3, 1), Moment.From(time, MomentType.Month).ToDateOnly());
        Assert.Equal(new DateOnly(2000, 1, 1), Moment.From(time, MomentType.Year).ToDateOnly());
    }
}
