using NStandard.Reference;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace NStandard
{
    public class AssemblyContext : AssemblyLoadContext
    {
        public Assembly RootAssembly;
        public AssemblyName[] ReferencedAssemblies;
        public DotNetFramework Framework;
        public string LocationDirectory;

        public AssemblyContext(string path, DotNetFramework framework)
        {
            LocationDirectory = Path.GetDirectoryName(path);
            RootAssembly = LoadFromAssemblyPath(path);
            Framework = framework;
            ReferencedAssemblies = RootAssembly.GetReferencedAssemblies();
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var useAssembly = ReferencedAssemblies.FirstOrDefault(x => x.Name == assemblyName.Name);

            string dll;
            if (useAssembly != null)
                dll = GAC.GetAssemblyFile(useAssembly.Name, useAssembly.Version, Framework, GACFolders.All, new[] { LocationDirectory });
            else dll = GAC.GetAssemblyFile(assemblyName.Name, assemblyName.Version, Framework, GACFolders.All, new[] { LocationDirectory });

            return LoadFromAssemblyPath(dll);
        }

        public virtual Type GetType(string name) => RootAssembly.GetType(name);
        public virtual Type GetType(string name, bool throwOnError) => RootAssembly.GetType(name, throwOnError);
        public virtual Type GetType(string name, bool throwOnError, bool ignoreCase) => RootAssembly.GetType(name, throwOnError, ignoreCase);
        public virtual Type[] GetTypes() => RootAssembly.GetTypes();
    }
}
