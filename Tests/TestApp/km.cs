using NStandard.Measures;

namespace TestApp;

[Measure<m>(1000)]
public partial struct km : IMeasurable
{
    public string Measure => "km";
    public decimal Value { get; set; }
}
