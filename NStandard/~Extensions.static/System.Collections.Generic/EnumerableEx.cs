namespace NStandard;

public static class EnumerableEx
{
    public static IEnumerable<T> Concat<T>(IEnumerable<IEnumerable<T>> enumerables)
    {
        foreach (var enumerable in enumerables)
            foreach (var item in enumerable)
                yield return item;
    }

    public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] enumerables)
    {
        foreach (var enumerable in enumerables)
            foreach (var item in enumerable)
                yield return item;
    }

    public static IEnumerable<T> Create<T>(int count, Func<int, T> generate)
    {
        for (int i = 0; i < count; i++)
            yield return generate(i);
    }

    public static IEnumerable<T> Create<T>(T start, Func<T, T> generate, Func<T, int, bool> condition)
    {
        int i = 0;
        for (T item = start; condition(item, i); item = generate(item))
        {
            yield return item;
            i++;
        }
    }

    public static IEnumerable<TSelf> CreateFromLinked<TSelf>(TSelf @this, Func<TSelf, TSelf> selector)
    {
        var select = @this;
        while (select is not null)
        {
            yield return select;
            select = selector(select);
        }
    }

}
