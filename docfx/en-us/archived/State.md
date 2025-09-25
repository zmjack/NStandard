## State

**State** is used for data binding.

<br/>

For example, there are two variables `a` `b` of type **State\<int\>**.

```csharp
using var a = State.Use(2);
using var b = State.Use(3);
```

and a variable `c` which is automatically calculated by `a + b`.

```csharp
using var c = State.From(() => a + b);
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

**State** collects dependencies at creation time and binds a notification function for each dependency.

Subsequently, if the value of the dependency changes, the target **State** object is notified to set an expiration for the current value.

When the **State** object gets the value, if the current value has expired, the latest value will be recalculated and cached.

<br/>

In general, **State** should be used together with **using** to avoid possible memory leaks.

But this is not necessary, you can still improve performance through manual management, especially for a large number of **State** object management.

<br/>

Let's use an example to illustrate how **State** works.

1. Define variables `a` `b`:

   ```csharp
   using var a = State.Use(2);
   using var b = State.Use(3);
   ```

   ![State-001.png](/images/State-001.png)

2. Define variable `c` with value `a + b`.

   (This operation will also subscribe to notification events for `a` `b`.)

    ```csharp
    var c = State.From(() => a + b);
    ```

   ![State-002.png](/images/State-002.png)
3. Get the value of `c` and output it.

   (The operation will evaluate the formula and cache the result.)

   ```csharp
   Console.WriteLine(c.Value);
   ```

   ![State-003.png](/images/State-003.png)

4. Change the value of `a` to 7.

   (This operation will set the `c` value to **Expired**.)
   
   ```csharp
   a.Value = 7;
   ```

   ![State-004.png](/images/State-004.png)

5. Retrieve the value of `c` and output it.

   (This operation will recalculate the formula and cache the result.)
   
   ```csharp
   Console.WriteLine(c.Value);
   ```

   ![State-005.png](/images/State-005.png)
   
6. Dispose `c`.

   (The operation will unsubscribe from its dependencies' notification events.)

   ```csharp
   c.Dispose();
   ```

   ![State-006.png](/images/State-006.png)

<br/>

### Avoid memory leaks

Consider the following example:

- **root** is a **static** **State** object.
- Array of **State** objects in member functions, dependent on **root**.
- Because these **State** objects are subscribed to **static** **State** update notifications, they will not be recycled when the function lifetime ends.
- To avoid memory leaks, **using** should be used or **Dispose** should be called manually.
- Calling the **Release** method of the subscriber to cancel all subscriptions that depend on the object can also avoid memory leaks, and the execution efficiency is higher.

```csharp
class Program
{
    static State<int> root = State.Use(1);

    static void Main(string[] args)
    {
        PrintMemory("Beginning:");
        
        MakeSomeStateObjects(1_000);
        PrintMemory("Before release:");
        
        root.Release();
        PrintMemory("After release:");
    }
    
    static void MakeSomeStateObjects(int count)
    {
        var States = new State<int>[count];
        for (int i = 0; i < count; i++)
        {
            var index = i;
            States[i] = State.From(() => root + index);
        }
        //States.DisposeAll();
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

