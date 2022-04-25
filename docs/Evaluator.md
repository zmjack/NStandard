# NStandard

**.NET** extension library for system library.

<br/>

## Numerical Evaluator

We provide **NumericalEvaluator** to support string expression parsing.

For example:

```csharp
Evaluator.Numerical.Eval("2 >= 3 ? 5 : 7");
```

The result is ***7***.

<br/>

We provide a rich set of operators:

| Priority | Operator | Description                                                  | Example                                         |
| -------- | -------- | ------------------------------------------------------------ | ----------------------------------------------- |
| 1        | **       | Exponentiation.                                              | (2 ** 3) is 8.                                  |
| 3        | //       | Floor division.                                              | (7 // 5) is 1.                                  |
|          | *        | Multiplication.                                              | (2 * 3) is 6.                                   |
|          | /        | Division.                                                    | (7 / 5) is 1.4.                                 |
|          | %        | Modulus.                                                     | (7 % 5) is 2.                                   |
| 4        | +        | Addition.                                                    | (2 + 3) is 5.                                   |
|          | -        | Subtraction.                                                 | (7 - 5) is 2.                                   |
| 6        | >        | Greater than.                                                | (7 > 5) is 1.                                   |
|          | \>=      | Greater than or equal to.                                    | (7 >= 5) is 1.                                  |
|          | <        | Less than.                                                   | (2 < 3) is 1.                                   |
|          | <=       | Less than or equal to.                                       | (2 <= 3) is 1.                                  |
| 7        | ==       | Equal.                                                       | (7 == 7) is 1.                                  |
|          | !=       | Not equal.                                                   | (2 != 3) is 1.                                  |
| 11       | and      | **Returns** the **left-hand** expression **if** the **left-hand** expression **is 0**,<br/>**otherwise** the **right-hand** expression. | (3 and 5) is 5.<br/>(0 and 3) is 0.             |
| 12       | or       | **Returns** the **left-hand** expression **if** the **left-hand** expression **is not 0**,<br/>**otherwise** the **right-hand** expression. | (3 or 5) is 3.<br/>(0 or 3) is 3.               |
| 13       | ??       | **Returns** the **right-hand** expression **if** the **left-hand** expression **is** **double.NaN**,<br/>**otherwise** the **left-hand** expression. | (5 ?? 7) is 5.<br/>(NaN ?? 7) is 7.             |
| 14       | ?        | **Returns** the **right-hand** expression **if** the **left-hand** expression **is not 0**,<br/>**otherwise** **double.NaN**. | (2 > 3 ? 5) is NaN.<br/>(2 < 3 ? 5) is 5.       |
| 15       | :        | **Returns** the **right-hand** expression **if** the **left-hand** expression **is** **double.NaN**,<br/>**otherwise** the **left-hand** expression. | (2 > 3 ? 5 : 7) is 7.<br/>(2 < 3 ? 5 : 7) is 5. |

<br/>

It looks like **Python** and is much easier to use.

If there are no parameters:

```csharp
Evaluator.Numerical.Eval("2 >= 3 ? 5 : 7");
```

or there is any parameter:

```csharp
var exp = "${price} >= 100 ? ${price} * 0.8 : ${price}";
Func<object, double> func = Evaluator.Numerical.Compile(exp);
var result = func(new { Price = 100 });
// The result is 80.
```

```csharp
class Item
{
    public double Price { get; set; }
}

void Main()
{
    var exp = "${price} >= 100 ? ${price} * 0.8 : ${price}";
    Func<Item, double> func = Evaluator.Numerical.Compile<Item>(exp);
    var result = func(new Item { Price = 100 });
    // The result is 80.
}
```

It's worth noting that, these operators ( **?** and **:** ) are specific. Used in combination, it will have the same effect as the ternary operator ( **? :** ).

<br/>

### Compilation phase

For example, parse the string into a function.

```csharp
var exp = "${x} + sqrt(abs(${x} * 3)) * 3";
```

1. Parse a string into a collection of nodes. 

   | NodeType       | Index | Value |
   | :------------- | :---- | :---- |
   | Parameter      | 0     | ${x}  |
   | BinaryOperator | 5     | +     |
   | StartBracket   | 7     | sqrt( |
   | StartBracket   | 12    | abs(  |
   | Parameter      | 16    | ${x}  |
   | BinaryOperator | 21    | *     |
   | Operand        | 23    | 3     |
   | EndBracket     | 24    | )     |
   | EndBracket     | 25    | )     |
   | BinaryOperator | 27    | *     |
   | Operand        | 29    | 3     |

2. Build the **Expression**.

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

3. Compile the Expression to **Func<object, double>**.

<br/>

### Customize evaluator

There is a simple evaluator which is extend **NumericalEvaluator**:

```csharp
public class MyEvaluator : NumericalEvaluator
{
    public MyEvaluator() : base(false)
    {
        AddUnaryOpFunction("!", value => value != 0d ? 0d : 1d);
        AddBracketFunction(("[", "]"), value => Math.Sqrt(value));
        Initialize();
    }
}
```

Let's evaluate the string:

```csharp
"[9] + !0"
```

```csharp
var evaluator = new MyEvaluator();
var result = evaluator.Eval("[9] + !0");
// [9] is Sqrt(9) = 3
// !0  is 1
// The result is 4.
```

<br/>