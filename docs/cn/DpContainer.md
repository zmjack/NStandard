# NStandard

**.NET** extension library for system library.

<br/>

## DpContainer

**DpContainer** 用于动态规划计算。

<br/>

以**斐波那契**函数为例。

### 递归代码：

```csharp
int Fib(int n)
{
	if (n == 0 || n == 1) return 1;
	else return Fib(n - 1) + Fib(n - 2);
}

void Main()
{
	Fib(42).Dump();	
}
```

### DP 代码 1（使用 class）:

```csharp
class DpFib : DpContainer<int, int>
{
    public override int StateTransfer(int n)
    {
        if (n == 0 || n == 1) return 1;
        else return this[n - 1] + this[n - 2];
    }
}

void Main()
{
	var fib = new DpFib();
	fib[42].Dump();
}
```

### DP 代码 2（使用函数）:

```csharp
int Fib(DefaultDpContainer<int, int> dp, int n)
{
	if (n == 0 || n == 1) return 1;
	else return dp[n - 1] + dp[n - 2];
}

void Main()
{
	var fib = DpContainer.Create<int, int>(Fib);
	fib[42].Dump();		// same as Fib(fib, 42).Dump();	
}
```

<br/>

正确结果是 **433494437**.

上述代码得到正确结果所需的时间大致如下：

|      | Name                    | Elapsed       |
| ---- | ----------------------- | ------------- |
|      | 递归代码                | 约 3.823 秒。 |
| *    | DP 代码 1（使用 class） | 约 0.010 秒。 |
|      | DP 代码 2（使用函数）   | 约 0.011 秒。 |

