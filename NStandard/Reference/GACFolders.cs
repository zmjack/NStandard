using System;

namespace NStandard.Reference
{
    [Flags]
    public enum GACFolders
    {
        All = Shared | SDK | NuGet | Reserved,
        Shared = 1,
        SDK = 2,
        NuGet = 4,
        Reserved = 8,
    }
}
