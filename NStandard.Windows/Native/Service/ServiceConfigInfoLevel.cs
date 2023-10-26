namespace NStandard.Windows.Native.Service;

internal enum ServiceConfigInfoLevel
{
    /// <summary>
    /// The lpInfo parameter is a pointer to a SERVICE_DELAYED_AUTO_START_INFO structure.\\Windows Server 2003 and Windows XP:  This value is not supported.
    /// </summary>
    SERVICE_CONFIG_DELAYED_AUTO_START_INFO = 3,

    /// <summary>
    /// The lpInfo parameter is a pointer to a SERVICE_DESCRIPTION structure.
    /// </summary>
    SERVICE_CONFIG_DESCRIPTION = 1,

    /// <summary>
    /// The lpInfo parameter is a pointer to a SERVICE_FAILURE_ACTIONS structure.
    /// If the service controller handles the SC_ACTION_REBOOT action, the caller must have the SE_SHUTDOWN_NAME privilege. For more information, see Running with Special Privileges.
    /// </summary>
    SERVICE_CONFIG_FAILURE_ACTIONS = 2,

    /// <summary>
    /// The lpInfo parameter is a pointer to a SERVICE_FAILURE_ACTIONS_FLAG structure.
    /// Windows Server 2003 and Windows XP:  This value is not supported.
    /// </summary>
    SERVICE_CONFIG_FAILURE_ACTIONS_FLAG = 4,

    /// <summary>
    /// The lpInfo parameter is a pointer to a SERVICE_PREFERRED_NODE_INFO structure.
    /// Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This value is not supported.
    /// </summary>
    SERVICE_CONFIG_PREFERRED_NODE = 9,

    /// <summary>
    /// The lpInfo parameter is a pointer to a SERVICE_PRESHUTDOWN_INFO structure.
    /// Windows Server 2003 and Windows XP:  This value is not supported.
    /// </summary>
    SERVICE_CONFIG_PRESHUTDOWN_INFO = 7,

    /// <summary>
    /// The lpInfo parameter is a pointer to a SERVICE_REQUIRED_PRIVILEGES_INFO structure.
    /// Windows Server 2003 and Windows XP:  This value is not supported.
    /// </summary>
    SERVICE_CONFIG_REQUIRED_PRIVILEGES_INFO = 6,

    /// <summary>
    /// The lpInfo parameter is a pointer to a SERVICE_SID_INFO structure.
    /// </summary>
    SERVICE_CONFIG_SERVICE_SID_INFO = 5,

    /// <summary>
    /// The lpInfo parameter is a pointer to a SERVICE_TRIGGER_INFO structure. This value is not supported by the ANSI version of ChangeServiceConfig2.
    /// Windows Server 2008, Windows Vista, Windows Server 2003 and Windows XP:  This value is not supported until Windows Server 2008 R2.
    /// </summary>
    SERVICE_CONFIG_TRIGGER_INFO = 8,

    /// <summary>
    /// The lpInfo parameter is a pointer a SERVICE_LAUNCH_PROTECTED_INFO structure.
    /// </summary>
    SERVICE_CONFIG_LAUNCH_PROTECTED = 12,

}
