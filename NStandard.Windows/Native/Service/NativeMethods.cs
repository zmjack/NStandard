using System;
using System.Runtime.InteropServices;

namespace NStandard.Windows.Native.Service
{
    internal class NativeMethods
    {
        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr OpenSCManager(string machineName, string databaseName, ScmRights dwAccess);

        [DllImport("advapi32.dll", EntryPoint = "OpenServiceW", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, ServiceRights dwDesiredAccess);

        [DllImport("advapi32.dll", EntryPoint = "QueryServiceStatus", CharSet = CharSet.Auto)]
        internal static extern bool QueryServiceStatus(IntPtr hService, ref ServiceStatus dwServiceStatus);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateService(
            IntPtr hSCManager,
            string lpServiceName,
            string lpDisplayName,
            ServiceRights dwDesiredAccess,
            ServiceType dwServiceType,
            ServiceStartType dwStartType,
            ServiceErrorControl dwErrorControl,
            string lpBinaryPathName,
            [Optional] string lpLoadOrderGroup,
            [Optional] string lpdwTagId,    // only string so we can pass null
            [Optional] string lpDependencies,
            [Optional] string lpServiceStartName,
            [Optional] string lpPassword);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ControlService(IntPtr hService, ServiceControls dwControl, ref ServiceStatus lpServiceStatus);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseServiceHandle(IntPtr hSCObject);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool StartService(IntPtr hService, int dwNumServiceArgs, string[] lpServiceArgVectors);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteService(IntPtr hService);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ChangeServiceConfig2(IntPtr hService, ServiceConfigInfoLevel dwInfoLevel, IntPtr lpInfo);

    }
}
