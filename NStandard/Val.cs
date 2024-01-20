using System;
using System.Diagnostics;

namespace NStandard;

[DebuggerDisplay("{Target}")]
public struct Val<T>
{
    public T Target { get; }

    [Obsolete("Use Target instead.")]
    public T Any => Target;

    public Val(T target)
    {
        Target = target;
    }

    public static implicit operator Val<T>(T target)
    {
        return new Val<T>(target);
    }
}
