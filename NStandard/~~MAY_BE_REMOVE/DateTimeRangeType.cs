using System;
using System.ComponentModel;

namespace NStandard;

[Obsolete("Use DateOnlyType instead.", true)]
[EditorBrowsable(EditorBrowsableState.Never)]
public enum DateTimeRangeType
{
    Unset,
    Year,
    Month,
    Day,
    Hour,
    Minute,
    Second,
}
