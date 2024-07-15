using NStandard.Measures;

namespace TestApp;

internal class Program
{
    static void Main(string[] args)
    {
        _km mes = 1;
        var result = (_m)mes + (_m)3;
        Console.WriteLine(result);
    }
}

[Measure<_m>(1000)]
public partial struct _km : IMeasurable
{
    public string Measure => "km";
    public decimal Value { get; set; }
}

[Measure<_dm>(10)]
public partial struct _m : IMeasurable
{
    public string Measure => "m";
    public decimal Value { get; set; }
}

[Measure<_cm>(10)]
public partial struct _dm : IMeasurable
{
    public string Measure => "dm";
    public decimal Value { get; set; }
}

[Measure<_mm>(10)]
public partial struct _cm : IMeasurable
{
    public string Measure => "cm";
    public decimal Value { get; set; }
}

public partial struct _mm : IMeasurable
{
    public string Measure => "mm";
    public decimal Value { get; set; }
}

[Measure<_kg>(1000)]
public partial struct _t : IMeasurable
{
    public string Measure => "t";
    public decimal Value { get; set; }
}

[Measure<_g>(1000)]
public partial struct _kg : IMeasurable
{
    public string Measure => "kg";
    public decimal Value { get; set; }
}

public partial struct _g : IMeasurable
{
    public string Measure => "g";
    public decimal Value { get; set; }
}
