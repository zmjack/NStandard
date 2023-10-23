﻿using System;

namespace NStandard
{
    public class EnumOption : IEquatable<EnumOption>
    {
        public object Enum { get; }
        public string Name { get; }
        public object Value { get; }

        [Obsolete("Use Enum instead.")]
        public object EnumValue => Enum;

        [Obsolete("Use Value instead.")]
        public object UnderlyingValue => Value;

        public EnumOption(Type enumType, string name)
        {
            var underlyingType = System.Enum.GetUnderlyingType(enumType);
            Enum = System.Enum.Parse(enumType, name);
            Name = name;
            Value = Convert.ChangeType(Enum, underlyingType);
        }

        public bool Equals(EnumOption other)
        {
            return Enum.Equals(other.Enum) && Value.Equals(other.Value);
        }
    }

    public class EnumOption<TEnum, TValue> : EnumOption, IEquatable<EnumOption<TEnum, TValue>>
        where TEnum : Enum
        where TValue : struct
    {
        public new TEnum Enum => (TEnum)base.Enum;
        public new TValue Value => (TValue)base.Value;

        public new TEnum EnumValue => (TEnum)base.Enum;
        public new TValue UnderlyingValue => (TValue)base.Value;

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

        public EnumOption(TValue value) : base(typeof(TEnum), value.ToString())
        {
            CheckUnderlyingType();
        }

        public bool Equals(EnumOption<TEnum, TValue> other)
        {
            return Enum.Equals(other.Enum) && Value.Equals(other.Value);
        }
    }

}
