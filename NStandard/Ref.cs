using System;
using System.Diagnostics;

namespace NStandard;

[DebuggerDisplay("{Target}")]
public class Ref<T>
{
    public T Target { get; }

    [Obsolete("Use Target instead.")]
    public T Any => Target;

    public Ref(T target)
    {
        Target = target;
    }

    public static implicit operator Ref<T>(T target)
    {
        return new Ref<T>(target);
    }
}
