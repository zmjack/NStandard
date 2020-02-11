using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace NStandard.Reference
{
    public static class GAC
    {
        private static readonly string ProgramFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        private static readonly string UserProfileFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public static string GetAssemblyFile(string assembly, Version version, string targetFramework, GACFolders folders)
        {
            var framework = DotNetFramework.Parse(targetFramework);
            var nugetVersion = version.MinorRevision == 0
                ? $"{version.Major}.{version.Minor}.{version.Build}"
                : $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";

            if (folders.HasFlag(GACFolders.NuGet))
            {
                var libFolder = $"{UserProfileFolder}/.nuget/packages/{assembly}/{nugetVersion}/lib";
                if (Directory.Exists(libFolder))
                {
                    var libTFMs = Directory.GetDirectories(libFolder);
                    var libTFM = framework.CompatibilityFrameworks.FirstOrDefault(x => libTFMs.Contains(x.TFM));
                    if (libTFM != null)
                    {
                        var file = $"{libFolder}/{libTFM.TFM}/{assembly}.dll";
                        if (File.Exists(file)) return file;
                    }
                }
            }
            if (folders.HasFlag(GACFolders.SDK))
            {
                var libFolder = $"{ProgramFilesFolder}/dotnet/sdk/NuGetFallbackFolder/{assembly}/{nugetVersion}/lib";
                if (Directory.Exists(libFolder))
                {
                    var libTFMs = Directory.GetDirectories(libFolder);
                    var libTFM = framework.CompatibilityFrameworks.FirstOrDefault(x => libTFMs.Contains(x.TFM));
                    if (libTFM != null)
                    {
                        var file = $"{libFolder}/{libTFM.TFM}/{assembly}.dll";
                        if (File.Exists(file)) return file;
                    }
                }
            }
            if (folders.HasFlag(GACFolders.Shared))
            {
                var searchFolders = Directory.GetDirectories($"{ProgramFilesFolder}/dotnet/shared");
                switch (framework.Abbreviation)
                {
                    case "netcoreapp":
                        foreach (var folder in searchFolders)
                        {
                            var libFolder = $"{ProgramFilesFolder}/dotnet/shared/{folder}";
                            if (Directory.Exists(libFolder))
                            {
                                var _version = Directory.GetDirectories(libFolder)
                                    .Select(v => new Version(v))
                                    .Where(v => v >= framework.Version)
                                    .Min();

                                if (_version != null)
                                {
                                    var file = $"{libFolder}/{_version}/{assembly}.dll";
                                    if (File.Exists(file)) return file;
                                }
                            }
                        }
                        break;

                    default:
                        throw new NotSupportedException("Only netcoreapp is supported.");
                }
            }
            return null;
        }

        public static ResolveEventHandler CreateAssemblyResolver(string targetFramework, GACFolders folders)
        {
            return new ResolveEventHandler((sender, args) =>
            {
                var regex = new Regex("([^,]+), Version=([^,]+), Culture=[^,]+, PublicKeyToken=.+");
                var match = regex.Match(args.Name);

                var assembly = match.Groups[1].Value;
                var version = new Version(match.Groups[2].Value);
                var dll = GetAssemblyFile(assembly, version, targetFramework, folders);

                return Assembly.LoadFrom(dll);
            });
        }

    }
}
