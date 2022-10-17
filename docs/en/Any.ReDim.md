# NStandard

**.NET** extension library for system library.

<br/>

## Any.ReDim

Reallocates storage space for an array variable.

<br/>

**Example 1** ( Shrink the space ):

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

**Example 2** ( Expand space ):

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

