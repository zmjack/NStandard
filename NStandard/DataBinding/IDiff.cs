namespace NStandard.DataBinding;

public interface IDiff<T>
{
    T OldValue { get; set; }
    T NewValue { get; set; }
    void Overwrite(T value);
}
