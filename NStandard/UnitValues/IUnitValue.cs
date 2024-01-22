using System;

namespace NStandard.UnitValues;

[Obsolete("Use IMeasurable instead.")]
public interface IUnitValue
{
    string Unit { get; set; }
}

[Obsolete("Use IMeasurable instead.")]
public interface IUnitValue<TValue> : IUnitValue
{
    TValue Value { get; }
    TValue GetValue(string unit);
}
