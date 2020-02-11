#if A
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NStandard
{
    public static class GAC
    {
        private static readonly string ProgramFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        private static readonly string UserProfileFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        private static Version[] GetAssemblyVersions(string assemblyFolder)
        {
            return Directory.GetDirectories(assemblyFolder).Select(x => new Version(x)).ToArray();
        }
        private static Version GetCandidateVersion(Version[] verions, Version targetVersion)
        {
            var candidates = verions.Where(x => x >= targetVersion);
            return candidates.Any() ? candidates.Min() : null;
        }

        private static string GetCandidateFramework(string[] frameworks, string targetFramework)
        {
            switch (targetFramework)
            {
                case string framework when "netcopeapp"
            }
        }

        public static string GetFiles(string assembly, Version version, string targetFramework, GACFolders folders)
        {
            var candidates = new Dictionary<(GACFolders Folder, Version Version), string>();

            if (folders.HasFlag(GACFolders.NuGet) || folders.HasFlag(GACFolders.SDK))
            {
                var nugetVersion = version.MinorRevision == 0
                    ? $"{version.Major}.{version.Minor}.{version.Build}"
                    : $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";

                if (folders.HasFlag(GACFolders.NuGet))
                {
                    var assemblyFolder = $"{UserProfileFolder}/.nuget/packages/{assembly}";
                    var candidateVersion = GetCandidateVersion(assemblyFolder);

                    var file = $"{UserProfileFolder}/.nuget/packages/{assembly}/{candidateVersion}/lib/netstandard2.0/{assembly}.dll";
                    if (File.Exists(file)) return file;
                }
                if (folders.HasFlag(GACFolders.SDK))
                {
                    var file = $"{ProgramFilesFolder}/dotnet/sdk/NuGetFallbackFolder/{assembly}/{nugetVersion}/lib/netstandard2.0/{assembly}.dll";
                    if (File.Exists(file)) return file;
                }
            }



        }

        public Assembly AssemblyResolver(object sender, ResolveEventArgs args)
        {

        }
    }
}
#endif
