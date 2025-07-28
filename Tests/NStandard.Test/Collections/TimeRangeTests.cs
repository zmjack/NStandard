using Xunit;

namespace NStandard.Collections.Test;

#if NET6_0_OR_GREATER
public class TimeRangeTests
{
    [Theory]
    [InlineData("08:00", "12:00", "10:00", true)]
    [InlineData("08:00", "12:00", "07:59", false)]
    [InlineData("08:00", "12:00", "12:00", true)]
    [InlineData("08:00", "12:00", "08:00", true)]
    [InlineData("22:00", "06:00", "23:00", true)] // overnight range
    [InlineData("22:00", "06:00", "05:00", true)] // overnight range
    [InlineData("22:00", "06:00", "21:00", false)] // overnight range
    [InlineData("22:00", "06:00", "06:00", true)] // overnight range
    [InlineData("22:00", "06:00", "22:00", true)] // overnight range
    public void Contains_TimeOnly_WorksCorrectly(string start, string end, string time, bool expected)
    {
        var range = new TimeRange(TimeOnly.Parse(start), TimeOnly.Parse(end));
        var testTime = TimeOnly.Parse(time);
        Assert.Equal(expected, range.Contains(testTime));
    }

    [Theory]
    [InlineData("08:00", "12:00", 10, 0, true)]
    [InlineData("08:00", "12:00", 7, 59, false)]
    [InlineData("22:00", "06:00", 23, 0, true)]
    [InlineData("22:00", "06:00", 5, 0, true)]
    [InlineData("22:00", "06:00", 21, 0, false)]
    public void Contains_TimeSpan_WorksCorrectly(string start, string end, int hour, int minute, bool expected)
    {
        var range = new TimeRange(TimeOnly.Parse(start), TimeOnly.Parse(end));
        var testSpan = new TimeSpan(hour, minute, 0);
        Assert.Equal(expected, range.Contains(testSpan));
    }

    [Theory]
    [InlineData("08:00", "12:00", 4, 0, 0)]
    [InlineData("22:00", "06:00", 8, 0, 0)] // End - Start: 06:00 - 22:00 = -16:00, but TimeSpan allows negative
    [InlineData("00:00", "00:00", 0, 0, 0)]
    public void Interval_ReturnsCorrectTimeSpan(string start, string end, int expectedHours, int expectedMinutes, int expectedSeconds)
    {
        var range = new TimeRange(TimeOnly.Parse(start), TimeOnly.Parse(end));
        var expected = new TimeSpan(expectedHours, expectedMinutes, expectedSeconds);
        Assert.Equal(expected, range.Interval);
    }
}
#endif
