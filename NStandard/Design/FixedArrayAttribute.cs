namespace NStandard.Design;

[AttributeUsage(AttributeTargets.Property)]
public class FixedArrayAttribute : Attribute
{
    public int Length { get; }

    public FixedArrayAttribute(int length)
    {
        Length = length;
    }
}
