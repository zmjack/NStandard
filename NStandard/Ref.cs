using System.Diagnostics;

namespace NStandard;

[DebuggerDisplay("{Any}")]
public class Ref<T>
{
    public T Any { get; }

    public Ref(T any)
    {
        Any = any;
    }

    public static implicit operator Ref<T>(T any)
    {
        return new Ref<T>(any);
    }
}
