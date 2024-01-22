namespace NStandard.Measures;

public interface IMeasurable<TValue>
{
    TValue Value { get; set; }
}
