namespace NStandard.UnitValues;

public interface IUnitValue
{
    string Unit { get; set; }
}

public interface IUnitValue<TValue> : IUnitValue
{
    TValue Value { get; }
    TValue GetValue(string unit);
}
