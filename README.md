<h1 align="center">NStandard</h1>

<p align="center">
    <a href="https://www.nuget.org/packages/NStandard" rel="nofollow"><img src="https://img.shields.io/nuget/v/NStandard.svg?logo=nuget&label=NStandard" /></a>
    <a href="https://www.nuget.org/packages/NStandard" rel="nofollow"><img src="https://img.shields.io/nuget/dt/NStandard.svg?logo=nuget&label=Download" /></a>
</p>

**.NET** extension library for system library.

- [English Readme](https://github.com/zmjack/NStandard/blob/master/README.md)
- [中文自述](https://github.com/zmjack/NStandard/blob/master/README.zh.md)

**No depndencies**, providing compatible implementations for .NET Framework similar to some of .NET Core functions.

<br/>

You can use it to get the following support:

- Provide compatibility implementation of some new features of **.NET Core** for **.NET Framework**.
- Common data structures and some code patterns.

<br/>

These frameworks are supported:

| Frameworks         | Versions                                                     |
| ------------------ | ------------------------------------------------------------ |
| **.NET**           | ![Static Badge](https://img.shields.io/badge/-8.0-8A2BE2) ![Static Badge](https://img.shields.io/badge/-7.0-8A2BE2) ![Static Badge](https://img.shields.io/badge/-6.0-8A2BE2) ![Static Badge](https://img.shields.io/badge/-5.0-8A2BE2) |
| **.NET Standard**  | ![Static Badge](https://img.shields.io/badge/-2.1-orange) ![Static Badge](https://img.shields.io/badge/-2.0-orange) |
| **.NET Framework** | ![Static Badge](https://img.shields.io/badge/-4.8-blue) ![Static Badge](https://img.shields.io/badge/-4.7.2-blue) ![Static Badge](https://img.shields.io/badge/-4.7.1-blue) ![Static Badge](https://img.shields.io/badge/-4.7-blue) ![Static Badge](https://img.shields.io/badge/-4.6.2-blue) ![Static Badge](https://img.shields.io/badge/-4.6.1-blue) ![Static Badge](https://img.shields.io/badge/-4.6-blue) ![Static Badge](https://img.shields.io/badge/-4.5.2-blue) ![Static Badge](https://img.shields.io/badge/-4.5.1-blue) ![Static Badge](https://img.shields.io/badge/-3.5-blue) |

<br/>

## Install

- **.NET CLI**

  ```powershell
  dotnet add package NStandard
  ```

<br/>

## Recently

### Version 0.73.0

- Add class **RgbaColor** and provide **Json** serialization.

### Version 0.72.5

- Adjust **IJsonValue** structure.
  - Split

  - ```csharp
    object Value { get; set; }
    ```

  - into 
  
  - ```csharp
    object Value { get; }
    ```
  
  - ```csharp
    JsonElement RawValue { set; }
    ```

### Version 0.72.0

- **Breaking Change**: Adjust **Variant** logic to throw an error when conversion is not possible. 
  - Previously, the default value was returned, which resulted in some erroneous logic not being perceived by users.
- Provide deserialization support for **IJsonValue**.

### Version 0.71.0

- Adjust the **Evaluator** code structure to make it easier to extend.

### Version 0.70.0

- Enable **nullable** checking.
- Fix some potential issues.
- **Breaking Change**: Most of the deprecated features have been marked as errors. Please upgrade to version **0.68.0** and follow the prompts to modify them.

>[!Warning]
>
>Most of the deprecated features have been marked as errors. Please upgrade to version **0.68.0** and follow the prompts to modify them.
>
>**These features will be removed in the next version.**

---

### Version 0.68.0

- Add **JsonValueAttribute\<T\>** to declare the Value of its instance to be used when serializing.

### Version 0.67.0

- Fix **NullReferenceException** when using **State\<string\>**.

### Version 0.66.0

- Change the underlying logic of the **Sequence** class.
- Optimize the classes in the **NStandard.Measures** namespace and change the underlying type to **decimal**.

### Version 0.65.1

- Optimize **MathEx.Permut** / **MathEx.Combin** methods and add overflow checks.

### Version 0.64.6

- Add **JsonImplAttribute\<T\>** to declare interface to be serialized as its instance.
- Add **JsonImplAttribute\<T, TSerialize\>** to declare class to be serialized as the specified interface.
- Remove **JsonImplConverterAttribute\<T\>**, use **JsonImplAttribute\<T\>** instead.

### Version 0.63.2

- Add new class **JsonImplConverter** to use instance types when serializing interfaces to Json.
- Add string extension methods for removing **head** or **tail** substrings from a string.

  - StringExtensions.**TrimStart**(this string @this, string trimString)
  - StringExtensions.**TrimStart**(this string @this, params string[] trimStrings)
  - StringExtensions.**TrimEnd**(this string @this, string trimString)
  - StringExtensions.**TrimEnd**(this string @this, params string[] trimStrings)
- Provide more options for **Sliding**.

### Version 0.62.0

- Add a class **Snowflake128** to generate **Snowflake ID128** or **Guid**.

### Version 0.61.0

- Add a class **Snowflake** to generate **Snowflake ID**.

### Version 0.60.0

- Add a method **PadSlide** to create a **Sliding** with padding.

### Version 0.59.0

- Add a method group to compares two **floats/doubles** if they are similar.
  - Method group: **MathEx.Approximately**.
  - Similar to **[UnityEngine.Mathf.Approximately](https://docs.unity3d.com/ScriptReference/Mathf.Approximately.html)**, but the tolerance can be specified.
  - **False**: `2.4 + 2.4 - 1.2 == 3.6`
  - **True**: `MathEx.Approximately(2.4 + 2.4 - 1.2, 3.6)`

### Version 0.58.0

- Provide singleton interface: **ISingleton\<T\>**.

### Version 0.57.0

- **IUnitValue** is obsolete, use **IMeasurable** instead.
- **StorageValue** is obsolete, use **StorageCapacity** instead.
- New measure classes supported: **Measures.Length**, **Measures.Weight**.

### Version 0.56.1

- Add a new structure: **MinMaxPair**.
- **AsIndexValuePairs** has been renamed to **Pairs**. 

### Version 0.54.0

- Add a new method **Clone** to **ValueDiff**, which is used to create a shallow copy of the original object.

### Version 0.53.0

- Add new class **ValueDiff**, describing the value of the state difference.

### Version 0.52.0

- Add **MathEx.Ceiling** for calculations that **round numbers up** to the nearest multiple of significance.
- Add **MathEx.Floor** for calculations that **round numbers down** to the nearest multiple of significance.
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
- **Breaking change**: Add **Val** type (value wrapper).
- **Breaking change**: Remove **ValueWrapper**, use **Val** instead.
- **Breaking change**: Remove **VString** (the value wrapper type for **string**), use **Val\<string\>** instead.

### Version 0.45.0

- **Breaking change**: DateOnlyType and DateTimeType redesigned as Flags.

### Version 0.42.0

- New feature: Provide **StartOfSeason** / **EndOfSeason** methods for **DateOnly** / **DateTime** / **DateTimeOffset**.

### Version 0.41.0

- Add new class **HashMap<TKey, TValue>**.

  It is a **IDictionary** type that allows the use of **null** key.

### Version 0.39.0

- **DateRangeType** has been renamed to **DateOnlyType**. 
- **DateTimeRangeType** has been renamed to **DateTimeType**. 

### Version: 0.38.0

- Add new data structure **Interval\<T\>** for **.NET 7+** to support interval operations.

### Version: 0.36.0

- Provide **BitConverterEx** class for **.NET 7+** to support **Int128** and **UInt128** related methods.

- Provide calculation support from **IPAddress** to **UInt32 / UInt128**.

  - IPAddressEx.**Create**
  - IPAddressExtensions.**ToUInt32**
  - IPAddressExtensions.**ToUInt128**

- Provide **.NET 7** compatibility scheme for **DateTime** / **DateTimeOffset** breaking updates:

  **.NET 7** calculates **Ticks** algorithms such as **AddDays** in units of every **1 tick**;

  **.NET 6** and earlier are calculated every **10'000 ticks**.

  Provide **ToFixed** extension method for rounding **DateTime** every **10 ticks** using **Banker's Rounding**.

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

