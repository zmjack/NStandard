# NStandard

**.NET** extension library for system library.

<br/>

## Any.Zip

Iterate over several iterables in parallel, producing tuples with an item from each one.

<br/>

Example:

```csharp
var numbers = new[] { 1, 2, 3 };
var weeks = new[] { "Monday", "Tuesday", "Wednesday" };
var abbrWeeks = new[] { "Mon.", "Tues.", "Wed." };

foreach(var item in Any.Zip(numbers, weeks, abbrweeks))
{
    Console.WriteLine(item);
}
```

> (1, Monday, Mon.)
> (2, Tuesday, Tues.)
> (3, Wednesday, Wed.)

<br/>

Since each item is a **ValueTuple**, it can also be automatically deconstructed.

```csharp
foreach (var (number, week, abbrWeek) in Any.Zip(numbers, weeks, abbrWeeks))
{
    Console.WriteLine($"({number}, {week}, {abbrWeek})");
}
```

> (1, Monday, Mon.)
> (2, Tuesday, Tues.)
> (3, Wednesday, Wed.)

<br/>

The **Any.Zip** method supports up to **eight** parameters, and nesting can be used if more parameters are required.

```csharp
foreach (var (number, (week, abbrWeek)) in Any.Zip(numbers, Any.Zip(weeks, abbrWeeks)))
{
    Console.WriteLine($"({number}, {week}, {abbrWeek})");
}
```

<br/>

### Cases

#### Sum the elements of each column

```csharp
void Main()
{
	var numbers = new[]
	{
		new[] { 1, 2, 3 },
		new[] { 4, 5, 6 },
		new[] { 7, 8, 9 },
	};
	var zip = Any.Zip(numbers, col => col.Sum());
	Print(zip);
}

void Print(IEnumerable<int> numbers)
{
	Console.WriteLine($"[{string.Join(", ", numbers)}]");
}
```

>[12, 15, 18]

<br/>

#### Matrix Transpose

```csharp
void Main()
{
	var numbers = new[]
	{
		new[] { 1, 2, 3 },
		new[] { 4, 5, 6 },
		new[] { 7, 8, 9 },
	};
	
	PrintMatrix(numbers);
	PrintMatrix(Any.Zip(numbers));
}

void PrintMatrix(IEnumerable<IEnumerable<int>> matrix)
{
	Console.WriteLine(
		string.Join(Environment.NewLine,
		(
			matrix.Select(row => $"[{string.Join(", ", row)}]")
		))
	);
	Console.WriteLine();
}
```

>[1, 2, 3]
>[4, 5, 6]
>[7, 8, 9]
>
>[1, 4, 7]
>[2, 5, 8]
>[3, 6, 9]

<br/>

