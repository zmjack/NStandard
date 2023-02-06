# NStandard

**.NET** extension library for system library.

<br/>

## Sync

**Sync** 用来进行数据绑定。

<br/>

例如，这里有两个类型为 **Sync\<int\>** 变量 `a` `b`。

```csharp
using var a = new Sync<int>(2);
using var b = new Sync<int>(3);
```

以及一个通过 `a + b` 自动计算得到的变量 `c`。

```csharp
using var c = Sync.From(() => a + b);
// c.Value 的值为 5。
Console.WriteLine(c.Value);
```

当 `a` `b` 任意 **Value** 改变时，`c` 值会根据公式同时被改变。

```csharp
a.Value = 7;
// c.Value 的值为 10。
Console.WriteLine(c.Value);
```

<br/>

### 实现过程

**Sync** 会在创建时收集依赖，并为每个依赖项绑定通知函数。

随后，若依赖项的值发生变化，会通知到目标 **Sync** 对象为当前值设置过期。

**Sync** 对象在获取值时，如果当前值已过期，则会重新计算最新值并缓存。

<br/>

一般来说，**Sync** 应该与 **using** 一起使用，以避免可能的内存泄漏。

但这不并不是必须的，你仍然可以通过手动管理来提升性能，特别是对大量 **Sync** 对象管理。

<br/>

下面通过例子来说明 **Sync** 是如何工作的。

1. 定义变量 `a` `b`：

    ```csharp
    using var a = new Sync<int>(2);
    using var b = new Sync<int>(3);
    ```

    ![Sync-001.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-001.png?raw=true)

2. 定义变量 `c`，取值为 `a + b`。
   （该操作会同时会为 `a` `b` 订阅通知事件。）
   
    ```csharp
    var c = Sync.From(() => a + b);
    ```
   
    ![Sync-002.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-002.png?raw=true)
3. 获取 `c` 值并输出。
    （该操作将计算公式并缓存结果。）
    
    ```csharp
    Console.WriteLine(c.Value);
    ```

    ![Sync-003.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-003.png?raw=true)

4. 改变 `a` 值为 7。
   （该操作会将 `c` 值设置为 **已过期**。）
   
    ```csharp
    a.Value = 7;
    ```
   

![Sync-004.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-004.png?raw=true)

5. 重新获取 `c` 值并输出。
    （该操作将重新计算公式并缓存结果。）

    ```csharp
    Console.WriteLine(c.Value);
    ```

    ![Sync-005.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-005.png?raw=true)

6. 销毁 `c`。
（该操作将退订其依赖项的通知事件。）
   
    ```csharp
    c.Dispose();
    ```
   
    ![Sync-006.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-006.png?raw=true)

<br/>

### 避免内存泄漏

参考下面的例子：

- **root** 为 **static** **Sync** 对象。
- 成员函数中的 **Sync** 对象数组，依赖于 **root**。
- 因为这些 **Sync** 对象订阅了 **static** **Sync** 的更新通知，所以在函数生命周期结束时，它们不会被回收。
- 为了避免内存泄漏，应该使用 **using** 或手动调用 **Dispose**。
- 调用被订阅者的 **Release** 方法，取消依赖于该对象的所有订阅，也能够避免内存泄漏，并且执行效率更高。

```csharp
class Program
{
    static Sync<int> root = new Sync<int>(1);

    static void Main(string[] args)
    {
        PrintMemory("Beginning:");
        
        MakeSomeSyncObjects(1_000);
        PrintMemory("Before release:");
        
        root.Release();
        PrintMemory("After release:");
    }
    
    static void MakeSomeSyncObjects(int count)
    {
        var syncs = new Sync<int>[count];
        for (int i = 0; i < count; i++)
        {
            var index = i;
            syncs[i] = Sync.From(() => root + index);
        }
        //syncs.DisposeAll();
    }
    
    static void PrintMemory(string title)
    {
        Console.WriteLine($"{title，-38}{GC.GetTotalMemory(true) / 1024:N0} KB");
    }
}
```

> Beginning:                            504 KB<br/>
> Before release:                       2,018 KB<br/>
> After release:                        533 KB

<br/>

### 可选事件

| 事件名   | 签名                         | 描述                           |
| -------- | ---------------------------- | ------------------------------ |
| Changed  | void Changed (TValue value)  | 如果值被 **显式更改** 时触发。 |
| Updating | void Updating (TValue value) | 如果值被 **设置过期** 时触发。 |

