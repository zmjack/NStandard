using System;
using System.Reflection;

namespace NStandard
{
    public static class XEventInfo
    {
        public static void AddEvent(this EventInfo @this, object target, MethodInfo method)
        {
            @this.AddEventHandler(target, Delegate.CreateDelegate(@this.EventHandlerType, method));
        }

    }
}
