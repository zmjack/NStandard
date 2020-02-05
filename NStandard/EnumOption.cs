using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    public class EnumOption : IEquatable<EnumOption>
    {
        public object Value { get; private set; }
        public object UnderlyingValue { get; private set; }

        public EnumOption(Type enumType, string value)
        {
            var underlyingType = Enum.GetUnderlyingType(enumType);
            Value = Enum.Parse(enumType, value);
            UnderlyingValue = Convert.ChangeType(Value, underlyingType);
        }

        public bool Equals(EnumOption other)
        {
            return Value.Equals(other.Value) && UnderlyingValue.Equals(other.UnderlyingValue);
        }
    }

    public class EnumOption<TEnum, TUnderlying> : EnumOption, IEquatable<EnumOption<TEnum, TUnderlying>>
        where TEnum : struct
        where TUnderlying : struct
    {
        public new TEnum Value => (TEnum)base.Value;
        public new TUnderlying UnderlyingValue => (TUnderlying)base.Value;

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
            return Value.Equals(other.Value) && UnderlyingValue.Equals(other.UnderlyingValue);
        }
    }

}
