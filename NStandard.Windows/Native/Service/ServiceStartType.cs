namespace NStandard.Windows.Native.Service
{
    public enum ServiceStartType
    {
        /// <summary>
        /// A service started automatically by the service control manager during system startup. For more information, see Automatically Starting Services.
        /// </summary>
        SERVICE_AUTO_START = 0x00000002,

        /// <summary>
        /// A device driver started by the system loader. This value is valid only for driver services.
        /// </summary>
        SERVICE_BOOT_START = 0x00000000,

        /// <summary>
        /// A service started by the service control manager when a process calls the StartService function. For more information, see Starting Services on Demand.
        /// </summary>
        SERVICE_DEMAND_START = 0x00000003,

        /// <summary>
        /// A service that cannot be started. Attempts to start the service result in the error code ERROR_SERVICE_DISABLED.
        /// </summary>
        SERVICE_DISABLED = 0x00000004,

        /// <summary>
        /// A device driver started by the IoInitSystem function. This value is valid only for driver services.
        /// </summary>
        SERVICE_SYSTEM_START = 0x00000001,

    }
}
