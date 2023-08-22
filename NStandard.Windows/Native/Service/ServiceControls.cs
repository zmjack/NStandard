﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStandard.Windows.Native.Service
{
    [Flags]
    internal enum ServiceControls : uint
    {
        SERVICE_CONTROL_CONTINUE = 0x00000003,
        SERVICE_CONTROL_INTERROGATE = 0x00000004,
        SERVICE_CONTROL_NETBINDADD = 0x00000007,
        SERVICE_CONTROL_NETBINDDISABLE = 0x0000000A,
        SERVICE_CONTROL_NETBINDENABLE = 0x00000009,
        SERVICE_CONTROL_NETBINDREMOVE = 0x00000008,
        SERVICE_CONTROL_PARAMCHANGE = 0x00000006,
        SERVICE_CONTROL_PAUSE = 0x00000002,
        SERVICE_CONTROL_STOP = 0x00000001,
    }
}
