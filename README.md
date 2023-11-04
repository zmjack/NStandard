# NStandard

**.NET** extension library for system library.

- [English Readme](https://github.com/zmjack/NStandard/blob/master/README.md)
- [中文自述](https://github.com/zmjack/NStandard/blob/master/README-CN.md)

**No depndencies**, providing compatible implementations for .NET Framework similar to some of .NET Core functions.

<br/>

You can use it to get the following support:

- Provide compatibility implementation of some new features of **.NET Core** for **.NET Framework**.
- Common data structures and some code patterns.

<br/>

These frameworks are supported:

| NET                                      | NET Standard                                | NET Framework                                                |
| ---------------------------------------- | ------------------------------------------- | ------------------------------------------------------------ |
| - .NET 5.0<br/>- .NET 6.0<br/>- .NET 7.0 | - .NET Standard 2.0<br/>- .NET Standard 2.1 | - .NET Framework 3.5<br />- .NET Framework 4.5.1 - 4.5.2<br />- .NET Framework 4.6 - 4.6.2<br />- .NET Framework 4.7 - 4.7.2<br />- .NET Framework 4.8 |

<br/>

## Install

- **.NET CLI**

  ```powershell
  dotnet add package NStandard
  ```

<br/>

## Recently

### Version 0.52.0

- Added **MathEx.Ceiling** for calculations that **round numbers up** to the nearest multiple of significance.
- Added **MathEx.Floor** for calculations that **round numbers down** to the nearest multiple of significance.
- **MathEx.Permutation** is obsolete, use **MathEx.Permut** instead.
- **MathEx.Combination** is obsolete, use **MathEx.Combin** instead.

### Version 0.51.0

- Adjusted **JsonXmlSerializer** parsing method.
- Add new class **Sequence\<T\>**.
- Class **Flated\<T\>** is obsolete, use **Sequence\<T\>** instead.

### Version 0.50.0

- New feature: Provides **JsonXmlSerializer** to convert **JSON** to **XmlDocument**, or **XmlNode** to **JSON**.

### Version 0.49.0

- Adjust the **EnumOption** structure.

### Version 0.48.0

- **Breaking change**: Simplified wrapper type implementation **Ref** (reference wrapper).
- **Breaking change**: Added **Val** type (value wrapper).
- **Breaking change**: Remove **ValueWrapper**, use **Val** instead.
- **Breaking change**: Remove **VString** (the value wrapper type for **string**), use **Val\<string\>** instead.

### Version 0.45.0

- **Breaking change**: DateOnlyType and DateTimeType redesigned as Flags.

### Version 0.42.0

- New feature: Provide **StartOfSeason** / **EndOfSeason** methods for **DateOnly** / **DateTime** / **DateTimeOffset**.

### Version 0.41.0

- Added new class **HashMap<TKey, TValue>**.

  It is a **IDictionary** type that allows the use of **null** key.

### Version 0.39.0

- **DateRangeType** has been renamed to **DateOnlyType**. 
- **DateTimeRangeType** has been renamed to **DateTimeType**. 

### Version: 0.38.0

- Added new data structure **Interval\<T\>** for **.NET 7+** to support interval operations.

### Version: 0.36.0

- Provide **BitConverterEx** class for **.NET 7+** to support **Int128** and **UInt128** related methods.

- Provide calculation support from **IPAddress** to **UInt32 / UInt128**.

  - IPAddressEx.**Create**
  - IPAddressExtensions.**ToUInt32**
  - IPAddressExtensions.**ToUInt128**

- Provide **.NET 7** compatibility scheme for **DateTime** / **DateTimeOffset** breaking updates:

  **.NET 7** calculates **Ticks** algorithms such as **AddDays** in units of every **1 tick**;

  **.NET 6** and earlier are calculated every **10'000 ticks**.

  Provides **ToFixed** extension method for rounding **DateTime** every **10 ticks** using **Banker's Rounding**.

  ```csharp
  var dt = new DateTime(2000, 1, 1).AddDays(9.2);
  // .NET 6-:
  //      2000/1/10 4:48:00    630830764800000000 ticks
  // .NET 7+:
  //      2000/1/10 4:47:59    630830764799999999 ticks
  
  var dtf = dt.ToFixed();
  // .NET 6-:
  //      2000/1/10 4:48:00    630830764800000000 ticks
  // .NET 7+:
  //      2000/1/10 4:48:00    630830764800000000 ticks
  ```

### Version: 0.35.0

- Optimize **PatternSearch** performance, add **Locate(s)** method to **Array** to find subsequence index.

- **VariantString** has been renamed to **Variant**.

### Version: 0.34.0

- New feature: **Any.Compose**, to create function composition.

### Version: 0.32.0

- New feature: **[Any.Chain](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Chain.md)**, Used to transform nested loops to a single loop.

### Version: 0.31.0

- The extension methods **Then** / **For** has been renamed to **Pipe** .

### Version: 0.22.0

- New feature: class **FixedSizeQueue**.
- **BREAKING CHANGE**: **SlidingWindow** has been redesigned and renamed to **Sliding**。

### Version: 0.21.0

- **BREAKING CHANGE**: Use **StructTuple** instead of **ValueTuple** to provide better compatibility.
- **BREAKING CHANGE**: Use **Any.Zip** instead of **Zipper** member methods.
- **BREAKING CHANGE**: Use **AsIndexValuePairs** instead of **AsKeyValuePairs** / **AsKvPairs** methods.
- **BREAKING CHANGE**: Use **Any.Forward** instead of **ObjectExtension.Forward** method.
- **BREAKING CHANGE**: Use **Sync** instead of **SyncLazy** type.
- **BREAKING CHANGE**: Classes **LabelValuePair**, **Zipper**, **SyncLazy** has been removed.
- New feature: class **[State](https://github.com/zmjack/NStandard/blob/master/docs/en/State.md)**, to support data binding.
- Adjust precompiled macros to make them more reasonable.

### Version: 0.17.0

- New feature: **[Any.Forward](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Forward.md)**, iterate by depth.
- New feature: **[ArrayExtensions.Map](https://github.com/zmjack/NStandard/blob/master/docs/en/ArrayExtensions.md)**, projects all elements to an array of the new type.

### Version: 0.16.0

- New feature: **Any.Text.ComputeHashCode**, Computes a fixed hash of a string.

### Version: 0.15.0

- New feature: **[Any.Flat](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Flat.md)**, flatten array.
- New feature: **[Any.ReDim](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.ReDim.md)**, reallocate array size.

### Version: 0.14.0

- Add **[Any.Zip](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Zip.md)** function instead of **Zipper.Create**.

### Version: 0.13.0

- **BREAKING CHANGE**: To make the name more readable, the extension method class names has been renamed from **X...** to **...Extensions**.

### Version: 0.8.40

- Add struct **HashCode** for **.NET Framework**, which behaves like **.NET Core**.
- Adjust precompiled macros to make them more reasonable.

<br/>

## About the Official Version (v1.0.0, Not Release)

**<font color=red>`Draft`</font>** : Features are designed, but not final. Class members may be redesigned.

**<font color=blue>`Finalized`</font>** : Features are designed and finalized. It will not be easily modified in the future unless there are major design flaws.

### Utility functions

- Class **Any**.

| Name                                                         | Description                                                  | Status                                  |
| ------------------------------------------------------------ | ------------------------------------------------------------ | --------------------------------------- |
| **[Any.Chain](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Chain.md)** | Convert multiple layers of nested loops to a single-layer chain. | **<font color=blue>`Finalized`</font>** |
| **Any.Create**                                               | Creates a instance by the specified function.                | **<font color=blue>`Finalized`</font>** |
| **[Any.Flat](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Flat.md)** | Creates a one-dimensional array containing all elements of the specified multidimensional arrays. | **<font color=blue>`Finalized`</font>** |
| **[Any.Forward](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Forward.md)** | Computes the element by path and returns the element.        | **<font color=blue>`Finalized`</font>** |
| **[Any.ReDim](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.ReDim.md)** | Reallocates storage space for an array variable.             | **<font color=blue>`Finalized`</font>** |
| **Any.Text.ComputeHashCode**                                 | Computes a deterministic hash code for a string.             | **<font color=blue>`Finalized`</font>** |
| **[Any.Zip](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Zip.md)** | Applies a specified function to the corresponding elements of many sequences, producing a sequence of the results. | **<font color=blue>`Finalized`</font>** |

### DateTime Extensions

- Class **DateTimeExtensions** or **DateTimeOffsetExtensions**. ([**Document**](https://github.com/zmjack/NStandard/blob/master/docs/en/DateTimeExtensions.md))

| Name                    | Description                                                  | Status                                  |
| ----------------------- | ------------------------------------------------------------ | --------------------------------------- |
| **AddDays**             | Provides a special arithmetic method for calculating **working days** or **non-working days**. | **<font color=blue>`Finalized`</font>** |
| **AddTotalYears**       | Returns a new **DateTime** that adds the specified **diff-number of years** to the value of this instance. | **<font color=blue>`Finalized`</font>** |
| **AddTotalMonths**      | Returns a new **DateTime** that adds the specified **diff-number of months** to the value of this instance. | **<font color=blue>`Finalized`</font>** |
| **StartOf** & **EndOf** | Provides operations for the **DateTime** / **DateTimeOffset** to return time to a starting point or advance to an end point. | **<font color=blue>`Finalized`</font>** |
| **Week**                | Gets the **number of weeks in a year** for the specified date. | **<font color=blue>`Finalized`</font>** |
| **WeekInMonth**         | Gets the **number of weeks in a month** for the specified date. | **<font color=blue>`Finalized`</font>** |

### Calculation container features

| Class Name                                                   | Description                          | Status                             |
| ------------------------------------------------------------ | ------------------------------------ | ---------------------------------- |
| **[DpContainer](https://github.com/zmjack/NStandard/blob/master/docs/en/DpContainer.md)** | Provides dynamic programing feature. | **<font color=red>`Draft`</font>** |

### Data-binding features

Data binding is very useful feature when we need a dynamic variable that depends on other variables.

It can help us maintain only dependent values and achieve the effect of synchronous changes.

| Class Name                                                   | Description                                | Status                             |
| ------------------------------------------------------------ | ------------------------------------------ | ---------------------------------- |
| **[State](https://github.com/zmjack/NStandard/blob/master/docs/en/State.md)** | Provides data binding feture for instance. | **<font color=red>`Draft`</font>** |



Currently, the following functions still need to be improved:

- [ ] [Compatibility for .NET Framework](https://github.com/zmjack/NStandard/blob/master/docs/en/Compatibility.md)
- [ ] [Dynamic formula calculation](https://github.com/zmjack/NStandard/blob/master/docs/en/Evaluator.md)
- [x] [Numerical operations with units](https://github.com/zmjack/NStandard/blob/master/docs/en/UnitValue.md)
- [x] [Dynamic programming](https://github.com/zmjack/NStandard/blob/master/docs/en/DpContainer.md)
- [ ] [SequenceInputStream](https://github.com/zmjack/NStandard/blob/master/docs/en/SequenceInputStream.md)
- [ ] And more...

<br/>

## Pipelines Extension Functions

- **Let**
  
  Use a method to initialize each element of an array.
  
  ```csharp
  var arr = new int[5];
  for (int i = 0; i < arr.Length; i++)
  {
      arr[i] = i * 2 + 1;
  }
  ```
  
  Simplify:
  
  ```csharp
  var arr = new int[5].Let(i => i * 2 + 1);
  ```

- **Pipe** (No return value)
  
  Run a task for an object, then return itself.
  
  ```csharp
  public class AppSecurity
  {
      public Aes Aes = Aes.Create();
      public AppSecurity()
      {
          Aes.Key = "1234567890123456".Bytes();
      }
  }
  ```
  
  Simplify:
  
  ```csharp
  public class AppSecurity
  {
      public Aes Aes = Aes.Create().Pipe(x => x.Key = "1234567890123456".Bytes());
  }
  ```
  
- **Pipe**（Has return value）

  Casts the object to another object through the specified convert method.

  ```csharp
  var orderYear = Product.Order.Year;
  var year = orderYear > 2020 ? orderYear : 2020;
  ```
  
  Simplify:
  
  ```csharp
  var year = Product.Order.Year.Pipe(y => y > 2020 ? y : 2020);
  ```

<br/>

