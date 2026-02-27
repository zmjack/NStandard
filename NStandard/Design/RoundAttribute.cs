namespace NStandard.Design;

[AttributeUsage(AttributeTargets.Property)]
public class RoundAttribute : Attribute
{
    public int Digits { get; }
    public MidpointRounding Mode { get; }

    public RoundAttribute(int digits)
    {
        Digits = digits;
        Mode = MidpointRounding.ToEven;
    }

    public RoundAttribute(int digits, MidpointRounding mode)
    {
        Digits = digits;
        Mode = mode;
    }
}
