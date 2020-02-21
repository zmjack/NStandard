using System;
using System.ComponentModel;
using System.Reflection;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XAppDomain
    {
        //TODO: Long-term Maintenance
        public static Assembly GetCoreLibAssembly(this AppDomain @this)
        {
            var assemblies = @this.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                switch (assembly.FullName)
                {
                    case "System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e":
                    case "mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089":
                    case "mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089":
                        return assembly;
                }
            }
            return null;
        }

    }
}
