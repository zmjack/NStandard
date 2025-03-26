namespace LinqSharp;

public static partial class IEnumerableExtensions
{
    public static int MaxOrDefault(this IEnumerable<int> source, int @default = default(int)) => source.Any() ? source.Max() : @default;
}
