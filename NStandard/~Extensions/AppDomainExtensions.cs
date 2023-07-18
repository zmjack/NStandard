using System;
using System.ComponentModel;
using System.Reflection;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class AppDomainExtensions
    {
        //TODO: Long-term Maintenance
        public static Assembly GetCoreLibAssembly(this AppDomain @this)
        {
            var assemblies = @this.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                switch (assembly.GetName().Name)
                {
                    // "System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"
                    // "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
                    case "System.Private.CoreLib":
                    case "mscorlib": return assembly;
                }
            }
            return null;
        }

    }
}
