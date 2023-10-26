using System;
using System.ComponentModel;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class IDisposableExtension
{
    public static void DisposeAll<T>(this T[] @this) where T : IDisposable
    {
        foreach (var item in @this)
        {
            item.Dispose();
        }
    }
}
