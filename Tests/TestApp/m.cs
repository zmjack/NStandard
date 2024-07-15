using NStandard.Measures;

namespace TestApp;

[Measure<cm>(100)]
public partial struct m : IMeasurable
{
    public string Measure => "m";
    public decimal Value { get; set; }
}
