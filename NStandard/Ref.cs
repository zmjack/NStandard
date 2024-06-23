using System;
using System.Diagnostics;

namespace NStandard;

[DebuggerDisplay("{Target}")]
public class Ref<T>(T target)
{
    public T Target { get; } = target;

    [Obsolete("Use Target instead.", true)]
    public T Any => Target;

    public static implicit operator Ref<T>(T target)
    {
        return new Ref<T>(target);
    }
}
