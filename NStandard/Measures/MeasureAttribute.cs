using System;

namespace NStandard.Measures;

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class MeasureAttribute<T> : Attribute
{
    public double Coef { get; set; }

    public MeasureAttribute(double coef)
    {
        Coef = coef;
    }
}

[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
public class MeasureAttribute : Attribute
{
    public string Name { get; set; }

    public MeasureAttribute(string name)
    {
        Name = name;
    }
}
