# NStandard

**.NET** extension library for system library.

<br/>

## Any.Zip

并行迭代几个可迭代对象，为每个对象项目生成元组。

<br/>

例如：

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

由于每个返回项都是 **ValueTuple**，所以它能够自动解构。

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

**Any.Zip** 方法最多支持 **8** 个参数，如果需要更多参数，可以使用嵌套。

```csharp
foreach (var (number, (week, abbrWeek)) in Any.Zip(numbers, Any.Zip(weeks, abbrWeeks)))
{
    Console.WriteLine($"({number}, {week}, {abbrWeek})");
}
```

<br/>

### 案例

#### 合计每列的元素

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

#### 矩阵转置

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

