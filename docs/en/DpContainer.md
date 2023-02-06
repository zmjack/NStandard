# NStandard

**.NET** extension library for system library.

<br/>

## DpContainer

**DpContainer** is used for dynamic programming calculations.

<br/>

Take the **Fibonacci** function as an example.

### Recursion Code

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

### DP Code 1 (Using class)

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

### DP Code 2 (Using function)

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

The correct answer is **433494437**.

And the time required for the above code to get the correct result is roughly as follows:

|      | Name                       | Elapsed              |
| ---- | -------------------------- | -------------------- |
|      | Recursion Code             | about 3.823 seconds. |
| *    | DP Code 1 (Using class)    | about 0.010 seconds. |
|      | DP Code 2 (Using function) | about 0.011 seconds. |

