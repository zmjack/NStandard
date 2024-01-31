namespace NStandard;

public static class Singleton<T> where T : class, ISingleton<T>, new()
{
    private static readonly object _sync = new();
    private static T _instance = null;

    /// <summary>
    /// Get or create a thread-safe singleton instance.
    /// </summary>
    /// <returns></returns>
    public static T GetOrCreate()
    {
        if (_instance is null)
        {
            lock (_sync)
            {
                if (_instance is null)
                {
                    _instance = new T();
                    _instance.OnInit();
                }
            }
        }
        return _instance;
    }

    public static void Destory()
    {
        if (_instance is not null)
        {
            lock (_sync)
            {
                _instance = null;
            }
        }
    }
}

public static class StaticSingleton<T> where T : class, ISingleton<T>, new()
{
    /// <summary>
    /// Get the static singleton instance.
    /// </summary>
    public static readonly T Instance;

    static StaticSingleton()
    {
        Instance = new T();
        Instance.OnInit();
    }
}

public interface ISingleton<TSelf> where TSelf : ISingleton<TSelf>
{
#if NET7_0_OR_GREATER
    /// <summary>
    /// Use <see cref="Singleton{Form}.GetOrCreate()" /> or <see cref="StaticSingleton{T}.Instance" /> to get a singleton instance.
    /// </summary>
    public abstract static TSelf Instance { get; }
#endif
    void OnInit();
}
