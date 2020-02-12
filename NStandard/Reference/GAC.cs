using System;
using System.IO;
using System.Linq;

namespace NStandard.Reference
{
    public static class GAC
    {
        private static readonly string ProgramFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
#if NET35
        private static readonly string UserProfileFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..");
#else
        private static readonly string UserProfileFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
#endif

        public static string GetAssemblyFile(string assembly, Version version, string targetFramework, GACFolders folders, string[] customSearchDirs = null)
        {
            var framework = DotNetFramework.Parse(targetFramework);
            var nugetVersion = version.MinorRevision == 0
                ? $"{version.Major}.{version.Minor}.{version.Build}"
                : $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";

            if (customSearchDirs != null)
            {
                foreach (var libDir in customSearchDirs)
                {
                    var file = $"{libDir}/{assembly}.dll";
                    if (File.Exists(file)) return file;
                }
            }

            if (folders.HasFlag(GACFolders.NuGet))
            {
                var libDir = $"{UserProfileFolder}/.nuget/packages/{assembly}/{nugetVersion}/lib";
                if (Directory.Exists(libDir))
                {
                    var libTFMs = Directory.GetDirectories(libDir).Select(x => Path.GetFileName(x));
                    var libTFM = framework.CompatibilityFrameworks.FirstOrDefault(x => libTFMs.Contains(x.TFM));
                    if (libTFM != null)
                    {
                        var file = $"{libDir}/{libTFM.TFM}/{assembly}.dll";
                        if (File.Exists(file)) return file;
                    }
                }
            }
            if (folders.HasFlag(GACFolders.SDK))
            {
                var libDir = $"{ProgramFilesFolder}/dotnet/sdk/NuGetFallbackFolder/{assembly}/{nugetVersion}/lib";
                if (Directory.Exists(libDir))
                {
                    var libTFMs = Directory.GetDirectories(libDir).Select(x => Path.GetFileName(x));
                    var libTFM = framework.CompatibilityFrameworks.FirstOrDefault(x => libTFMs.Contains(x.TFM));
                    if (libTFM != null)
                    {
                        var file = $"{libDir}/{libTFM.TFM}/{assembly}.dll";
                        if (File.Exists(file)) return file;
                    }
                }
            }
            if (folders.HasFlag(GACFolders.Shared))
            {
                var searchDirs = Directory.GetDirectories($"{ProgramFilesFolder}/dotnet/shared");
                switch (framework.Abbreviation)
                {
                    case "netcoreapp":
                        foreach (var libDir in searchDirs)
                        {
                            var _version = Directory.GetDirectories(libDir)
                                .Select(x => new Version(Path.GetFileName(x)))
                                .Where(v => v >= framework.Version)
                                .Min();

                            if (_version != null)
                            {
                                var file = $"{libDir}/{_version}/{assembly}.dll";
                                if (File.Exists(file)) return file;
                            }
                        }
                        break;

                    default:
                        throw new NotSupportedException("Only netcoreapp is supported.");
                }
            }
            return null;
        }

    }
}
