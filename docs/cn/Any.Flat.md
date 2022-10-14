# NStandard

**.NET** extension library for system library.

<br/>

## Any.Flat

创建一个包含指定多维数组的所有元素的一维枚举。

<br/>

### 扁平化数组

**示例 1** （扁平化多维数组）:

```csharp
var d2 = new string[2, 2]
{
    { "0", "1" },
    { "2", "3" }
};

Console.WriteLine(
    Any.Flat<string>(d2).Join(", ")
);
```

> 0, 1, 2, 3

<br/>

**示例 2**（扁平化多项数组）:

```csharp
var d1_d1 = new[]
{
    new[] { "0", "1" },
    new[] { "2", "3" },
};

Console.WriteLine(
    Any.Flat<string>(d1_d1).Join(", ")
);
```

> 0, 1, 2, 3, 4, 5, 6, 7

<br/>

**示例 3**（扁平化嵌套数组）:

```csharp
var array = new object[]
{
    new[] { "0", "1" },
    new object[]
    {
        "2",
        new object[]
        {
            "3", "4"
        }
    }
};

Console.WriteLine(
    Any.Flat<string>(array).Join(", ")
);
```

> 0, 1, 2, 3, 4

<br/>

### 扁平化非托管数组

**示例 4**（扁平化非托管多维数组）:

```csharp
var d2 = new int[2, 2]
{
    { 0, 1 },
    { 2, 3 }
};
var length = d2.GetSequenceLength();

fixed (int* pd2 = d2)
{
    Console.WriteLine(
        Any.Flat(pd2, length).Join(", ")
    );
}
```

> 0, 1, 2, 3

**示例 5**（扁平化多项非托管数组）:

```csharp
var d1_d1 = new[]
{
    new[] { 0, 1 },
    new[] { 2, 3 },
};
var lengths = d1_d1.Select(x => x.GetSequenceLength()).ToArray();

fixed (int* pd0 = d1_d1[0])
fixed (int* pd1 = d1_d1[1])
{
    Console.WriteLine(
        Any.Flat(new[] { pd0, pd1 }, lengths).Join(", ")
    );
}
```

> 0, 1, 2, 3

<br/>

### 案例

#### 合计每个数组的所有元素

```csharp
var numbers = new[]
{
    new[] { 1, 2, 3 },
    new[] { 4, 5, 6 },
    new[] { 7, 8, 9 },
};

Console.WriteLine(
    Any.Flat<int>(numbers).Sum(x => x)
);
```

>45

<br/>

