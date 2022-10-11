# NStandard

**.NET** extension library for system library.

<br/>

## UnitValue

**UnitValue** 用于对执行带单位的数值运算。

<br/>

例如，**StorageValue** 是实现了 **IUnitValue** 的结构体：

```csharp
var a = StorageValue.Parse(".5 MB");
var b = new StorageValue(512, "KB");
var c = a + b;

var mb = c.GetValue("MB");	// 1
var kb = c.GetValue("KB");	// 1024
```

支持数学运算：

- **加法** (+)

  ```csharp
  a + b
  ```

- **减法** (-)

  ```csharp
  a - b
  ```

- **乘法** (*)

  ```csharp
  a * 2
  ```

- **除法** (/)

  ```csharp
  a / 2
  ```

- **比较** ( ==, !=, <, <=, >, >= )

  ```csharp
  a == b
  a != b
  a < b
  a <= b
  a > b
  a >= b
  ```

<br/>

### 多值相加

构建复杂结构体是一项相对耗时的操作。 因此，将每个数值依次相加是低效的。

我们提供了更高效的方法（使用 **QuickSum** 函数）来处理这种情况。

<br/>

例如，现在有若干 **StorageValues**:

```csharp
var values = new StorageValue[100_000_000].Let(i => new StorageValue(i));
```

然后，计算他们的和值：

✔️ 考虑使用（约 1.00 秒）

```csharp
var sum = new StorageValue();
sum.QuickSum(values);
```

❌ 避免使用（约 4.00 秒）

```csharp
var sum = new StorageValue();
foreach (var value in values)
{
    sum += value;
}
```

#### Using LinqSharp

✔️ 考虑使用（约 1.00 秒）

```csharp
var sum = values.QSum();
```

❌ 避免使用（约 4.00 秒）

```csharp
var sum = values.Sum();
```

<br/>

