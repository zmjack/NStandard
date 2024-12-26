namespace NStandard.Measures;

public interface IMeasureConvertible<T> where T : IMeasurable
{
    bool CanConvert();
    T Convert();
}
