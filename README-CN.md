# NStandard

**.NET** 系统库扩展包。

- [English Readme](https://github.com/zmjack/NStandard/blob/master/README.md)
- [中文自述](https://github.com/zmjack/NStandard/blob/master/README-CN.md)

**完全无依赖**，且为旧的 **.NET Framework** 框架提供兼容实现。

使用这个库来简化代码，使它更容易阅读。

<br/>

## 安装

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

## 链式扩展函数

- **Then**

  为对象运行任务后，返回它自己。

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

  简化：

  ```csharp
  public class AppSecurity
  {
      public Aes Aes = Aes.Create().Then(x => x.Key = "1234567890123456".Bytes());
  }
  ```

- **For**

  通过指定的转换方法将对象转换为另一个对象。

  ```csharp
  var orderYear = Product.Order.Year;
  var year = orderYear > 2020 ? orderYear : 2020;
  ```

  简化：

  ```csharp
  var year = Product.Order.Year.For(y => y > 2020 ? y : 2020);
  ```

- **Let**

  使用方法初始化数组的每个元素。

  ```csharp
  var arr = new int[5];
  for (int i = 0; i < arr.Length; i++)
  {
      arr[i] = i * 2 + 1;
  }
  ```

  简化：

  ```csharp
  var arr = new int[5].Let(i => i * 2 + 1);
  ```

<br/>

## 基本数据结构扩展

### 扩展: XDateTime

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

- 以及更多 ...

### 静态扩展: DateTimeEx

- **YearDiff** / **ExactYearDiff**

  周期内的完整年数，类似于 **Excel** 中的 **DATEDIF(*， *， "Y")** 函数。

  ```csharp
  DateTimeEx.YearDiff(
      new DateTime(2000, 2, 29),
      new DateTime(2001, 2, 28));      // 0
    
  DateTimeEx.ExactYearDiff(
      new DateTime(2000, 2, 29),       // 365 / 366
      new DateTime(2001, 2, 28));      // 0.9972677595628415
  ```

- **MonthDiff** / **ExactMonthDiff**

  周期内的完整月份数，类似于 **Excel** 中的 **DATEDIF(*， *， "M")** 函数。

  ```csharp
  DateTimeEx.MonthDiff(
      new DateTime(2000, 2, 29),
      new DateTime(2001, 2, 28));      // 11
  
  DateTimeEx.ExactMonthDiff(
      new DateTime(2000, 2, 29),       // 11 + (2 + 28) / 31
      new DateTime(2001, 2, 28));      // 11.967741935483872
  ```

- 以及更多 ...

