#if NET7_0_OR_GREATER
using System;
using System.Runtime.CompilerServices;

namespace NStandard.Algorithm;

/// <summary>
/// Snowflake128 generator.
/// <para>Source: Scala code (<see href="https://github.com/twitter-archive/snowflake" />).</para>
/// <para>ID   = Timestamp(64b) DataCenterId(16b) WorkerId(16b) Sequnce(32b) </para>
/// <para>GUID = Sequnce(8c) - WorkerId(4c) - DataCenterId(4c) - Timestamp(4c+12c) </para>
/// </summary>
/// <inheritdoc cref="SnowflakeWorker"/>
public class SnowflakeWorker128
{
    private static readonly int workerIdBits = 16;
    private static readonly int datacenterIdBits = 16;
    private static readonly int maxWorkerId = -1 ^ (-1 << workerIdBits);
    private static readonly int maxDatacenterId = -1 ^ (-1 << datacenterIdBits);

    private static readonly int sequenceBits = 32;
    private static readonly int workerIdShift = sequenceBits;
    private static readonly int datacenterIdShift = sequenceBits + workerIdBits;
    private static readonly int timestampLeftShift = sequenceBits + workerIdBits + datacenterIdBits;

    private static readonly Int128 sequenceMask = (Int128)(-1) ^ ((Int128)(-1) << sequenceBits);

    private Int128 lastTimestamp = -1L;
    private Int128 sequence = 0;

    public Int128 WorkerId { get; internal set; }
    public Int128 DataCenterId { get; internal set; }

    public SnowflakeWorker128(Int128 workerId, Int128 dataCenterId)
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

    protected virtual Int128 NewTimestamp() => DateTime.Now.ToUnixTimeMilliseconds();

    protected Int128 NextTimeStamp(Int128 lastTimestamp)
    {
        var timestamp = NewTimestamp();

        while (timestamp <= lastTimestamp)
        {
            timestamp = NewTimestamp();
        }
        return timestamp;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Int128 NewId()
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

        return (timestamp << timestampLeftShift)
            | (DataCenterId << datacenterIdShift)
            | (WorkerId << workerIdShift)
            | sequence;
    }

    public Guid NewGuid()
    {
        var id = NewId();
        return new Guid(BitConverterEx.GetBytes(id));
    }
}
#endif
