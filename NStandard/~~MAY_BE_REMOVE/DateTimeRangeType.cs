using System;
using System.ComponentModel;

namespace NStandard
{
    [Obsolete("Use DateOnlyType instead.")]
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
}
