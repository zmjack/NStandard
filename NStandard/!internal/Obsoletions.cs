using System.ComponentModel;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Advanced)]
internal static class Obsoletions
{
    internal const string Reserved = "Reserved.";
    internal const string PlanToRemove = "Plan to remove.";
    internal const string MayChangeOrBeRemoved = "This API may change or be removed in future releases.";
    internal const string ProductionIncompatible = "Do not use this function in PRODUCTION environment. GC may change the pointer of MANAGED OBJECT..";
}
