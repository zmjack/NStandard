using System.Runtime.InteropServices;

namespace NStandard.Windows.Native.Service;

internal struct SERVICE_DESCRIPTIONW
{
    [MarshalAs(UnmanagedType.LPWStr)] public string Description;
}
