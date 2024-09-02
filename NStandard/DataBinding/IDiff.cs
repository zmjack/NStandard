namespace NStandard.DataBinding;

public interface IDiff<T>
{
    T? OldValue { get; set; }
    T? NewValue { get; set; }
    T? Value { get; set; }
}
