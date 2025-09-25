# Field 特性

<br/>

自动属性仅允许直接设置或获取支持字段，只能通过在访问器上添加访问修饰符来实现部分控制。

有时需要对一个或两个访问器中发生的情况进行额外的控制，但这会给用户带来 **声明支持字段** 的额外编码开销。

支持字段名称必须与属性保持同步，并且支持字段的作用域限定为整个类，这可能导致在类内部意外绕过访问器。

以下是实现示例：

[!code-csharp[](../../../Tests/NStandard.Test/Analyzer/FieldFeatureTests.cs#LegacyModel)]

但是，我们希望 **避免在较旧的项目中手动声明支持字段**。

使用 **FieldFeature** 启用生成器分析类或结构体的代码，并使用 **FieldBackend** 隐藏支持字段：

[!code-csharp[](../../../Tests/NStandard.Test/Analyzer/FieldFeatureTests.ModelsWithNoNs.cs#ModelWithNoNS)]

当然，最好的情况是使用 **C# 14.0 或更高版本** 并使用 **field** 关键字来处理这种场景。

```csharp
public class Model
{
    public int Number
    {
        get => field;
        set => field = value;
    }

    public int[] Numbers
    {
        get => field;
        set => field = value;
    }
}
```

