# NStandard

**.NET** extension library for system library.

<br/>

## SequenceInputStream

**SequenceInputStream** can combine multiple streams into a single stream.

<br/>

For example:

```csharp
using var stream = new SequenceInputStream(
    new MemoryStream("123".Bytes(Encoding.UTF8)),
    new MemoryStream("456".Bytes(Encoding.UTF8)),
    new MemoryStream("789".Bytes(Encoding.UTF8))
);
using var reader = new StreamReader(stream);
var result = reader.ReadToEnd();	// 123456789
```

<br/>

