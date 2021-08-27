using System;

namespace NStandard.UnitValues
{
    public interface IUnitValue
    {
        double OriginalValue { get; set; }
        string Unit { get; set; }
        double Value { get; }
        string GetDefaultUnit();
        void Init();
    }
}

namespace NStandard
{
    public static class XIUnitValue
    {
        public static TSelf WithUnit<TSelf>(this TSelf @this, string unit) where TSelf : struct, UnitValues.IUnitValue
        {
            @this.Unit = unit;
            return @this;
        }
    }
}
