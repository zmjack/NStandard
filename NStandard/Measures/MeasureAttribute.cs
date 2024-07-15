using System;

namespace NStandard.Measures;

[AttributeUsage(AttributeTargets.Struct)]
public class MeasureAttribute<T>(double coef) : Attribute
{
    public double Coef { get; set; } = coef;
}
