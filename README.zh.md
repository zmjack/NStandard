<h1 align="center">NStandard</h1>

<p align="center">
    <a href="https://www.nuget.org/packages/NStandard" rel="nofollow"><img src="https://img.shields.io/nuget/v/NStandard.svg?logo=nuget&label=NStandard" /></a>
    <a href="https://www.nuget.org/packages/NStandard" rel="nofollow"><img src="https://img.shields.io/nuget/dt/NStandard.svg?logo=nuget&label=Download" /></a>
</p>

**.NET** 系统库扩展包。

- [English Readme](https://github.com/zmjack/NStandard/blob/master/README.md)
- [中文自述](https://github.com/zmjack/NStandard/blob/master/README.zh.md)

**完全无依赖**，为 **.NET Framework** 框架提供与 **.Net Core** 部分功能相似的兼容实现。

<br/>

您可以通过使用它来获得以下支持：

- 为 **.NET Framework** 的提供 **.NET Core** 部分新功能兼容性实现。
- 常用数据结构及一些代码定式。

<br/>

支持的目标框架：

| 目标框架           | 版本                                                         |
| ------------------ | ------------------------------------------------------------ |
| **.NET**           | ![Static Badge](https://img.shields.io/badge/-8.0-8A2BE2) ![Static Badge](https://img.shields.io/badge/-7.0-8A2BE2) ![Static Badge](https://img.shields.io/badge/-6.0-8A2BE2) ![Static Badge](https://img.shields.io/badge/-5.0-8A2BE2) |
| **.NET Standard**  | ![Static Badge](https://img.shields.io/badge/-2.1-orange) ![Static Badge](https://img.shields.io/badge/-2.0-orange) |
| **.NET Framework** | ![Static Badge](https://img.shields.io/badge/-4.8-blue) ![Static Badge](https://img.shields.io/badge/-4.7.2-blue) ![Static Badge](https://img.shields.io/badge/-4.7.1-blue) ![Static Badge](https://img.shields.io/badge/-4.7-blue) ![Static Badge](https://img.shields.io/badge/-4.6.2-blue) ![Static Badge](https://img.shields.io/badge/-4.6.1-blue) ![Static Badge](https://img.shields.io/badge/-4.6-blue) ![Static Badge](https://img.shields.io/badge/-4.5.2-blue) ![Static Badge](https://img.shields.io/badge/-4.5.1-blue) ![Static Badge](https://img.shields.io/badge/-3.5-blue) |

<br/>

## 安装

- **.NET CLI**

  ```powershell
  dotnet add package NStandard
  ```

<br/>

## 最近更新

### 版本：0.57.0

- **IUnitValue** 已过时，请使用 **IMeasurable** 代替。
- **StorageValue** 已过时，请使用 **StorageCapacity** 代替。
- 支持新的度量单位类型：**Measures.Length**、**Measures.Weight**。

### 版本：0.56.1

- 新增结构体：**MinMaxPair**。
- **AsIndexValuePairs** 已重命名为 **Pairs**。

### 版本：0.54.0

- 为 **ValueDiff** 添加一个新方法 **Clone**, 用于创建一个原始对象的浅拷贝副本。

### 版本：0.53.0

- 新增类型 **ValueDiff**，用于描述某个值的变化。

### 版本：0.52.0

- 新增 **MathEx.Ceiling**，用于计算将数字 **向上舍入** 到最接近的有效倍数。
- 新增 **MathEx.Floor**，用于计算将数字 **向下舍入** 到最接近的有效倍数。
- **MathEx.Permutation** 已过时，使用 **MathEx.Permut** 代替。
- **MathEx.Combination** 已过时，使用 **MathEx.Combin** 代替。

### 版本：0.51.0

- 调整了 **JsonXmlSerializer** 解析方法。
- 添加新类型 **Sequence\<T\>**。
- 类型 **Flated\<T\>** 已过时，使用 **Sequence\<T\>**. 代替。

### 版本：0.50.0

- 提供 **JsonXmlSerializer** 将 **JSON** 转换为 **XmlDocument**，或将 **XmlNode** 转换为 **JSON**。

### 版本：0.49.0

- 调整 **EnumOption** 结构。

### 版本：0.48.0

- **中断性更新**：简化包装类型实现 **Ref**（引用包装）。
- **中断性更新**：新增 **Val** 类型（值包装）。
- **中断性更新**：删除 **ValueWrapper**，使用 **Val** 代替。
- **中断性更新**：删除 **VString**（**string** 的值包装类型），使用 **Val\<string\>** 代替。

### 版本：0.45.0

- **中断性更新**：**DateOnlyType** 和 **DateTimeType** 已按 **Flags** 方案重新设计。

### 版本：0.42.0

- 新功能：为 **DateOnly** / **DateTime** / **DateTimeOffset** 提供 **StartOfSeason** / **EndOfSeason** 方法。

### 版本：0.41.0

- 添加了新类型 **HashMap<TKey, TValue>**。

  它是允许使用 **null** 键值的 **IDictionary** 类型。

### 版本：0.39.0

- **DateRangeType** 已重命名为 **DateOnlyType**. 
- **DateTimeRangeType** 已重命名为 **DateTimeType**. 

### 版本：0.38.0

- 为 **.NET 7+** 新增数据结构 **Interval\<T\>**，用于支持区间运算。

### 版本：0.36.0

- 为 **.NET 7+** 提供 **BitConverterEx** 类以对支持 **Int128** 及 **UInt128** 相关方法。

- 提供 **IPAddress** 到 **UInt32 / UInt128** 的计算支持。

  - IPAddressEx.**Create**
  - IPAddressExtensions.**ToUInt32**
  - IPAddressExtensions.**ToUInt128**

- 提供 **.NET 7** 对 **DateTime** / **DateTimeOffset** 中断性更新的兼容性方案：

  **.NET 7** 对 **AddDays** 等 **Ticks** 算法以每 **1 tick** 为单位进行计算；

  **.NET 6** 及以前是以每 **10'000 ticks** 为单位进行计算。

  提供 **ToFixed** 扩展方法，用于使用 **Banker's Rounding** 对 **DateTime** 以每 **10 ticks** 为单位进行取整。

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

### 版本：0.35.0

- 优化 **PatternSearch** 性能，为 **Array** 增加 **Locate(s)** 方法用于查找子序列索引。
- **VariantString** 已重命名为 **Variant**。

### 版本：0.34.0

- 新功能：**Any.Compose**，用于创建合并函数。

### 版本：0.32.0

- 新功能：**[Any.Chain](https://github.com/zmjack/NStandard/blob/master/docs/cn/Any.Chain.md)**，用于将多层嵌套循环转换为单层。

### 版本：0.31.0

- 扩展方法 **Then** / **For** 已重命名为 **Pipe** 。

### 版本：0.22.0

- 新功能：**FixedSizeQueue** 类型.
- **中断性变更**：**SlidingWindow** 已重新设计，并重命名为 **Sliding**。

### 版本：0.21.0

- **中断性变更**：使用 **StructTuple** 替代 **ValueTuple** 以提供更好的兼容性。
- **中断性变更**：使用 **Any.Zip** 替代 **Zipper** 成员方法。
- **中断性变更**：使用 **AsIndexValuePairs** 替代 **AsKeyValuePairs** / **AsKvPairs** 方法。
- **中断性变更**：使用 **Any.Forward** 替代 **ObjectExtension.Forward** 方法。
- **中断性变更**：使用 **Sync** 替代 **SyncLazy** 类型。
- **中断性变更**：已移除类 **LabelValuePair**、**Zipper**、**SyncLazy**。
- 新功能：**[State](https://github.com/zmjack/NStandard/blob/master/docs/cn/State.md)** 类型，用于支持数据绑定。
- 调整预编译宏，使其更加合理。

### 版本：0.17.0

- 新功能：**[Any.Forward](https://github.com/zmjack/NStandard/blob/master/docs/cn/Any.Forward.md)**，深度迭代。
- 新功能：**[ArrayExtensions.Map](https://github.com/zmjack/NStandard/blob/master/docs/cn/ArrayExtensions.md)**，投影所有元素到新类型的数组。

### 版本：0.16.0

- 新功能：**Any.Text.ComputeHashCode**，计算字符串的固定哈希值。

### 版本：0.15.0

- 新功能：**[Any.Flat](https://github.com/zmjack/NStandard/blob/master/docs/cn/Any.Flat.md)**，扁平化数组。
- 新功能：**[Any.ReDim](https://github.com/zmjack/NStandard/blob/master/docs/cn/Any.ReDim.md)**，重新分配数组大小。

### 版本：0.14.0

- 新增 **[Any.Zip](https://github.com/zmjack/NStandard/blob/master/docs/cn/Any.Zip.md)** 函数替代 **Zipper.Create**。

### 版本：0.13.0

- **中断性变更**：为使名称更具可读性，扩展方法类名称从 **X...** 更名为 **...Extensions**。

### 版本：0.8.40

- 为 **.NET Framework** 添加 **HashCode** 结构体，其表现与 **.NET Core** 类似。
- 调整预编译宏，使其更加合理。

<br/>

## 关于正式版本（v1.0.0，未发布）

**<font color=red>`草稿`</font>** ：功能已设计，但不是最终版本。 类成员可能会被重新设计。

**<font color=blue>`定稿`</font>** ：功能已设计且定稿。 除非有重大设计缺陷，否则不会轻易修改。

  ### 实用函数（Any 类）

| 名称                                                         | 描述                                           | 状态                               |
| ------------------------------------------------------------ | ---------------------------------------------- | ---------------------------------- |
| **[Any.Chain](https://github.com/zmjack/NStandard/blob/master/docs/cn/Any.Chain.md)** | 将多层嵌套循环转换为单层链。                   | **<font color=blue>`定稿`</font>** |
| **Any.Create**                                               | 通过指定的函数创建一个实例。                   | **<font color=blue>`定稿`</font>** |
| **[Any.Flat](https://github.com/zmjack/NStandard/blob/master/docs/cn/Any.Flat.md)** | 创建一个包含指定多维数组的所有元素的一维数组。 | **<font color=blue>`定稿`</font>** |
| **[Any.Forward](https://github.com/zmjack/NStandard/blob/master/docs/cn/Any.Forward.md)** | 按路径计算元素并返回。                         | **<font color=blue>`定稿`</font>** |
| **[Any.ReDim](https://github.com/zmjack/NStandard/blob/master/docs/cn/Any.ReDim.md)** | 为数组变量重新分配存储空间。                   | **<font color=blue>`定稿`</font>** |
| **Any.Text.ComputeHashCode**                                 | 为字符串计算固定的哈希值。                     | **<font color=blue>`定稿`</font>** |
| **[Any.Zip](https://github.com/zmjack/NStandard/blob/master/docs/cn/Any.Zip.md)** | 同时迭代多个序列的每个元素。                   | **<font color=blue>`定稿`</font>** |

  ### 时间日期扩展

  - **DateTimeExtensions** 或 **DateTimeOffsetExtensions** 类，跳转到 [**文档**](https://github.com/zmjack/NStandard/blob/master/docs/cn/DateTimeExtensions.md).。

| 名称                    | 描述                                                         | 状态                               |
| ----------------------- | ------------------------------------------------------------ | ---------------------------------- |
| **AddDays**             | 提供计算 **工作日** 或 **非工作日** 的特殊算术方法。         | **<font color=blue>`定稿`</font>** |
| **AddTotalYears**       | 返回一个新的 **DateTime**，它将指定的 **年份差值** 相加到此实例值中。 | **<font color=blue>`定稿`</font>** |
| **AddTotalMonths**      | 返回一个新的 **DateTime**，它将指定的 **月份差值** 相加到此实例值中。 | **<font color=blue>`定稿`</font>** |
| **StartOf** & **EndOf** | 提供 **DateTime** / **DateTimeOffset** 将时间返回到起点或终点的操作。 | **<font color=blue>`定稿`</font>** |
| **Week**                | 获取 **一年** 中指定日期的周数。                             | **<font color=blue>`定稿`</font>** |
| **WeekInMonth**         | 获取指定日期 **一个月** 中的周数。                           | **<font color=blue>`定稿`</font>** |

### 计算容器

| 类名                                                         | 描述               | 状态                              |
| ------------------------------------------------------------ | ------------------ | --------------------------------- |
| **[DpContainer](https://github.com/zmjack/NStandard/blob/master/docs/en/DpContainer.md)** | 提供动态规划功能。 | **<font color=red>`草稿`</font>** |

### 数据绑定

当我们需要一个依赖于其他变量的动态变量时，数据绑定是非常有用的特性。 

它可以帮助我们只维护依赖的值，达到同步变化的效果。

| 类名                                                         | 描述                     | 状态                              |
| ------------------------------------------------------------ | ------------------------ | --------------------------------- |
| **[State](https://github.com/zmjack/NStandard/blob/master/docs/cn/State.md)** | 为实例提供数据绑定功能。 | **<font color=red>`草稿`</font>** |

<br/>

## 管道扩展函数

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

- **Pipe**（无返回值）

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
      public Aes Aes = Aes.Create().Pipe(x => x.Key = "1234567890123456".Bytes());
  }
  ```

- **Pipe**（有返回值）

  通过指定的转换方法将对象转换为另一个对象。

  ```csharp
  var orderYear = Product.Order.Year;
  var year = orderYear > 2020 ? orderYear : 2020;
  ```

  简化：

  ```csharp
  var year = Product.Order.Year.Pipe(y => y > 2020 ? y : 2020);
  ```

<br/>

