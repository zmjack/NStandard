namespace NStandard.UnitValues
{
    public interface IUnitValue
    {
        string Unit { get; set; }
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
