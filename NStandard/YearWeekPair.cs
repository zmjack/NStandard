using System.Diagnostics;

namespace NStandard;

[DebuggerDisplay("Week {Week}, {Year}")]
public record struct YearWeekPair(int Year, int Week);
