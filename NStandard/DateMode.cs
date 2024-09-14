namespace NStandard;

/// <summary>
/// Indicates Weekday or Weekend.
/// </summary>
[Flags]
public enum DayMode
{
    /// <summary>
    /// Undefined.
    /// </summary>
    Undefined,

    /// <summary>
    /// Indicates Monday, Tuesday, Wednesday, Thursday, Friday.
    /// </summary>
    Weekday = 1,

    /// <summary>
    /// Indicates Saturday, Sunday.
    /// </summary>
    Weekend = 2,
}
