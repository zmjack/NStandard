# NStandard

**.NET** 系统库扩展包。

- [English Readme](https://github.com/zmjack/NStandard/blob/master/README.md)
- [中文自述](https://github.com/zmjack/NStandard/blob/master/README-CN.md)

**完全无依赖**，为 **.NET Framework** 框架提供与 **.Net Core** 部分功能相似的兼容实现。

<br/>

您可以通过使用它来获得以下支持：

- 为 **.NET Framework** 的提供 **.NET Core** 部分新功能兼容性实现。
- 常用数据结构及一些代码定式。

<br/>

支持的目标框架：

| NET                                        | NET Standard                                 | NET Framework                                                |
| ------------------------------------------ | -------------------------------------------- | ------------------------------------------------------------ |
| - .NET 5.0<br />- .NET 6.0<br />- .NET 7.0 | - .NET Standard 2.0<br />- .NET Standard 2.1 | - .NET Framework 3.5<br />- .NET Framework 4.5.1 - 4.5.2<br />- .NET Framework 4.6 - 4.6.2<br />- .NET Framework 4.7 - 4.7.2<br />- .NET Framework 4.8 |

<br/>

## 安装

- **.NET CLI**

  ```powershell
  dotnet add package NStandard
  ```

<br/>

## 最近更新

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

