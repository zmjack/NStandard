using System;
using System.ComponentModel;
using System.Reflection;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class EventInfoExtensions
    {
        public static void AddEventHandler(this EventInfo @this, object declaringObject, MethodInfo method)
        {
            @this.AddEventHandler(declaringObject, Delegate.CreateDelegate(@this.EventHandlerType, method));
        }

    }
}
