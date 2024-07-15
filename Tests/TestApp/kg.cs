using NStandard.Measures;

namespace TestApp;

public partial struct kg : IMeasurable
{
    public string Measure => "kg";
    public decimal Value { get; set; }
}
