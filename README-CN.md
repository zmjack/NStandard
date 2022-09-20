# NStandard

**.NET** 系统库扩展包。

- [English Readme](https://github.com/zmjack/NStandard/blob/master/README.md)
- [中文自述](https://github.com/zmjack/NStandard/blob/master/README-CN.md)

**完全无依赖**，为 **.NET Framework** 框架提供与 **.Net Core** 部分功能相似的兼容实现。

<br/>

您可以通过使用它来获得以下支持：

- 针对 .NET Framework 的新功能兼容性实现。
- 常用数据结构及一些代码定式。

<br/>

支持的目标框架：

| NET                        | NET Standard                                 | NET Framework                                                |
| -------------------------- | -------------------------------------------- | ------------------------------------------------------------ |
| - .NET 5.0<br />- .NET 6.0 | - .NET Standard 2.0<br />- .NET Standard 2.1 | - .NET Framework 3.5<br />- .NET Framework 4.0<br />- .NET Framework 4.5<br />- .NET Framework 4.5.1<br />- .NET Framework 4.5.2<br />- .NET Framework 4.6 |

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

## 最近更新

### 版本 0.14.0

- 添加 **[Any.Zip](https://github.com/zmjack/NStandard/blob/master/docs/Any.Zip.md)** 函数替代 **Zipper.Create**。

### 版本: 0.13.0

- **破坏性更新**：为使名称更具可读性，扩展方法类名称从 **X...** 更名为 **...Extensions**。

### 版本: 0.8.40

- 为 **.NET Framework** 添加 **HashCode** 结构体，其表现与 **.NET Core** 类似。
- 调整预编译宏，使其更加合理。

<br/>

## 关于正式版本（v1.0.0，未发布）

基础函数：

- [x] [Any.Zip](https://github.com/zmjack/NStandard/blob/master/docs/Any.Zip.md)

扩展函数：

- [ ] [DateTime 扩展](https://github.com/zmjack/NStandard/blob/master/docs/DateTimeExtensions.md)

目前针对以下功能仍需改进：

- [ ] [.NET Framework 兼容性](https://github.com/zmjack/NStandard/blob/master/docs/Compatibility.md)
- [ ] [动态公式计算](https://github.com/zmjack/NStandard/blob/master/docs/Evaluator.md)
- [x] [带单位的数值运算](https://github.com/zmjack/NStandard/blob/master/docs/UnitValue.md)
- [x] [动态规划计算](https://github.com/zmjack/NStandard/blob/master/docs/DpContainer.md)
- [ ] [合并流](https://github.com/zmjack/NStandard/blob/master/docs/SequenceInputStream.md)
- [ ] 更多...

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

