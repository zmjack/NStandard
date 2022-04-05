namespace NStandard.UnitValues
{
    public interface IUnitValue : IInitialize
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
        public static TUnitValue WithUnit<TUnitValue>(this TUnitValue @this, string unit) where TUnitValue : struct, UnitValues.IUnitValue
        {
            @this.Unit = unit;
            return @this;
        }
    }
}
