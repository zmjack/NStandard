﻿using System.Diagnostics;

namespace NStandard;

[DebuggerDisplay("{Target}")]
public struct Val<T>(T target)
{
    public T Target { get; } = target;

    public static implicit operator Val<T>(T target)
    {
        return new Val<T>(target);
    }
}
