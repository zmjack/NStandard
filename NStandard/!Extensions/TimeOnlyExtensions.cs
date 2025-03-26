#if NET6_0_OR_GREATER
using System;
using System.ComponentModel;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class TimeOnlyExtensions
{
    /// <summary>
    /// Get the start point of the specified hour.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static TimeOnly StartOfHour(this TimeOnly @this) => new(@this.Hour, 0, 0, 0);

    /// <summary>
    /// Get the start point of the specified minute.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static TimeOnly StartOfMinute(this TimeOnly @this) => new(@this.Hour, @this.Minute, 0, 0);

    /// <summary>
    /// Get the start point of the specified second.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static TimeOnly StartOfSecond(this TimeOnly @this) => new(@this.Hour, @this.Minute, @this.Second, 0);

    /// <summary>
    /// Get the start point of the specified hour.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static TimeOnly EndOfHour(this TimeOnly @this) => new(@this.Hour, 59, 59, 999);

    /// <summary>
    /// Get the start point of the specified minute.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static TimeOnly EndOfMinute(this TimeOnly @this) => new(@this.Hour, @this.Minute, 59, 999);

    /// <summary>
    /// Get the start point of the specified second.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static TimeOnly EndOfSecond(this TimeOnly @this) => new(@this.Hour, @this.Minute, @this.Second, 999);

}
#endif
