using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStandard.Windows.Native.Service
{
    public enum ServiceErrorControl
    {
        /// <summary>
        /// The startup program logs the error in the event log, if possible. If the last-known-good configuration is being started, the startup operation fails. Otherwise, the system is restarted with the last-known good configuration.
        /// </summary>
        SERVICE_ERROR_CRITICAL = 0x00000003,

        /// <summary>
        /// The startup program ignores the error and continues the startup operation.
        /// </summary>
        SERVICE_ERROR_IGNORE = 0x00000000,

        /// <summary>
        /// The startup program logs the error in the event log but continues the startup operation.
        /// </summary>
        SERVICE_ERROR_NORMAL = 0x00000001,

        /// <summary>
        /// The startup program logs the error in the event log. If the last-known-good configuration is being started, the startup operation continues. Otherwise, the system is restarted with the last-known-good configuration.
        /// </summary>
        SERVICE_ERROR_SEVERE = 0x00000002,

    }


}
