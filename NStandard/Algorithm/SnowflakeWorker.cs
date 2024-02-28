using System;
using System.Runtime.CompilerServices;

namespace NStandard.Algorithm;

/// <summary>
/// Snowflake generator.
/// <para>Source: Scala code (<see href="https://github.com/twitter-archive/snowflake" />).</para>
/// <para>0(1b) Timestamp(41b) DataCenterId(5b) WorkerId(5b) Sequnce(12b) </para>
/// </summary>
public class SnowflakeWorker
{
    private static readonly long twepoch = 1288834974657;

    private static readonly int workerIdBits = 5;
    private static readonly int datacenterIdBits = 5;
    private static readonly int maxWorkerId = -1 ^ (-1 << workerIdBits);
    private static readonly int maxDatacenterId = -1 ^ (-1 << datacenterIdBits);

    private static readonly int sequenceBits = 12;
    private static readonly int workerIdShift = sequenceBits;
    private static readonly int datacenterIdShift = sequenceBits + workerIdBits;
    private static readonly int timestampLeftShift = sequenceBits + workerIdBits + datacenterIdBits;
    private static readonly int sequenceMask = -1 ^ (-1 << sequenceBits);

    private long lastTimestamp = -1L;
    private long sequence = 0;

    public long WorkerId { get; internal set; }
    public long DataCenterId { get; internal set; }

    public SnowflakeWorker(int workerId, int dataCenterId)
    {
        // sanity check for workerId
        if (workerId > maxWorkerId || workerId < 0)
        {
            throw new ArgumentException($"The workerId can not be greater than {maxWorkerId} or less than 0", nameof(workerId));
        }

        if (dataCenterId > maxDatacenterId || dataCenterId < 0)
        {
            throw new ArgumentException($"The datacenterId can not be greater than {maxDatacenterId} or less than 0", nameof(dataCenterId));
        }

        WorkerId = workerId;
        DataCenterId = dataCenterId;
    }

    protected long NewTimestamp() => DateTime.Now.ToUnixTimeMilliseconds();

    protected long NextTimeStamp(long lastTimestamp)
    {
        var timestamp = NewTimestamp();

        while (timestamp <= lastTimestamp)
        {
            timestamp = NewTimestamp();
        }
        return timestamp;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public long NewId()
    {
        var timestamp = NewTimestamp();

        if (timestamp < lastTimestamp)
        {
            throw new InvalidOperationException($"Clock moved backwards. Refusing to generate id for {lastTimestamp - timestamp} milliseconds");
        }

        if (lastTimestamp == timestamp)
        {
            sequence = (sequence + 1) & sequenceMask;
            if (sequence == 0)
            {
                timestamp = NextTimeStamp(lastTimestamp);
            }
        }
        else
        {
            sequence = 0;
        }

        lastTimestamp = timestamp;

        return ((timestamp - twepoch) << timestampLeftShift)
            | (DataCenterId << datacenterIdShift)
            | (WorkerId << workerIdShift)
            | sequence;
    }
}
