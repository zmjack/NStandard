using NStandard;

namespace System
{
    public static class XEnum
    {
        public static long ToInt64(this Enum @this) => (long)Convert.ChangeType(@this, typeof(long));

#if NET35
        // Tested: NET35
        public static bool HasFlag(this Enum @this, Enum flag)
        {
            if (flag == null)
                throw new ArgumentNullException(nameof(flag));

            if (@this.GetType() != flag.GetType())
            {
                throw new ArgumentException(SR.Format(SR.Argument_EnumTypeDoesNotMatch, flag.GetType(), @this.GetType()));
            }

            return (ToInt64(@this) & ToInt64(flag)) > 0;
        }
#endif
    }
}
