namespace LinqSharp;

public static partial class XIEnumerable
{
    public static IEnumerable<TSource> WhereMax<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    {
        if (source.Any())
        {
            var max = source.Max(selector);
            return source.Where(x => selector(x).Equals(max));
        }
        else return source;
    }

}
