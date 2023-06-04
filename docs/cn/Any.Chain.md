## Any.Chain

转换 **嵌套循环** 为 **单层循环**。

<br/>

### 嵌套循环

#### 已知嵌套次数

**示例 1**：3 次嵌套

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

> A, a, 1
> A, a, 2
> A, b, 1
> A, b, 2
> B, a, 1
> B, a, 2
> B, b, 1
> B, b, 2

<br/>

#### 未知嵌套次数

**示例 2**：n 层嵌套（通过数组传递）

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

与 **嵌套循环** 不同的是，**ChainIterator** 中的 **Iterators** 包含了所有的遍历值（数组）。

这意味着我们可以使用同样的方式来处理不定层数嵌套循环。

<br/>

实际情况可能会更复杂，但是这种模式具有相同的特性：

- 数据处理通常在最里层，因为最里层拥有全部的迭代值。
- 其他层级可能会做一些特殊的处理，因此需要明确知道每个层级 **何时开始**，**何时结束**。

<br/>

**示例 3**：更复杂的 3 层嵌套

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

**示例 4**：更复杂的 3 次嵌套（Any.Chain）

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

> Begin: A, ,
> Begin: A, a,
> A, a, 1
> A, a, 2
> End: A, a,
> Begin: A, b,
> A, b, 1
> A, b, 2
> End: A, b,
> End: A, ,
> Begin: B, ,
> Begin: B, a,
> B, a, 1
> B, a, 2
> End: B, a,
> Begin: B, b,
> B, b, 1
> B, b, 2
> End: B, b,
> End: B, ,

<br/>

#### 动态生成嵌套

从种子值为每个层级依次生成迭代器。

**示例 5**：九九乘法表

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

> 1x1 
> 2x1 2x2 
> 3x1 3x2 3x3 
> 4x1 4x2 4x3 4x4 
> 5x1 5x2 5x3 5x4 5x5 
> 6x1 6x2 6x3 6x4 6x5 6x6 
> 7x1 7x2 7x3 7x4 7x5 7x6 7x7 
> 8x1 8x2 8x3 8x4 8x5 8x6 8x7 8x8 
> 9x1 9x2 9x3 9x4 9x5 9x6 9x7 9x8 9x9 

