using System.ComponentModel;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XInt64
    {
        /// <summary>
        /// Returns whether the specified number is odd.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsOdd(this long @this) => (@this & 1) == 1;

        /// <summary>
        /// Returns whether the specified number is even.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsEven(this long @this) => (@this & 1) == 0;

        /// <summary>
        /// Gets the positive integer modulus. (Unlike the operator %, this method always returns a positive number)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static long Mod(this long @this, long mod)
        {
            if (@this < 0)
                return @this % mod + mod;
            else return @this % mod;
        }

    }
}
