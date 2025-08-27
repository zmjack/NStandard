## SequenceInputStream

**SequenceInputStream** 可以将多个流组合成一个流。

<br/>

**示例**

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

