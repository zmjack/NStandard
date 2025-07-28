using NStandard.Algorithm;

namespace NStandard.Data;

public static class Snowflake
{
    public static long WorkerId
    {
        get => _worker.WorkerId;
        set => _worker.WorkerId = value;
    }

    public static long DataCenterId
    {
        get => _worker.DataCenterId;
        set => _worker.DataCenterId = value;
    }

    private static readonly SnowflakeWorker _worker = new(0, 0);

    /// <summary>
    /// Generate a snowflake ID with default WorkerId and DataCenterId.
    /// </summary>
    /// <returns></returns>
    public static long NewId() => _worker.NewId();
}

#if NET7_0_OR_GREATER
public static class Snowflake128
{
    public static Int128 WorkerId
    {
        get => _worker.WorkerId;
        set => _worker.WorkerId = value;
    }

    public static Int128 DataCenterId
    {
        get => _worker.DataCenterId;
        set => _worker.DataCenterId = value;
    }

    private static readonly SnowflakeWorker128 _worker = new(0, 0);

    /// <summary>
    /// Generate a snowflake ID with default WorkerId and DataCenterId.
    /// </summary>
    /// <returns></returns>
    public static Int128 NewId() => _worker.NewId();

    /// <summary>
    /// Generate a snowflake ID with default WorkerId and DataCenterId.
    /// </summary>
    /// <returns></returns>
    public static Guid NewGuid() => _worker.NewGuid();
}
#endif
