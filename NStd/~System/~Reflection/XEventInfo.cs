using System;
using System.ComponentModel;
using System.Reflection;

namespace NStd
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XEventInfo
    {
        public static void AddEvent(this EventInfo @this, object target, MethodInfo method)
        {
            @this.AddEventHandler(target, Delegate.CreateDelegate(@this.EventHandlerType, method));
        }

    }
}
