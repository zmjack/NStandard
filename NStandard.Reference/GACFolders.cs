using System;

namespace NStandard.Reference
{
    [Flags]
    public enum GACFolders
    {
        All = Shared | SDK | NuGet,
        Shared = 1,
        SDK = 2,
        NuGet = 4,
    }
}
