namespace NStandard.Data;

public enum MomentType
{
    Undefined = 0,

    [Obsolete("Not supported")]
    Nanosecond = 1,
    [Obsolete("Not supported")]
    Microsecond = Nanosecond * 0b1000,

    Millisecond = Microsecond * 0b1000,
    Second = Millisecond * 0b1000,
    Minute = Second * 0b100,
    Hour = Minute * 0b100,
    Day = Hour * 0b100,
    Week = Day * 0b10,
    Month = Day * 0b100,
    Quarter = Month * 0b10,
    Year = Month * 0b100,
}
