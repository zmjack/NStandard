# NStandard

DotNet Core 标准库扩展。



## Flow（流式转换/管道机制）

Flow 提供“由单个类型实例通过一系列转换方法后得到新的类型实例”的实现方案。

---

例如，我们需要设计一个函数实现如下需求：

1. 获取指定字符串的 **UTF8** 的字符数组；
2. 转换该字符数组的 **Base64** 编码。

一般实现：

```c#
var str = "abc";
var bytes = Encoding.UTF8.GetBytes(str);
var result = Convert.ToBase64String(bytes);
```

使用 **Flow** 方法：

```c#
var str = "abc";
var result = str.Flow(Encoding.UTF8.GetBytes, Convert.ToBase64String);
```

使用这样的机制可以使代码更加简洁易读。

----

如果相同转换方法流会应用到多个地方，那么建议使用预构建 **Flow** 类实例来使用。

例如，如上案例可以预定义转换流 **StringFlow.Base64**：

```c#
public static class StringFlow
{
    public static IFlow<string, string> Base64 = new Flow<string, byte[], string>(
        Encoding.UTF8.GetBytes,
        Convert.ToBase64String);
}
```

```c#
var str = "abc";
var result = str.Flow(StringFlow.Base64);
```

**Flow** 实现用于简化序列流式转换编写，提高重用度。其他应用场景，包括加解密场景等同样适用。



## Zipper (多序列融合)

**Zipper** 提供“一种多个独立序列进行同时遍历”的实现方案。

----

例如，求解一组出发日期（starts）和抵达日期（ends）所经历的天数总合：

```c#
var starts = new[] { "2010-9-1", "2012-4-16", "2017-5-1" };
var ends = new[] { "2010-9-2", "2012-4-18", "2017-5-4"};
var result = Zipper.Create(starts, ends, (start, end) =>
                           (DateTime.Parse(end) - DateTime.Parse(start).Days);
```

---

对不同 **.NET** 框架版本支持情况如下：

- **NET35**
  不带选择器方法最多支持 **7** 个序列，返回类型 **Tuple**（迁移代码实现）；
  带选择器方法最多支持 **4** 个序列（**Func** 最多只支持 4 个参数）。
- **NET40**
  不带选择器方法最多支持 **7** 个序列，返回类型 **Tuple**（标准库实现）；
  带选择器方法最多支持 **7** 个序列（**Tuple** 最多只支持 7 个参数）。
- **NETSTANDARD2_0**
  不带选择器方法最多支持 **16** 个序列，返回类型 **ValueTuple**（标准库实现）；
  带选择器方法最多支持 **16** 个序列。

