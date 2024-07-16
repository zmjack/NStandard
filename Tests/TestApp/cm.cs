using NStandard.Measures;

namespace TestApp;

public partial struct cm : IMeasurable
{
    public string Measure => "cm";
    public decimal Value { get; set; }
}
