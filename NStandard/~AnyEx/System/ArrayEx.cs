namespace NStandard
{
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER ||  NET46_OR_GREATER
#else
    public class ArrayEx
    {
        private static class EmptyArray<T>
        {
#pragma warning disable CA1825 // this is the implementation of Array.Empty<T>()
            internal static readonly T[] Value = new T[0];
#pragma warning restore CA1825
        }

        public static T[] Empty<T>()
        {
            return EmptyArray<T>.Value;
        }
    }
#endif
}
