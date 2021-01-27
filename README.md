# NStandard

**.NET** extension library for system library.

- [English Readme](https://github.com/zmjack/NStandard/blob/master/README.md)
- [中文自述](https://github.com/zmjack/NStandard/blob/master/README-CN.md)

**No depndencies**, and provide some compatibility implementations for older **.NET Framework** versions.

Use this library to simplify your code and make it easier to read.

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

