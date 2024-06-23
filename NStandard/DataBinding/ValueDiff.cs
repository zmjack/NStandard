using System.Diagnostics;

namespace NStandard.DataBinding;

/// <summary>
/// A value which describe a difference in state.
/// </summary>
/// <typeparam name="T"></typeparam>
[DebuggerDisplay("{OldValue} → {NewValue}")]
public sealed class ValueDiff<T> : IDiff<T?>
{
    public T? OldValue { get; set; }
    public T? NewValue { get; set; }

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
    /// Use <see cref="NewValue"/> to assign to <see cref="OldValue" />, and then use the specified value to assign to <see cref="NewValue"/>.
    /// </summary>
    /// <param name="value"></param>
    public void Overwrite(T? value)
    {
        OldValue = NewValue;
        NewValue = value;
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
