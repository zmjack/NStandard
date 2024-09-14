namespace NStandard.Collections;

[Flags]
public enum SlidingMode
{
    None = 0,
    PadLeft = 1,
    PadRight = 2,
    PadBoth = PadLeft | PadRight,
}
