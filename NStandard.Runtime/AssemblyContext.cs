using NStandard.Reference;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace NStandard.Runtime
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

        public Assembly LoadAssembly(AssemblyName assemblyName) => Load(assemblyName);

        public virtual Type GetType(string name)
        {
            if (name.Count(",") == 1)
            {
                var parts = name.Split(',');
                var typeName = parts[0];
                var pureAssemblyName = parts[1];

                if (pureAssemblyName == RootAssembly.GetName().Name)
                    return RootAssembly.GetType(typeName);
                else
                {
                    var assemblyName = ReferencedAssemblies.FirstOrDefault(x => x.Name == pureAssemblyName);
                    if (assemblyName != null)
                    {
                        var assembly = Load(assemblyName);
                        return assembly.GetType(typeName);
                    }
                    else return null;
                }
            }
            else return RootAssembly.GetType(name);
        }

        public virtual Type[] GetTypes() => RootAssembly.GetTypes();
    }
}
