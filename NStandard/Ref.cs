using System.Diagnostics;

namespace NStandard;

[DebuggerDisplay("[{GetHashCode()}] {Target}")]
public class Ref<T>(T target)
{
    public T Target { get; set; } = target;

    public static implicit operator Ref<T>(T target)
    {
        return new Ref<T>(target);
    }
}
