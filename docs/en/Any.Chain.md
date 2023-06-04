## Any.Chain

Transform nested loops to a single loop

<br/>

### Nested loop

#### Known nesting times

**Example 1**: 3 nested loops

```csharp
foreach (var i in new[] { "A", "B" })
{
    foreach (var j in new[] { "a", "b" })
    {
        foreach (var k in new[] { "1", "2" })
        {
            Console.WriteLine($"{i}, {j}, {k}");
        }
    }
}
```

> A, a, 1<br/>
> A, a, 2<br/>
> A, b, 1<br/>
> A, b, 2<br/>
> B, a, 1<br/>
> B, a, 2<br/>
> B, b, 1<br/>
> B, b, 2

<br/>

#### Unknown nesting times

**Example 2**: 3 nested loops (Passing through array)

```csharp
var chain = Any.Chain(new[]
{
    new[] { "A", "B" },
    new[] { "a", "b" },
    new[] { "1", "2" },
})
    .Where(x => x.Origin == ChainOrigin.Current);

foreach (var iterator in chain)
{
    var (i, j, k) = iterator.Iterators;
    Console.WriteLine($"{i}, {j}, {k}");
}
```

Different from **Nested Loop**, **Iterators** in **ChainIterator** contains all traversal values (arrays).

This means we can handle nested loops in the same way.

<br/>

The actual situation may be more complicated, but they have the same characteristics:

- Data processing is usually in the innermost layer, because the innermost layer has all the iteration values.
- Other layers may do some special processing, so you need to know exactly when each layer **starts** and **ends**.

**Example 3**: More complex 3 nested loops

```csharp
foreach (var i in new[] { "A", "B" })
{
    // Origin = Begin, Cursor = 0
    Console.WriteLine($"Begin: {i}, ,");
    foreach (var j in new[] { "a", "b" })
    {
        // Origin = Begin, Cursor = 1
        Console.WriteLine($"Begin: {i}, {j},");
        foreach (var k in new[] { "1", "2" })
        {
            // Origin = Current, Cursor = 2
            Console.WriteLine($"{i}, {j}, {k}");
        }
        // Origin = End, Cursor = 1
        Console.WriteLine($"End: {i}, {j},");
    }
    // Origin = End, Cursor = 0
    Console.WriteLine($"End: {i}, ,");
}
```

**Example 4**: More complex 3 nested loops (Any.Chain)

```csharp
var chain = Any.Chain(new[]
{
    new[] { "A", "B" },
    new[] { "a", "b" },
    new[] { "1", "2" },
});

foreach (var iterator in chain)
{
    var (i, j, k) = iterator.Iterators;

    if (iterator.Origin == ChainOrigin.Begin)
    {
        Console.WriteLine($"Begin: {i}, {j},");
    }
    else if (iterator.Origin == ChainOrigin.Current)
    {
        Console.WriteLine($"{i}, {j}, {k}");
    }
    else if (iterator.Origin == ChainOrigin.End)
    {
        Console.WriteLine($"End: {i}, {j},");
    }
}
```

> Begin: A, ,<br/>
> Begin: A, a,<br/>
> A, a, 1<br/>
> A, a, 2<br/>
> End: A, a,<br/>
> Begin: A, b,<br/>
> A, b, 1<br/>
> A, b, 2<br/>
> End: A, b,<br/>
> End: A, ,<br/>
> Begin: B, ,<br/>
> Begin: B, a,<br/>
> B, a, 1<br/>
> B, a, 2<br/>
> End: B, a,<br/>
> Begin: B, b,<br/>
> B, b, 1<br/>
> B, b, 2<br/>
> End: B, b,<br/>
> End: B, ,

<br/>

#### Dynamically generate nested

Generate iterators for each level in turn from the seed value.

**Example 5**: multiplication table

```csharp
var chain = Any.Chain(9, new Func<int, IEnumerable<int>>[]
{
    s => new int[s].Let(i => i + 1),
    i => new int[i].Let(j => j + 1),
});

foreach (var iterator in chain)
{
    var (i, j) = iterator.Iterators;

    if (iterator.Origin == ChainOrigin.Current)
    {
        Console.Write($"{i}x{j}  ");
    }
    else if (iterator.Origin == ChainOrigin.End)
    {
        Console.WriteLine();
    }
}
```

> 1x1 <br/>
> 2x1 2x2 <br/>
> 3x1 3x2 3x3 <br/>
> 4x1 4x2 4x3 4x4 <br/>
> 5x1 5x2 5x3 5x4 5x5 <br/>
> 6x1 6x2 6x3 6x4 6x5 6x6 <br/>
> 7x1 7x2 7x3 7x4 7x5 7x6 7x7 <br/>
> 8x1 8x2 8x3 8x4 8x5 8x6 8x7 8x8 <br/>
> 9x1 9x2 9x3 9x4 9x5 9x6 9x7 9x8 9x9 

