# NStandard

**.NET** extension library for system library.

<br/>

## UnitValue

**UnitValue** is used to perform numeric operations with units.

<br/>

For example, the **StorageValue** is a data structure implemented based on **IUnitValue**:

```csharp
var a = StorageValue.Parse(".5 MB");
var b = new StorageValue(512, "KB");
var c = a + b;

var mb = c.GetValue("MB");	// 1
var kb = c.GetValue("KB");	// 1024
```

Support mathematical operations:

- **addition** (+)

  ```csharp
  a + b
  ```

- **subtraction** (-)

  ```csharp
  a - b
  ```

- **multiplication** (*)

  ```csharp
  a * 2
  ```

- **division** (/)

  ```csharp
  a / 2
  ```

- **comparison** ( ==, !=, <, <=, >, >= )

  ```csharp
  a == b
  a != b
  a < b
  a <= b
  a > b
  a >= b
  ```

<br/>

### Add up multiple values

Constructing complex structures is a relatively time-consuming operation. So, it doesn't make sense to add up each number in turn.

We provide a more efficient way (using **QuickSum** function) to handle this situation. 

<br/>

For example, there are many **StorageValues**:

```csharp
var values = new StorageValue[100_000_000].Let(i => new StorageValue(i));
```

then, to calculate their sum:

✔️ CONSIDER (about 1.00 seconds)

```csharp
var sum = new StorageValue();
sum.QuickSum(values);
```

❌ AVOID (about 4.00 seconds)

```csharp
var sum = new StorageValue();
foreach (var value in values)
{
    sum += value;
}
```

#### Using LinqSharp

✔️ CONSIDER (about 1.00 seconds)

```csharp
var sum = values.QSum();
```

❌ AVOID (about 4.00 seconds)

```csharp
var sum = values.Sum();
```

<br/>

