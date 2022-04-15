using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard
{
    /// <summary>
    /// Indicates Weekday or Weekend.
    /// </summary>
    public enum DayMode
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// Indicates Monday, Tuesday, Wednesday, Thursday, Friday.
        /// </summary>
        Weekday,

        /// <summary>
        /// Indicates Saturday, Sunday.
        /// </summary>
        Weekend,
    }
}
