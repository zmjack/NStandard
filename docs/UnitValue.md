# NStandard

**.NET** extension library for system library.

<br/>

## UnitValue

We provide **UnitValue** to support numeric operations using units.

<br/>

For example, the **StorageValue** is a data structure implemented based on **UnitValue**:

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

