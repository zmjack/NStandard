using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NStandard.Windows.Native.Service
{
    internal struct SERVICE_DESCRIPTIONW
    {
        [MarshalAs(UnmanagedType.LPWStr)] public string Description;
    }
}
