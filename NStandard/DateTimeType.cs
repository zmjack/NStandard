using System;

namespace NStandard
{
    [Flags]
    public enum DateTimeType
    {
        Unspecified,
        Year = 1,
        Month = 2,
        Day = 4,
        Hour = 8,
        Minute = 16,
        Second = 32,
    }
}
