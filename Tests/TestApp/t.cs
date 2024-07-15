using NStandard.Measures;

namespace TestApp;

[Measure<kg>(10)]
public partial struct t : IMeasurable
{
    public string Measure => "t";
    public decimal Value { get; set; }
}
