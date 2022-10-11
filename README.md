# NStandard

**.NET** extension library for system library.

- [English Readme](https://github.com/zmjack/NStandard/blob/master/README.md)
- [中文自述](https://github.com/zmjack/NStandard/blob/master/README-CN.md)

**No depndencies**, providing compatible implementations for .NET Framework similar to some of .NET Core functions.

<br/>

You can use it to get the following support:

- Implementation of new feature compatibility for **.NET Framework**.
- Common data structures and some code patterns.

<br/>

These frameworks are supported:

| NET                        | NET Standard                                 | NET Framework                                                |
| -------------------------- | -------------------------------------------- | ------------------------------------------------------------ |
| - .NET 5.0<br />- .NET 6.0 | - .NET Standard 2.0<br />- .NET Standard 2.1 | - .NET Framework 3.5<br />- .NET Framework 4.0<br />- .NET Framework 4.5<br />- .NET Framework 4.5.1<br />- .NET Framework 4.5.2<br />- .NET Framework 4.6 |

<br/>

## Install

- **Package Manager**

  ```powershell
  Install-Package NStandard
  ```

- **.NET CLI**

  ```powershell
  dotnet add package NStandard
  ```

- **PackageReference**

  ```powershell
  <PackageReference Include="NStandard" Version="*" />
  ```

<br/>

## Recently

### Version: 0.15.0

- Add functions: [Any.Flat](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Flat.md), [Any.ReDim](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.ReDim.md).

### Version: 0.14.0

- Add **[Any.Zip](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Zip.md)** function instead of **Zipper.Create**.

### Version: 0.13.0

- **BREAKING CHANGE**: To make the name more readable, the extension method class name has been renamed from **X...** to **...Extensions**.

### Version: 0.8.40

- Add struct **HashCode** for **.NET Framework**, which behaves like **.NET Core**.
- Adjust precompiled macros to make them more reasonable.

<br/>

## About the Official Version (v1.0.0, Not Release)

Functions:

- [x] [Any.Zip](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Zip.md)
  Applies a specified function to the corresponding elements of many sequences, producing a sequence of the results.
- [x] [Any.Flat](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.Flat.md)
  Creates a one-dimensional array containing all elements of the specified multidimensional arrays.
- [x] [Any.ReDim](https://github.com/zmjack/NStandard/blob/master/docs/en/Any.ReDim.md)
  Reallocates storage space for an array variable.

Extensions:

- [ ] [DateTime Extensions](https://github.com/zmjack/NStandard/blob/master/docs/en/DateTimeExtensions.md)

Currently, the following functions still need to be improved:

- [ ] [Compatibility for .NET Framework](https://github.com/zmjack/NStandard/blob/master/docs/en/Compatibility.md)
- [ ] [Dynamic formula calculation](https://github.com/zmjack/NStandard/blob/master/docs/en/Evaluator.md)
- [x] [Numerical operations with units](https://github.com/zmjack/NStandard/blob/master/docs/en/UnitValue.md)
- [x] [Dynamic programming](https://github.com/zmjack/NStandard/blob/master/docs/en/DpContainer.md)
- [ ] [SequenceInputStream](https://github.com/zmjack/NStandard/blob/master/docs/en/SequenceInputStream.md)
- [ ] And more...

<br/>

## Chaining Extension Functions

- **Then**
  
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
      public Aes Aes = Aes.Create().Then(x => x.Key = "1234567890123456".Bytes());
  }
  ```
  
- **For**

  Casts the object to another object through the specified convert method.

  ```csharp
  var orderYear = Product.Order.Year;
  var year = orderYear > 2020 ? orderYear : 2020;
  ```
  
  Simplify:
  
  ```csharp
  var year = Product.Order.Year.For(y => y > 2020 ? y : 2020);
  ```
  
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

<br/>

## Core DS Extensions

### Extension: XDateTime

- **StartOf** / **EndOf**

  ```csharp
  var today = new DateTime(2012, 4, 16, 22, 23, 24);
  today.StartOfYear();        // 2012/1/1 0:00:00
  today.StartOfMonth();       // 2012/4/1 0:00:00
  today.StartOfDay();         // 2012/4/16 0:00:00
  today.EndOfYear();          // 2012/12/31 23:59:59
  today.EndOfMonth();         // 2012/4/30 23:59:59
  today.EndOfDay();           // 2012/4/16 23:59:59
  ```

- **UnixTime**

  ```csharp
  var dt = new DateTime(1970, 1, 1, 16, 0, 0, DateTimeKind.Utc);
  dt.UnixTimeSeconds();           // 57600
  dt.UnixTimeMilliseconds();      // 57600000
  ```

- And more ...

### Static Extension: DateTimeEx

- **YearDiff** / **ExactYearDiff**
  
  The number of complete years in the period, similar as **DATEDIF(*, *, "Y")** function in **Excel**.

  ```csharp
  DateTimeEx.YearDiff(
      new DateTime(2000, 2, 29),
      new DateTime(2001, 2, 28));      // 0
    
  DateTimeEx.ExactYearDiff(
      new DateTime(2000, 2, 29),       // 365 / 366
      new DateTime(2001, 2, 28));      // 0.9972677595628415
  ```
  
- **MonthDiff** / **ExactMonthDiff**
  
  The number of complete months in the period, similar as **DATEDIF(*, *, "M")** function in **Excel**.

  ```csharp
  DateTimeEx.MonthDiff(
      new DateTime(2000, 2, 29),
      new DateTime(2001, 2, 28));      // 11

  DateTimeEx.ExactMonthDiff(
      new DateTime(2000, 2, 29),       // 11 + (2 + 28) / 31
      new DateTime(2001, 2, 28));      // 11.967741935483872
  ```

- And more ...

