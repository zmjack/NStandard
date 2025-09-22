## Numerical Evaluator

**NumericalEvaluator** 用于解析支持字符串表达式。

例如：

```csharp
Evaluator.Numerical.Eval("2 >= 3 ? 5 : 7");
```

结果为 ***7***.

<br/>

我们提供了一组丰富的运算符：

| 优先级 | 运算符 | 描述                                                         | 示例                                                |
| ------ | ------ | ------------------------------------------------------------ | --------------------------------------------------- |
| 1      | **     | 求幂。                                                       | (2 ** 3) 等于 8.                                    |
| 3      | //     | 整除，向下取整。                                             | (7 // 5) 等于 1.                                    |
|        | *      | 乘法。                                                       | (2 * 3) 等于 6.                                     |
|        | /      | 除法。                                                       | (7 / 5) 等于 1.4.                                   |
|        | %      | 取模。                                                       | (7 % 5) 等于 2.                                     |
| 4      | +      | 加法。                                                       | (2 + 3) 等于 5.                                     |
|        | -      | 减法。                                                       | (7 - 5) 等于 2.                                     |
| 6      | >      | 大于。                                                       | (7 > 5) 等于 1.                                     |
|        | \>=    | 大于等于。                                                   | (7 >= 5) 等于 1.                                    |
|        | <      | 小于。                                                       | (2 < 3) 等于 1.                                     |
|        | <=     | 小于等于。                                                   | (2 <= 3) 等于 1.                                    |
| 7      | ==     | 等于。                                                       | (7 == 7) 等于 1.                                    |
|        | !=     | 不等于。                                                     | (2 != 3) 等于 1.                                    |
| 11     | and    | 如果 **左表达式** 为 **0**，**返回** **左表达式**，否则 **返回** **右表达式**。 | (3 and 5) 等于 5.<br/>(0 and 3) 等于 0.             |
| 12     | or     | 如果 **左表达式** 不为 **0**，**返回** **左表达式**，否则 **返回** **右表达式**。 | (3 or 5) 等于 3.<br/>(0 or 3) 等于 3.               |
| 13     | ??     | 如果 **左表达式** 为 **double.NaN**，**返回** **右表达式**，否则 **返回** **左表达式**。 | (5 ?? 7) 等于 5.<br/>(NaN ?? 7) 等于 7.             |
| 14     | ?      | 如果 **左表达式** 不为 **0**，**返回** **右表达式**，否则 **返回** **double.NaN**。 | (2 > 3 ? 5) 等于 NaN.<br/>(2 < 3 ? 5) 等于 5.       |
| 15     | :      | 如果 **左表达式** 为 **double.NaN**，**返回** **右表达式**，否则 **返回** **左表达式**。 | (2 > 3 ? 5 : 7) 等于 7.<br/>(2 < 3 ? 5 : 7) 等于 5. |

内置的 **一元运算符**:

| 运算符 | 描述           | 示例             |
| ------ | -------------- | ---------------- |
| not    | 等同运算 `!x`. | not 0 = 1        |
| +      | 等同运算 `+x`. | + ( 2 + 3 ) = 5  |
| -      | 等同运算 `–x`. | - ( 2 + 3 ) = -5 |

内置的 **运算函数**:

| 运算符       | 描述                                         | 示例                    |
| ------------ | -------------------------------------------- | ----------------------- |
| ( ... )      | 高优先级运算。                               | 2 * ( 3 + 5 ) = 16      |
| abs( ... )   | 返回双精度浮点数的绝对值。                   | abs(-2) = 2             |
| sqrt( ... )  | 返回指定数值的平方根。                       | sqrt(9) = 3             |
| ceil( ... )  | 返回大于或等于指定双精度浮点数的最小整数值。 | ceil(6.8) = 7           |
| floor( ... ) | 返回小于或等于指定双精度浮点数的最大整数。   | floor(6.8) = 6          |
| sin( ... )   | 返回指定角度的正弦值。                       | sin({Math.PI / 2}) = 1  |
| cos( ... )   | 返回指定角度的余弦值。                       | cos(0) = 1              |
| tan( ... )   | 返回指定角度的正切值。                       | tan({Math.PI / 4}) = 1  |
| asin( ... )  | 返回正弦为指定数值的角度。                   | sin(1) = {Math.PI / 2}  |
| acos( ... )  | 返回余弦为指定数值的角度。                   | acos(1) = 0             |
| atan( ... )  | 返回正切为指定数值的角度。                   | atan(1) = {Math.PI / 4} |
| sinh( ... )  | 返回指定角度的双曲正弦值。                   | sinh(0.1) = 0.1001...   |
| cosh( ... )  | 返回指定角度的双曲余弦值。                   | cosh(0.1)  = 1.0050...  |
| tanh( ... )  | 返回指定角度的双曲正切值。                   | tanh(0.1) = 0.0996...   |

<br/>

不带参数的求值示例：

```csharp
Evaluator.Numerical.Eval("2 >= 3 ? 5 : 7");
```

带参数的求值求例：

```csharp
var exp = "${price} >= 100 ? ${price} * 0.8 : ${price}";
var func = Evaluator.Numerical.Compile(exp);
var result = func(new { Price = 100 });
// 结果为 80.
```

```csharp
var exp = "${price} >= 100 ? ${price} * 0.8 : ${price}";
var func = Evaluator.Numerical.Compile(exp);
var result = func(new Dictionary<string, double> 
{
    ["price"] = 100
});
// 结果为 80.
```

```csharp
class Item
{
    public double Price { get; set; }
}

void Main()
{
    var exp = "${price} >= 100 ? ${price} * 0.8 : ${price}";
    var func = Evaluator.Numerical.Compile<Item>(exp);
    var result = func(new Item { Price = 100 });
    // 结果为 80.
}
```

值得注意的是，这些运算符（ **?** 和 **:** ）是特定的。 结合使用，它将具有与三元运算符 ( **? :** ) 相同的效果。

<br/>

### 编译阶段

比如把字符串解析成函数。

```csharp
var exp = "${x} + sqrt(abs(${x} * 3)) * 3";
```

1. 将字符串解析为节点集合。

   | 问题     | 节点类型                      | 索引 | 值    |
   | -------- | :---------------------------- | :--- | :---- |
   |          | Parameter                     | 0    | ${x}  |
   | 节点歧义 | UnaryOperator, BinaryOperator | 5    | +     |
   |          | StartBracket                  | 7    | sqrt( |
   |          | StartBracket                  | 12   | abs(  |
   |          | Parameter                     | 16   | ${x}  |
   |          | BinaryOperator                | 21   | *     |
   |          | Operand                       | 23   | 3     |
   |          | EndBracket                    | 24   | )     |
   |          | EndBracket                    | 25   | )     |
   |          | BinaryOperator                | 27   | *     |
   |          | Operand                       | 29   | 3     |

2. 消除节点歧义。

   | 节点类型           | 索引 | 值    |
   | :----------------- | :--- | :---- |
   | Parameter          | 0    | ${x}  |
   | **BinaryOperator** | 5    | +     |
   | StartBracket       | 7    | sqrt( |
   | StartBracket       | 12   | abs(  |
   | Parameter          | 16   | ${x}  |
   | BinaryOperator     | 21   | *     |
   | Operand            | 23   | 3     |
   | EndBracket         | 24   | )     |
   | EndBracket         | 25   | )     |
   | BinaryOperator     | 27   | *     |
   | Operand            | 29   | 3     |

3. 构建 **表达式**。

   ```
   (
       IIF(p.ContainsKey("x"), p.get_Item("x"), 0) +
       (
           value(NStandard.Evaluators.NumericalEvaluator).Bracket
           (
               "sqrt(", ")", 
               value(NStandard.Evaluators.NumericalEvaluator).Bracket
               (
                   "abs(", ")", 
                   (
                       IIF(p.ContainsKey("x"), p.get_Item("x"), 0) * 3
                   )
               )
           ) * 3
       )
   )
   ```

4. 将表达式编译为 **Func<object, double>**。

   ```csharp
   var func = Evaluator.Numerical.Compile(exp);
   ```

5. 调用 **Func<object, double>**。

   使用 **匿名对象**：

   ```csharp
   var result = func(new { x = -3 });
   ```

   或者 **Dictionary<string, double>**:

   ```csharp
   var result = func(new Dictionary<string, double> 
   {
       ["x"] = -3
   });
   ```

<br/>

### 自定义求值器

扩展 **NumericalEvaluator** 创建一个更多运算符的求值器：

```csharp
public class MyEvaluator : NumericalEvaluator
{
    public MyEvaluator()
    {
        Define("!", value => value != 0d ? 0d : 1d);
        DefineBracket(new("|", "|"), value => Math.Abs(value));
        Initialize();
    }
}
```

- 使用 `| ... |` 计算数值的绝对值。
- 使用 `!` 对 **布尔值** 取反。

> [!NOTE]
>
> 在构造函数中调用 Initialize 方法可以为第一次操作提供更好的性能。

<br/>

让我们对下面的字符串求值：

```csharp
"|-9| + !0"
```

```csharp
var evaluator = new MyEvaluator();
var result = evaluator.Eval("|-9| + !0");
// |-9| is abs(-9) = 9
// !0  is 1
// 结果为 10.
```

<br/>

