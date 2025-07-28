namespace NStandard.Infrastructure;

public class EnumOption : IEquatable<EnumOption>
{
    public Enum Enum { get; }
    public string Name { get; }
    public object Value { get; }

    public EnumOption(Type enumType, string name)
    {
        var underlyingType = Enum.GetUnderlyingType(enumType);
        Enum = (Enum.Parse(enumType, name) as Enum)!;
        Name = name;
        Value = Convert.ChangeType(Enum, underlyingType);
    }

    public bool Equals(EnumOption? other)
    {
        return other is not null && Enum.Equals(other.Enum) && Value.Equals(other.Value);
    }
}

public class EnumOption<TEnum, TValue> : EnumOption, IEquatable<EnumOption<TEnum, TValue>>
    where TEnum : Enum
    where TValue : struct
{
    public new TEnum Enum => (TEnum)base.Enum;
    public new TValue Value => (TValue)base.Value;

    private void CheckUnderlyingType()
    {
        var underlyingType = System.Enum.GetUnderlyingType(typeof(TEnum));
        if (underlyingType != typeof(TValue))
            throw new InvalidOperationException($"The `{nameof(TEnum)}`'s underlying type should be {underlyingType.FullName}.");
    }

    public EnumOption(string value) : base(typeof(TEnum), value)
    {
        CheckUnderlyingType();
    }

    public EnumOption(TValue value) : base(typeof(TEnum), value.ToString()!)
    {
        CheckUnderlyingType();
    }

    public bool Equals(EnumOption<TEnum, TValue>? other)
    {
        return other is not null && Enum.Equals(other.Enum) && Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not EnumOption<TEnum, TValue> enumOption) return false;
        return Equals(enumOption);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
