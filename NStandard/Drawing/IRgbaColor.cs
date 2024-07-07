namespace NStandard.Drawing;

public interface IRgbaColor
{
    byte R { get; set; }
    byte G { get; set; }
    byte B { get; set; }
    byte A { get; set; }
    uint Value { get; }
}
