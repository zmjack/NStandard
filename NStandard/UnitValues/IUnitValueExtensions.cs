using System;
using System.ComponentModel;

namespace NStandard;

[Obsolete("Plan to remove.")]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IUnitValueExtensions
{
    public static TUnitValue Unit<TUnitValue>(this TUnitValue @this, string unit) where TUnitValue : struct, UnitValues.IUnitValue
    {
        @this.Unit = unit;
        return @this;
    }
}
