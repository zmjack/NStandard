using System;
using System.Linq;

namespace NStandard
{
    public static class EnumEx
    {
        public static EnumOption[] GetOptions(Type enumType)
        {
            if (!enumType.IsEnum) throw new ArgumentException($"The `{nameof(enumType)}` must be enum type.");

            var names = Enum.GetNames(enumType);
            var ret = names.Select(name => new EnumOption(enumType, name)).ToArray();
            return ret;
        }

        public static EnumOption<TEnum, TUnderlying>[] GetOptions<TEnum, TUnderlying>()
            where TEnum : struct
            where TUnderlying : struct
        {
            var enumType = typeof(TEnum);

            if (!enumType.IsEnum) throw new ArgumentException($"The `{nameof(enumType)}` must be enum type.");

            var names = Enum.GetNames(enumType);
            var ret = names.Select(name => new EnumOption<TEnum, TUnderlying>(name)).ToArray();
            return ret;
        }

    }
}
