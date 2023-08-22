using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NStandard.Windows.Native.Service.NativeMethods;
using NStandard.Windows.Native.Service;
using System.Runtime.InteropServices;

namespace NStandard.Windows
{
    public static class WindowsService
    {
        public static bool Start(string serviceName, int argc, string[] args)
        {
            var hManager = OpenSCManager(null, null, ScmRights.None);
            if (hManager == IntPtr.Zero) return false;

            var hService = OpenService(hManager, serviceName, ServiceRights.SERVICE_START);
            if (hService == IntPtr.Zero)
            {
                CloseServiceHandle(hManager);
                return false;
            }

            if (!StartService(hService, argc, args))
            {
                CloseServiceHandle(hService);
                CloseServiceHandle(hManager);
                return false;
            }

            CloseServiceHandle(hService);
            CloseServiceHandle(hManager);
            return true;
        }

        public static bool Stop(string serviceName)
        {
            var hManager = OpenSCManager(null, null, ScmRights.None);
            if (hManager == IntPtr.Zero) return false;

            var hService = OpenService(hManager, serviceName, ServiceRights.SERVICE_STOP);
            if (hService == IntPtr.Zero)
            {
                CloseServiceHandle(hManager);
                return false;
            }

            var status = new ServiceStatus();
            if (!ControlService(hService, ServiceControls.SERVICE_CONTROL_STOP, ref status))
            {
                CloseServiceHandle(hService);
                CloseServiceHandle(hManager);
                return false;
            }

            CloseServiceHandle(hService);
            CloseServiceHandle(hManager);
            return true;
        }

        public static bool Install(string serviceName, string displayName, string description, ServiceStartType startType, ServiceErrorControl errorControl, string binaryPath, string group = null)
        {
            var hManager = OpenSCManager(null, null, ScmRights.SC_MANAGER_CREATE_SERVICE);
            if (hManager == IntPtr.Zero) return false;

            var hService = CreateService(hManager, serviceName, displayName, ServiceRights.SERVICE_CHANGE_CONFIG, ServiceType.SERVICE_WIN32_OWN_PROCESS, startType, errorControl, binaryPath, group);
            if (hService == IntPtr.Zero)
            {
                CloseServiceHandle(hManager);
                return false;
            }

            var nDescription = Marshal.AllocHGlobal(Marshal.SizeOf<SERVICE_DESCRIPTIONW>());
            Marshal.StructureToPtr(new SERVICE_DESCRIPTIONW
            {
                Description = description,
            }, nDescription, false);
            if (!ChangeServiceConfig2(hService, ServiceConfigInfoLevel.SERVICE_CONFIG_DESCRIPTION, nDescription))
            {
                Marshal.FreeHGlobal(nDescription);
                CloseServiceHandle(hService);
                CloseServiceHandle(hManager);
                return false;
            }

            Marshal.FreeHGlobal(nDescription);
            CloseServiceHandle(hService);
            CloseServiceHandle(hManager);
            return true;
        }

        public static bool Uninstall(string serviceName)
        {
            var hManager = OpenSCManager(null, null, ScmRights.None);
            if (hManager == IntPtr.Zero)
            {
                return false;
            }
            var hService = OpenService(hManager, serviceName, ServiceRights.SERVICE_QUERY_STATUS | ServiceRights.DELETE | ServiceRights.SERVICE_STOP);
            if (hService == IntPtr.Zero)
            {
                CloseServiceHandle(hManager);
                return false;
            }

            var status = new ServiceStatus();
            if (!QueryServiceStatus(hService, ref status))
            {
                CloseServiceHandle(hService);
                CloseServiceHandle(hManager);
                return false;
            }

            if (status.dwCurrentState != ServiceState.SERVICE_STOPPED)
            {
                if (!ControlService(hService, ServiceControls.SERVICE_CONTROL_STOP, ref status))
                {
                    CloseServiceHandle(hService);
                    CloseServiceHandle(hManager);
                    return false;
                }
            }

            if (!DeleteService(hService))
            {
                CloseServiceHandle(hService);
                CloseServiceHandle(hManager);
                return false;
            }

            CloseServiceHandle(hService);
            CloseServiceHandle(hManager);
            return true;
        }
    }
}
