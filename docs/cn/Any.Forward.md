## Any.Forward

按指定路径依次获取元素。

<br/>

### 深层遍历

**示例 1** （最里面的 **Exception**）

```csharp
var exception = new Exception("3", new Exception("2", new Exception("1")));
var forwards = Any.Forward(exception, x => x.InnerException);

/*
 * Exception 3      = First
 * - Exception 2
 * - - Exception 1  = Last
 */
Assert.Equal("1", (from ex in forwards select ex).Last().Message);
```

<br/>

