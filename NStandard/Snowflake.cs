using NStandard.Algorithm;

namespace NStandard;

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
