# NStandard

**.NET** extension library for system library.

<br/>

## Numerical Evaluator

We provide **NumericalEvaluator** to support string expression parsing.

For example:

```csharp
Evaluator.Numerical.Eval("2 >= 3 ? 5 : 7");
```

and the result is ***7***.

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
| 11       | and      | **Returns** the **left-hand** expression **if** the **left-hand** expression **is 0**,<br/>**otherwise** the **right-hand** expression. | (3 and 5) is 5.<br/>(0 and 4) is 0.             |
| 12       | or       | **Returns** the **left-hand** expression **if** the **left-hand** expression **is not 0**,<br/>**otherwise** the **right-hand** expression. | (3 or 5) is 3.<br/>(0 or 4) is 4.               |
| 13       | ?        | **Returns** the **right-hand** expression **if** the **left-hand** expression **is not 0**,<br/>**otherwise** **double.NegativeInfinity**. | (2 > 3 ? 5) is -∞.<br/>(2 < 3 ? 5) is 3.        |
| 14       | :        | **Returns** the **right-hand** expression **if** the **left-hand** expression **is** **double.NegativeInfinity**,<br/>**otherwise** the **left-hand** expression. | (2 > 3 ? 5 : 7) is 7.<br/>(2 < 3 ? 5 : 7) is 5. |

<br/>

It looks like **Python** and is much easier to use.

If there are no parameters:

```csharp
Evaluator.Numerical.Eval("2 >= 3 ? 5 : 7");
```

or there is any parameter:

```csharp
Evaluator.Numerical.Eval(
    "${price} >= 100 ? ${price} * 0.8 : ${price}", 
    new Dictionary<string, double>
    {
        ["price"] = 100,
    });
// The result is 80.
```

It's worth noting that, these operators (**?** and **:**) are specific. Used in combination, it will have the same effect as the ternary operator.

<br/>

In addition, if you want to evaluate the same expression, you should not use **Evaluator.Numerical.Eval** method.

The correct approach is to **compile** it first and then **call** the compiled function. It will run faster.

For example, the faster code is:

```csharp
// Recommended
var exp = "1 + (2 * 3 - 4 * (5 + 6)) + 7";
var func = Evaluator.Numerical.Compile(exp);
for (int i = 0; i < 100000; i++)
{
    var actual = func();
}
```
and the slower code:

```csharp
var exp = "1 + (2 * 3 - 4 * (5 + 6)) + 7";
for (int i = 0; i < 100000; i++)
{
    var actual = Evaluator.Numerical.Eval(exp);
}
```

There is our test result:

| Name                                | Elapsed |
| ----------------------------------- | ------- |
| **Compile first** for 100'000 calls | 40 ms   |
| **Eval** for 100'000 calls          | 10 sec  |

