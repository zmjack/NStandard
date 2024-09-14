namespace NStandard;

public static partial class Any<T>
{
    /// <summary>
    /// Takes two functions and returns a function.
    /// </summary>
    /// <typeparam name="TRet"></typeparam>
    public static Func<T, TRet> Compose<TRet>(Func<T, TRet> pipe)
    {
        return x => pipe(x);
    }
}
