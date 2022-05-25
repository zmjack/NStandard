#if NET6_0_OR_GREATER
using System;
using System.ComponentModel;
using System.Linq;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XDateOnly
    {
        /// <summary>
        /// Get the start point of the sepecified year.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateOnly StartOfYear(this DateOnly @this) => new(@this.Year, 1, 1);

        /// <summary>
        /// Get the start point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateOnly StartOfMonth(this DateOnly @this) => new(@this.Year, @this.Month, 1);

        /// <summary>
        /// Get the end point of the sepecified year.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateOnly EndOfYear(this DateOnly @this) => new(@this.Year, 12, 31);

        /// <summary>
        /// Get the end point of the sepecified month.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static DateOnly EndOfMonth(this DateOnly @this)
        {
            if (new[] { 1, 3, 5, 7, 8, 10, 12 }.Contains(@this.Month))
                return new DateOnly(@this.Year, @this.Month, 31);
            else if (new[] { 4, 6, 9, 11 }.Contains(@this.Month))
                return new DateOnly(@this.Year, @this.Month, 30);
            else
            {
                if (DateTime.IsLeapYear(@this.Year))
                    return new DateOnly(@this.Year, @this.Month, 29);
                else return new DateOnly(@this.Year, @this.Month, 28);
            }
        }
    }
}
#endif
