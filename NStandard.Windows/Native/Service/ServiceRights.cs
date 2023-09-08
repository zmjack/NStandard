using System;

namespace NStandard.Windows.Native.Service
{
    [Flags]
    internal enum ServiceRights : uint
    {
        /// <summary>
        /// Includes STANDARD_RIGHTS_REQUIRED in addition to all access rights in this table.
        /// </summary>
        SERVICE_ALL_ACCESS = 0xF01FF,

        /// <summary>
        /// Required to call the ChangeServiceConfig or ChangeServiceConfig2 function to change the service configuration. Because this grants the caller the right to change the executable file that the system runs, it should be granted only to administrators.
        /// </summary>
        SERVICE_CHANGE_CONFIG = 0x0002,

        /// <summary>
        /// Required to call the EnumDependentServices function to enumerate all the services dependent on the service.
        /// </summary>
        SERVICE_ENUMERATE_DEPENDENTS = 0x0008,

        /// <summary>
        /// Required to call the ControlService function to ask the service to report its status immediately.
        /// </summary>
        SERVICE_INTERROGATE = 0x0080,

        /// <summary>
        /// Required to call the ControlService function to pause or continue the service.
        /// </summary>
        SERVICE_PAUSE_CONTINUE = 0x0040,

        /// <summary>
        /// Required to call the QueryServiceConfig and QueryServiceConfig2 functions to query the service configuration.
        /// </summary>
        SERVICE_QUERY_CONFIG = 0x0001,

        /// <summary>
        /// Required to call the QueryServiceStatus or QueryServiceStatusEx function to ask the service control manager about the status of the service.
        /// Required to call the NotifyServiceStatusChange function to receive notification when a service changes status.
        /// </summary>
        SERVICE_QUERY_STATUS = 0x0004,

        /// <summary>
        /// Required to call the StartService function to start the service.
        /// </summary>
        SERVICE_START = 0x0010,

        /// <summary>
        /// Required to call the ControlService function to stop the service.
        /// </summary>
        SERVICE_STOP = 0x0020,

        /// <summary>
        /// Required to call the ControlService function to specify a user-defined control code.
        /// </summary>
        SERVICE_USER_DEFINED_CONTROL = 0x0100,

        /// <summary>
        /// Required to call the DeleteService function to delete the service.
        /// </summary>
        DELETE = 0x10000,

        /// <summary>
        /// Required to call the QueryServiceObjectSecurity function to query the security descriptor of the service object.
        /// </summary>
        READ_CONTROL = 0x20000,

        /// <summary>
        /// Required to call the SetServiceObjectSecurity function to modify the Dacl member of the service object's security descriptor.
        /// </summary>
        WRITE_DAC = 0x40000,

        /// <summary>
        /// Required to call the SetServiceObjectSecurity function to modify the Owner and Group members of the service object's security descriptor.
        /// </summary>
        WRITE_OWNER = 0x80000,
    }
}
