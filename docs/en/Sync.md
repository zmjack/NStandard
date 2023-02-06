# NStandard

**.NET** extension library for system library.

<br/>

## Sync

**Sync** is used for data binding.

<br/>

For example, there are two variables `a` `b` of type **Sync\<int\>**.

```csharp
using var a = new Sync<int>(2);
using var b = new Sync<int>(3);
```

and a variable `c` which is automatically calculated by `a + b`.

```csharp
using var c = Sync.From(() => a + b);
// c.Value is 5。
Console.WriteLine(c.Value);
```

When any **Value** of `a` `b` is changed, the value of `c` will be changed according to the formula at the same time.

```csharp
a.Value = 7;
// c.Value is 10。
Console.WriteLine(c.Value);
```

<br/>

### Implementation

**Sync** collects dependencies at creation time and binds a notification function for each dependency.

Subsequently, if the value of the dependency changes, the target **Sync** object is notified to set an expiration for the current value.

When the **Sync** object gets the value, if the current value has expired, the latest value will be recalculated and cached.

<br/>

In general, **Sync** should be used together with **using** to avoid possible memory leaks.

But this is not necessary, you can still improve performance through manual management, especially for a large number of **Sync** object management.

<br/>

Let's use an example to illustrate how **Sync** works.

1. Define variables `a` `b`:

    ```csharp
    using var a = new Sync<int>(2);
    using var b = new Sync<int>(3);
    ```

    ![Sync-001.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-001.png?raw=true)

2. Define variable `c` with value `a + b`.
   (This operation will also subscribe to notification events for `a` `b`.)
   
    ```csharp
    var c = Sync.From(() => a + b);
    ```
   
    ![Sync-002.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-002.png?raw=true)
3. Get the value of `c` and output it.
    (The operation will evaluate the formula and cache the result.)
    
    ```csharp
    Console.WriteLine(c.Value);
    ```

    ![Sync-003.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-003.png?raw=true)

4. Change the value of `a` to 7.
   (This operation will set the `c` value to **Expired**.)
   
    ```csharp
    a.Value = 7;
    ```
   

![Sync-004.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-004.png?raw=true)

5. Retrieve the value of `c` and output it.
    (This operation will recalculate the formula and cache the result.)

    ```csharp
    Console.WriteLine(c.Value);
    ```

    ![Sync-005.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-005.png?raw=true)

6. Dispose `c`.
(The operation will unsubscribe from its dependencies' notification events.)
   
    ```csharp
    c.Dispose();
    ```
   
    ![Sync-006.png](https://github.com/zmjack/NStandard/blob/master/docs/images/Sync-006.png?raw=true)

<br/>

### Avoid memory leaks

Consider the following example:

- **root** is a **static** **Sync** object.
- Array of **Sync** objects in member functions, dependent on **root**.
- Because these **Sync** objects are subscribed to **static** **Sync** update notifications, they will not be recycled when the function lifetime ends.
- To avoid memory leaks, **using** should be used or **Dispose** should be called manually.
- Calling the **Release** method of the subscriber to cancel all subscriptions that depend on the object can also avoid memory leaks, and the execution efficiency is higher.

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

### Optional events

| Event    | Signature                    | Description                                     |
| -------- | ---------------------------- | ----------------------------------------------- |
| Changed  | void Changed (TValue value)  | Fired when the value is **explicitly changed**. |
| Updating | void Updating (TValue value) | Fired when the value is **set to expire**.      |

