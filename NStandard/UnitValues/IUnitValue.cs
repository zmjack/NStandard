namespace NStandard.UnitValues
{
    public interface IUnitValue
    {
        double OriginalValue { get; set; }
        string Unit { get; set; }
        double Value { get; }
        string GetDefaultUnit();
    }
}
