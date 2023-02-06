using System;
using System.Runtime.CompilerServices;

namespace NStandard.Flows
{
    public static class NumberFlow
    {
        public static double DefaultIfNonNormal(double number) => IsNormal(number) ? number : 0;
        public static float DefaultIfNonNormal(float number) => IsNormal(number) ? number : 0;

#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET451_OR_GREATER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        private unsafe static int SingleToInt32Bits(float value)
        {
            return *(int*)(&value);
        }

        private static bool IsNormal(float f)
        {
            int num = SingleToInt32Bits(f);
            num &= 0x7FFFFFFF;
            if (num < 2139095040 && num != 0)
            {
                return (num & 0x7F800000) != 0;
            }
            return false;
        }

        private static bool IsNormal(double d)
        {
            long num = BitConverter.DoubleToInt64Bits(d);
            num &= 0x7FFFFFFFFFFFFFFFL;
            if (num < 9218868437227405312L && num != 0L)
            {
                return (num & 0x7FF0000000000000L) != 0;
            }
            return false;
        }

    }
}
