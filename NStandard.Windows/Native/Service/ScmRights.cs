using System;

namespace NStandard.Windows.Native.Service;

[Flags]
internal enum ScmRights
{
    None = 0,
    SC_MANAGER_ALL_ACCESS = 0xF003F,
    SC_MANAGER_CREATE_SERVICE = 0x0002,
    SC_MANAGER_CONNECT = 0x0001,
    SC_MANAGER_ENUMERATE_SERVICE = 0x0004,
    SC_MANAGER_LOCK = 0x0008,
    SC_MANAGER_MODIFY_BOOT_CONFIG = 0x0020,
    SC_MANAGER_QUERY_LOCK_STATUS = 0x0010,
}
