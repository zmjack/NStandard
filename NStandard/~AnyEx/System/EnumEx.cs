using System;
using System.Collections.Generic;
using System.Linq;

namespace NStandard
{
    public static class EnumEx
    {
        public static EnumOption[] GetOptions(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException($"The `{nameof(enumType)}` must be enum type.");

            var names = Enum.GetNames(enumType);
            return names.Select(name => new EnumOption(enumType, name)).ToArray();
        }

        public static EnumOption<TEnum, TUnderlying>[] GetOptions<TEnum, TUnderlying>()
            where TEnum : struct
            where TUnderlying : struct
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum) throw new ArgumentException($"The `{nameof(enumType)}` must be enum type.");

            var names = Enum.GetNames(enumType);
            return names.Select(name => new EnumOption<TEnum, TUnderlying>(name)).ToArray();
        }

        public static EnumOption[] GetFlagOptions(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException($"The `{nameof(enumType)}` must be enum type.");

            var flags = GetFlags(enumType);
            var names = Enum.GetNames(enumType).Where(x => flags.Contains(x.ToString()));
            return names.Select(name => new EnumOption(enumType, name)).ToArray();
        }

        public static EnumOption<TEnum, TUnderlying>[] GetFlagOptions<TEnum, TUnderlying>()
            where TEnum : struct
            where TUnderlying : struct
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum) throw new ArgumentException($"The `{nameof(enumType)}` must be enum type.");

            var flags = GetFlags(enumType);
            var names = Enum.GetNames(enumType).Where(x => flags.Contains(x.ToString()));
            return names.Select(name => new EnumOption<TEnum, TUnderlying>(name)).ToArray();
        }

        public static TEnum[] GetFlags<TEnum>() where TEnum : Enum
        {
            var values = Enum.GetValues(typeof(TEnum)) as TEnum[];
            var flags = values.Where(x =>
            {
                var v = (int)(object)x;
                if (v == 0) return false;
                return (v & (v - 1)) == 0;
            }).ToArray();
            return flags;
        }

        public static object[] GetFlags(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException("Type provided must be an Enum.", nameof(enumType));

            var values = Enum.GetValues(enumType);
            var list = new List<object>();
            foreach (var value in values)
            {
                var v = (int)value;
                if (v == 0) continue;
                if ((v & (v - 1)) == 0) list.Add(value);
            }
            return list.ToArray();
        }

    }
}
