namespace NStandard.UnitValues
{
    public interface IUnitValue<out TSelf, TUnit, TValue> where TSelf : IUnitValue<TSelf, TUnit, TValue>
    {
        TValue OriginalValue { get; }
        TUnit Unit { get; }
        TValue Value { get; }
        TSelf Set(TValue originalValue, TUnit unit);
        TSelf Format(TUnit unit);
    }
}
