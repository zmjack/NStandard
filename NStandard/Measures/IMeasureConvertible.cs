namespace NStandard.Measures;

public interface IMeasureConvertible<T> where T : IMeasurable
{
    T GetUnderlyingValue();
}
