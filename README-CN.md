# NStandard

DotNet 标准库扩展。

- [English Readme](https://github.com/zmjack/NStandard/blob/master/README.md)
- [中文自述](https://github.com/zmjack/NStandard/blob/master/README-CN.md)

<br/>

## 设计模式

### Scope 范围影响设计

Scope 类型提供了范围影响设计方案。范围内的任意部分能够获取并使用范围变量，以此来影响部分代码行为。

---

假设数据库交互案例：

当使用 **MockSaveChanges** 方法时，如果没有指定事务，则新建事务使用；否则使用指定事务。

```c#
private class FakeTransaction : Scope<FakeTransaction>
{
    public string Name { get; private set; }
    public FakeTransaction(string name) => Name = name;
}

private string MockSaveChanges()
{
    return FakeTransaction.Current?.Name ?? "[New transaction]";
}

[Fact]
public void NotScopedTest()
{
    var ret = MockSaveChanges();
    Assert.Equal("[New transaction]", ret);
}

[Fact]
public void ScopedTest()
{
    using (new FakeTransaction("Transaction 1"))
    {
        var ret = MockSaveChanges();
        Assert.Equal("Transaction 1", ret);
    }
}
```

**FakeTransaction** 类是假定的事务容器；**MockSaveChanges** 方法是假定的提交方法。

**MockSaveChanges** 方法会判断自己是否处于事务容器范围定义中，并以此来进行不同的执行行为。

<br/>

### Flow（流式转换/管道机制）

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

<br/>

## 快速计算

### Zipper (多序列融合)

**Zipper** 提供“一种多个独立序列进行同时遍历”的实现方案。

----

例如，求解一组出发日期（starts）和抵达日期（ends）所经历的天数总合：

```c#
var starts = new[] { "2010-9-1", "2012-4-16", "2017-5-1" };
var ends = new[] { "2010-9-2", "2012-4-18", "2017-5-4"};
var result = Zipper.Create(starts, ends, 
	(start, end) => (DateTime.Parse(end) - DateTime.Parse(start).Days);
```

---

对不同 **.NET** 框架版本支持情况如下：

- **NET35**
  不带选择器方法最多支持 **7** 个序列，返回类型 **Tuple**（迁移代码实现）；
  带选择器方法最多支持 **4** 个序列（**Func** 最多只支持 4 个参数）。
- **NET40 / NETSTANDARD2_0**
  不带选择器方法最多支持 **7** 个序列，返回类型 **Tuple**（标准库实现）；
  带选择器方法最多支持 **7** 个序列（**Tuple** 最多只支持 7 个参数）。

<br/>

### 高阶函数

转换指定函数为它的高阶形态。

---

假设，函数 ***d*** 为求导函数，函数 ***f*** 为原函数：

```c#
public void HigherTest1()
{
    static HigherFunc<Func<decimal, decimal>> d = func =>
    {
        decimal deltaX = 0.000_000_000_000_1m;
        return x => (func(x + deltaX) - func(x)) / deltaX;
    };
    
    static decimal f(decimal x) => x * x * x * x;   // f  (x) = x^4
    var d1 = d.Higher(1);   // d1 = d(f)            // f' (x) = 4  * x^3
    var d2 = d.Higher(2);   // d2 = d(d(f))         // f''(x) = 12 * x^2
    
    Assert.Equal(32, (int)d(f)(2));
    Assert.Equal(32, (int)d1(f)(2));
    
    Assert.Equal(48, (int)d(d(f))(2));
    Assert.Equal(48, (int)d2(f)(2));
}
```
- ***d*** 的一阶函数 **d.Higher(1)**：***d(f)***
- ***d*** 的二阶函数 **d.Higher(2)**：***d(d(f))***



## 控制台

### 控制台输出重定向

重定向控制台输出。

---

某些特定的开发场景可能会使用控制台重定向输出。例如，测试 **CliTool** 应用程序。

默认的 **ConsoleAgent** 会将 **Console** 保存到缓存，当需要的时候调用 **ReadAllText** 函数来获取并清空缓存：

```c#
using (ConsoleAgent.Begin())
{
    Console.Write(123);
    Assert.Equal("123", ConsoleAgent.ReadAllText());

    Console.Write(456);
    Assert.Equal("456", ConsoleAgent.ReadAllText());
}
```
**ConsoleAgent** 也允许使用使用自定义 **TextWriter**：

```c#
var output = new StringBuilder();
var writer = new StringWriter(output);
using (ConsoleAgent.Begin(writer))
{
    Console.Write(123);
    Assert.Equal("123", output.ToString());

    Console.Write(456);
    Assert.Equal("123456", output.ToString());
}
```

