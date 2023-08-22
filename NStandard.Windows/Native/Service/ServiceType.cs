using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStandard.Windows.Native.Service
{
    internal enum ServiceType
    {
        /// <summary>
        /// Reserved.
        /// </summary>
        SERVICE_ADAPTER = 0x00000004,

        /// <summary>
        /// File system driver service.
        /// </summary>
        SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,

        /// <summary>
        /// Driver service.
        /// </summary>
        SERVICE_KERNEL_DRIVER = 0x00000001,

        /// <summary>
        /// Reserved.
        /// </summary>
        SERVICE_RECOGNIZER_DRIVER = 0x00000008,

        /// <summary>
        /// Service that runs in its own process.
        /// </summary>
        SERVICE_WIN32_OWN_PROCESS = 0x00000010,

        /// <summary>
        /// Service that shares a process with one or more other services. For more information, see Service Programs.
        /// </summary>
        SERVICE_WIN32_SHARE_PROCESS = 0x00000020,
    }
}
