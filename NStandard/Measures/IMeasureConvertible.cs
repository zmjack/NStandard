namespace NStandard.Measures;

public interface IMeasureConvertible
{
    decimal Value { get; set; }
    string Measure { get; set; }
    T Convert<T>() where T : struct, IMeasurable;
}
