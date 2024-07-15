using NStandard.Measures;

namespace TestApp;

[Measure<mm>(10)]
public partial struct cm : IMeasurable
{
    public string Measure => "cm";
    public decimal Value { get; set; }
}

public partial struct mm : IMeasurable
{
    public string Measure => "mm";
    public decimal Value { get; set; }
}
