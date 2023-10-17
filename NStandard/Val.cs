using System.Diagnostics;

namespace NStandard
{
    [DebuggerDisplay("{Any}")]
    public struct Val<T>
    {
        public T Any { get; }

        public Val(T any)
        {
            Any = any;
        }

        public static implicit operator Val<T>(T any)
        {
            return new Val<T>(any);
        }
    }
}
