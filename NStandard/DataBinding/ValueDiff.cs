using System.Diagnostics;

namespace NStandard.DataBinding;

/// <summary>
/// A value which describe a difference in state.
/// </summary>
/// <typeparam name="T"></typeparam>
[DebuggerDisplay("{OldValue} → {NewValue}")]
public sealed class ValueDiff<T> : IDiff<T>
{
    public T? OldValue { get; set; }
    public T? NewValue { get; set; }
    public T? Value
    {
        get => NewValue;
        set
        {
            OldValue = NewValue;
            NewValue = value;
        }
    }

    public ValueDiff()
    {
    }

    public ValueDiff(T value)
    {
        OldValue = value;
        NewValue = value;
    }

    public ValueDiff(T oldValue, T newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }

    /// <summary>
    /// Creates a shallow copy of the original object.
    /// </summary>
    /// <returns></returns>
    public ValueDiff<T> Clone()
    {
        return new ValueDiff<T>
        {
            OldValue = OldValue,
            NewValue = NewValue,
        };
    }
}
