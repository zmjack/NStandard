using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NStandard.Runtime
{
    public class AssemblyContext : AssemblyLoadContext
    {
        private static readonly string ProgramFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
#if NET35
        private static readonly string UserProfileFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..");
#else
        private static readonly string UserProfileFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
#endif
        public Assembly MainAssembly { get; private set; }

        public List<string> Directories = new();
        public List<string> LoadedSdks = new();
        public List<Assembly> LoadedAssemblies = new();
        public List<AssemblyName> LoadedAssemblyNames = new();

        public readonly DotNetFramework Framework;
        public readonly DotNetFramework[] CompatibilityFrameworks;

        public AssemblyContext(DotNetFramework framework, string sdkType)
        {
            Framework = framework;
            CompatibilityFrameworks = framework.CompatibilityFrameworks.OrderByDescending(x => x.Order).ToArray();
            LoadSdk(sdkType);
        }

        public void LoadMain(string assemblyFile)
        {
            if (MainAssembly is not null) throw new InvalidOperationException("Main assembly has been loaded.");

            var assembly = LoadFromAssemblyPath(assemblyFile);
            LoadedAssemblies.Add(assembly);
            LoadedAssemblyNames.Add(assembly.GetName());
            Directories.Add(Path.GetDirectoryName(assemblyFile));
            MainAssembly = assembly;

            foreach (var refAsseblyName in MainAssembly.GetReferencedAssemblies())
            {
                Load(refAsseblyName);
            }
        }

        private void LoadSdk(string sdkType)
        {
            if (LoadedSdks.Contains(sdkType)) return;
            if (!new[] { SdkType.Legency, SdkType.Core, SdkType.Web }.Contains(sdkType)) throw new ArgumentException($"Unkown sdk type. ({sdkType})", nameof(sdkType));
            if (sdkType == SdkType.Legency) throw new NotSupportedException(".NET Framework is not supported.");

            if (sdkType == SdkType.Core || sdkType == SdkType.Web)
            {
                LoadedSdks.Add(SdkType.Core);

                var packageDir = $"{ProgramFilesFolder}/dotnet/shared/Microsoft.NETCore.App";
                var verPairs = (from nuVersion in Directory.GetDirectories(packageDir).Select(x => Path.GetFileName(x))
                                let ver = GetVersionFromNuVersion(nuVersion)
                                where ver >= Framework.Version
                                orderby ver
                                select new { NuVersion = nuVersion, Version = ver }).ToArray();
                Directories.Add($"{packageDir}/{verPairs[0].NuVersion}");
            }

            if (sdkType == SdkType.Web)
            {
                LoadedSdks.Add(SdkType.Web);

                {
                    var packageDir = $"{ProgramFilesFolder}/dotnet/shared/Microsoft.AspNetCore.App";
                    var verPairs = (from nuVersion in Directory.GetDirectories(packageDir).Select(x => Path.GetFileName(x))
                                    let ver = GetVersionFromNuVersion(nuVersion)
                                    where ver >= Framework.Version
                                    orderby ver
                                    select new { NuVersion = nuVersion, Version = ver }).ToArray();
                    if (verPairs.Length > 0) Directories.Add($"{packageDir}/{verPairs[0].NuVersion}");
                }
                {
                    var packageDir = $"{ProgramFilesFolder}/dotnet/shared/Microsoft.AspNetCore.All";
                    var verPairs = (from nuVersion in Directory.GetDirectories(packageDir).Select(x => Path.GetFileName(x))
                                    let ver = GetVersionFromNuVersion(nuVersion)
                                    where ver >= Framework.Version
                                    orderby ver
                                    select new { NuVersion = nuVersion, Version = ver }).ToArray();
                    if (verPairs.Length > 0) Directories.Add($"{packageDir}/{verPairs[0].NuVersion}");
                }
            }
        }

        public string GetNuVersion(Version version)
        {
            return version.MinorRevision <= 0
                ? $"{version.Major}.{version.Minor}.{version.Build}"
                : $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private readonly Regex NuVersionRegex = new(@"(\d+)\.(\d+)\.(\d+)(?:\.(\d+))?(?:-\w+)?", RegexOptions.Singleline);
        public Version GetVersionFromNuVersion(string nuVersion)
        {
            var match = NuVersionRegex.Match(nuVersion);
            if (!match.Success) return default;

            var groups = match.Groups;
            var major = int.Parse(groups[1].Value);
            var minor = int.Parse(groups[2].Value);
            var build = int.Parse(groups[3].Value);
            var revision = groups[4];
            return new Version(major, minor, build, revision.Success ? int.Parse(revision.Value) : 0);
        }

        public string GetFileFromNugetCache(AssemblyName asmName)
        {
            var packageDir = $"{UserProfileFolder}/.nuget/packages/{asmName.Name.ToLower()}";
            if (!Directory.Exists(packageDir)) return null;

            var verPairs = (from nuVersion in Directory.GetDirectories(packageDir)
                            let ver = GetVersionFromNuVersion(nuVersion)
                            where ver >= asmName.Version
                            orderby ver
                            select new { NuVersion = nuVersion, Version = ver }).ToArray();
            if (verPairs.Length == 0) return null;
            foreach (var pair in verPairs)
            {
                var libDir = $"{packageDir}/{pair.NuVersion}/lib";
                if (!Directory.Exists(libDir)) continue;

                var tfms = Directory.GetDirectories(libDir);
                var selectTfm = CompatibilityFrameworks.FirstOrDefault(framework => tfms.Contains(framework.TFM));
                if (selectTfm != null)
                {
                    var file = $"{libDir}/{selectTfm.TFM}/{asmName.Name}.dll";
                    if (File.Exists(file)) return file;
                }
            }
            return null;
        }

        public string GetFileFromSdkNuGetFallbackFolder(AssemblyName asmName)
        {
            var packageDir = $"{ProgramFilesFolder}/dotnet/sdk/NuGetFallbackFolder/{asmName.Name.ToLower()}";
            if (!Directory.Exists(packageDir)) return null;

            var verPairs = (from nuVersion in Directory.GetDirectories(packageDir)
                            let ver = GetVersionFromNuVersion(nuVersion)
                            where ver >= asmName.Version
                            orderby ver
                            select new { NuVersion = nuVersion, Version = ver }).ToArray();
            if (verPairs.Length == 0) return null;
            foreach (var pair in verPairs)
            {
                var libDir = $"{packageDir}/{pair.NuVersion}/lib";
                if (!Directory.Exists(libDir)) continue;

                var tfms = Directory.GetDirectories(libDir);
                var selectTfm = CompatibilityFrameworks.FirstOrDefault(framework => tfms.Contains(framework.TFM));
                if (selectTfm != null)
                {
                    var file = $"{libDir}/{selectTfm.TFM}/{asmName.Name}.dll";
                    if (File.Exists(file)) return file;
                }
            }
            return null;
        }

        public virtual Type GetType(string name)
        {
            if (name.Count(",") == 1)
            {
                var parts = name.Split(',');
                var typeName = parts[0];
                var assemblyName = parts[1];
                return LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assemblyName)?.GetType(typeName);
            }
            else return MainAssembly.GetType(name);
        }

        public virtual Type[] GetTypes() => MainAssembly.GetTypes();

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var found = LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assemblyName.Name);
            if (found is not null) return found;

            foreach (var directory in Directories)
            {
                var dll = Directory.EnumerateFiles(directory, $"{assemblyName.Name}.dll").FirstOrDefault()
                    ?? GetFileFromSdkNuGetFallbackFolder(assemblyName)
                    ?? GetFileFromNugetCache(assemblyName);

                if (dll is not null)
                {
                    var assembly = LoadFromAssemblyPath(dll);
                    LoadedAssemblies.Add(assembly);
                    LoadedAssemblyNames.Add(assembly.GetName());
                    return assembly;
                }
            }
            throw new FileNotFoundException($"Can not find {assemblyName}.dll");
        }
    }
}
