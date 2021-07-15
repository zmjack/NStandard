namespace NStandard.UnitValues
{
    public interface IUnitValue<TSelf, TUnit, TValue> where TSelf : IUnitValue<TSelf, TUnit, TValue>
    {
        TUnit Unit { get; }
        TValue Value { get; }
        bool IsValidUnit(TUnit unit);
        TSelf Format(TUnit unit);
    }
}
