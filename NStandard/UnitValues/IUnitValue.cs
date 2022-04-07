namespace NStandard.UnitValues
{
    public interface IUnitValue
    {
        string Unit { get; set; }
    }

    public interface IUnitValue<TValue> : IUnitValue
    {
        TValue Value { get; }
        TValue GetValue(string unit);
    }
}

namespace NStandard
{
    public static class XIUnitValue
    {
        public static TUnitValue Unit<TUnitValue>(this TUnitValue @this, string unit) where TUnitValue : struct, UnitValues.IUnitValue
        {
            @this.Unit = unit;
            return @this;
        }
    }
}
