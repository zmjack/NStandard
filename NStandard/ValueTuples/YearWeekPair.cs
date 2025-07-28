using System.Diagnostics;

namespace NStandard.ValueTuples;

[DebuggerDisplay("Week {Week}, {Year}")]
public record struct YearWeekPair(int Year, int Week);
