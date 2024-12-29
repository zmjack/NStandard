using System.Diagnostics;

namespace NStandard;

[DebuggerDisplay("Week {Week}, {Year}")]
public record struct YearWeekPair(int year, int week)
{
    public int Year { get; set; } = year;
    public int Week { get; set; } = week;
}
