using System;

namespace NStandard
{
    public class EnumOption : IEquatable<EnumOption>
    {
        public object EnumValue { get; private set; }
        public object UnderlyingValue { get; private set; }

        public EnumOption(Type enumType, string value)
        {
            var underlyingType = Enum.GetUnderlyingType(enumType);
            EnumValue = Enum.Parse(enumType, value);
            UnderlyingValue = Convert.ChangeType(EnumValue, underlyingType);
        }

        public bool Equals(EnumOption other)
        {
            return EnumValue.Equals(other.EnumValue) && UnderlyingValue.Equals(other.UnderlyingValue);
        }
    }

    public class EnumOption<TEnum, TUnderlying> : EnumOption, IEquatable<EnumOption<TEnum, TUnderlying>>
        where TEnum : Enum
        where TUnderlying : struct
    {
        public new TEnum EnumValue => (TEnum)base.EnumValue;
        public new TUnderlying UnderlyingValue => (TUnderlying)base.EnumValue;

        private void CheckUnderlyingType()
        {
            var underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
            if (underlyingType != typeof(TUnderlying))
                throw new InvalidOperationException($"The `{nameof(TEnum)}`'s underlying type should be {underlyingType.FullName}.");
        }

        public EnumOption(string value) : base(typeof(TEnum), value)
        {
            CheckUnderlyingType();
        }

        public EnumOption(TUnderlying value) : base(typeof(TEnum), value.ToString())
        {
            CheckUnderlyingType();
        }

        public bool Equals(EnumOption<TEnum, TUnderlying> other)
        {
            return EnumValue.Equals(other.EnumValue) && UnderlyingValue.Equals(other.UnderlyingValue);
        }
    }

}
