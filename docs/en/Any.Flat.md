# NStandard

**.NET** extension library for system library.

<br/>

## Any.Flat

Creates a one-dimensional enumeration containing all elements of the specified multidimensional arrays.

<br/>

### Flat any array

**Example 1** ( Flat multidimensional array ):

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

**Example 2** ( Flat jagged array ):

```csharp
var d1_d1 = new string[2][]
{
    new string[] { "0", "1" },
    new string[] { "2", "3" },
};

Console.WriteLine(
    Any.Flat<string>(d1_d1).Join(", ")
);
```

> 0, 1, 2, 3

<br/>

**Example 3**（Flat jagged multidimensional array）:

```csharp
var d1_d2 = new string[2][,]
{
	new string[2, 2]
	{
		{ "0", "1" },
		{ "2", "3" }
	},
	new string[2, 2]
	{
		{ "4", "5" },
		{ "6", "7" }
	},
};

Console.WriteLine(
	Any.Flat<string>(d1_d2).Join(", ")
);
```

> 0, 1, 2, 3, 4, 5, 6, 7

<br/>

**Example 4** ( Flat nested array ):

```csharp
var array = new object[2]
{
    new string[2] { "0", "1" },
    new object[2]
    {
        "2",
        new string[2]
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

### Flat unmanaged array ( use pointer )

**Example 5** ( Flat unmanaged multidimensional array ):

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

**Example 6** ( Flat unmanaged jagged array ):

```csharp
var d1_d1 = new int[2][]
{
    new int[] { 0, 1 },
    new int[] { 2, 3 },
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

### Cases

#### Sum all the elements of each array

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

