# NStandard

**.NET** extension library for system library.

<br/>

## Any.ReDim

为数组重新分配空间。

<br/>

**示例 1**（减少维度空间）

```csharp
var d2 = new int[3, 3]
{
    { 0, 1, 2 },
    { 3, 4, 5 },
    { 6, 7, 8 }
};
Any.ReDim(ref d2, 2, 2);

for (int i = 0; i < d2.GetLength(0); i++)
{
    var row = new int[d2.GetLength(1)].Let(j => d2[i, j]);
    Console.WriteLine(row.Join(", "));
}
```

> 0, 1<br/>3, 4

<br/>

**示例 2**（扩大维度空间）

```csharp
var d2 = new int[3, 3]
{
    { 0, 1, 2 },
    { 3, 4, 5 },
    { 6, 7, 8 }
};
Any.ReDim(ref d2, 4, 4);

for (int i = 0; i < d2.GetLength(0); i++)
{
    var row = new int[d2.GetLength(1)].Let(j => d2[i, j]);
    Console.WriteLine(row.Join(", "));
}
```

> 0, 1, 2, 0<br/>3, 4, 5, 0<br/>6, 7, 8, 0<br/>0, 0, 0, 0

<br/>

