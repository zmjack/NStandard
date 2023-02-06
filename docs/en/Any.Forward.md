# NStandard

**.NET** extension library for system library.

<br/>

## Any.Forward

Get elements sequentially by the specified path.

<br/>

### Deep traversal

**Example 1** ( Innermost **Exception** )

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

