using System;
using System.ComponentModel;

namespace NStandard;

[Obsolete("Use DateOnlyType instead.", true)]
[EditorBrowsable(EditorBrowsableState.Never)]
public enum DateRangeType
{
    Unset,
    Year,
    Month,
    Day,
}
