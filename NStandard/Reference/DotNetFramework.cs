using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NStandard.Reference
{
    //TODO: Long-term Maintenance
    //TODO: To support more frameworks
    //Refer: https://docs.microsoft.com/en-us/nuget/reference/target-frameworks
    public class DotNetFramework
    {
        public string Name;
        public string Abbreviation;
        /// <summary>
        /// Target Framework Moniker
        /// </summary>
        public string TFM;
        public Version Version;
        public bool Supported;
        public DotNetFramework[] CompatibilityFrameworks;

        internal DotNetFramework(string name, string abbreviation, string tfm, Version version, bool supported)
        {
            Name = name;
            Abbreviation = abbreviation;
            TFM = tfm;
            Version = version;
            Supported = supported;
        }

        public override string ToString() => TFM;

        public static DotNetFramework NetStandard1_0 = new DotNetFramework(".NET Standard", "netstandard", "netstandard1.0", new Version(1, 0, 0), true);
        public static DotNetFramework NetStandard1_1 = new DotNetFramework(".NET Standard", "netstandard", "netstandard1.1", new Version(1, 1, 0), true);
        public static DotNetFramework NetStandard1_2 = new DotNetFramework(".NET Standard", "netstandard", "netstandard1.2", new Version(1, 2, 0), true);
        public static DotNetFramework NetStandard1_3 = new DotNetFramework(".NET Standard", "netstandard", "netstandard1.3", new Version(1, 3, 0), true);
        public static DotNetFramework NetStandard1_4 = new DotNetFramework(".NET Standard", "netstandard", "netstandard1.4", new Version(1, 4, 0), true);
        public static DotNetFramework NetStandard1_5 = new DotNetFramework(".NET Standard", "netstandard", "netstandard1.5", new Version(1, 5, 0), true);
        public static DotNetFramework NetStandard1_6 = new DotNetFramework(".NET Standard", "netstandard", "netstandard1.6", new Version(1, 6, 0), true);
        public static DotNetFramework NetStandard2_0 = new DotNetFramework(".NET Standard", "netstandard", "netstandard2.0", new Version(2, 0, 0), true);

        public static DotNetFramework NetCoreApp1_0 = new DotNetFramework(".NET Core App", "netcoreapp", "netcoreapp1.0", new Version(1, 0, 0), true);
        public static DotNetFramework NetCoreApp1_1 = new DotNetFramework(".NET Core App", "netcoreapp", "netcoreapp1.1", new Version(1, 1, 0), true);
        public static DotNetFramework NetCoreApp2_0 = new DotNetFramework(".NET Core App", "netcoreapp", "netcoreapp2.0", new Version(2, 0, 0), true);
        public static DotNetFramework NetCoreApp2_1 = new DotNetFramework(".NET Core App", "netcoreapp", "netcoreapp2.1", new Version(2, 1, 0), true);
        public static DotNetFramework NetCoreApp2_2 = new DotNetFramework(".NET Core App", "netcoreapp", "netcoreapp2.2", new Version(2, 2, 0), true);
        public static DotNetFramework NetCoreApp3_0 = new DotNetFramework(".NET Core App", "netcoreapp", "netcoreapp3.0", new Version(3, 0, 0), true);
        public static DotNetFramework NetCoreApp3_1 = new DotNetFramework(".NET Core App", "netcoreapp", "netcoreapp3.1", new Version(3, 1, 0), true);

        public static DotNetFramework Net11 = new DotNetFramework(".NET Framework", "net", "net11", new Version(1, 1, 0), true);
        public static DotNetFramework Net20 = new DotNetFramework(".NET Framework", "net", "net20", new Version(2, 0, 0), true);
        public static DotNetFramework Net35 = new DotNetFramework(".NET Framework", "net", "net35", new Version(3, 5, 0), true);
        public static DotNetFramework Net40 = new DotNetFramework(".NET Framework", "net", "net40", new Version(4, 0, 0), true);
        public static DotNetFramework Net403 = new DotNetFramework(".NET Framework", "net", "net403", new Version(4, 0, 3), true);
        public static DotNetFramework Net45 = new DotNetFramework(".NET Framework", "net", "net45", new Version(4, 5, 0), true);
        public static DotNetFramework Net451 = new DotNetFramework(".NET Framework", "net", "net451", new Version(4, 5, 1), true);
        public static DotNetFramework Net452 = new DotNetFramework(".NET Framework", "net", "net452", new Version(4, 5, 2), true);
        public static DotNetFramework Net46 = new DotNetFramework(".NET Framework", "net", "net46", new Version(4, 6, 0), true);
        public static DotNetFramework Net461 = new DotNetFramework(".NET Framework", "net", "net461", new Version(4, 6, 1), true);
        public static DotNetFramework Net462 = new DotNetFramework(".NET Framework", "net", "net462", new Version(4, 6, 2), true);
        public static DotNetFramework Net47 = new DotNetFramework(".NET Framework", "net", "net47", new Version(4, 7, 0), true);
        public static DotNetFramework Net471 = new DotNetFramework(".NET Framework", "net", "net471", new Version(4, 7, 1), true);
        public static DotNetFramework Net472 = new DotNetFramework(".NET Framework", "net", "net472", new Version(4, 7, 2), true);
        public static DotNetFramework Net48 = new DotNetFramework(".NET Framework", "net", "net48", new Version(4, 8, 0), true);

        static DotNetFramework()
        {
            DotNetFramework[] CombineFrameworks(DotNetFramework[] @new, DotNetFramework compatibility)
            {
                return @new.Concat(compatibility.CompatibilityFrameworks).ToArray();
            }

            NetStandard1_0.CompatibilityFrameworks = new[] { NetStandard1_0 };
            NetStandard1_1.CompatibilityFrameworks = CombineFrameworks(new[] { NetStandard1_1 }, NetStandard1_0);
            NetStandard1_2.CompatibilityFrameworks = CombineFrameworks(new[] { NetStandard1_2 }, NetStandard1_1);
            NetStandard1_3.CompatibilityFrameworks = CombineFrameworks(new[] { NetStandard1_3 }, NetStandard1_2);
            NetStandard1_4.CompatibilityFrameworks = CombineFrameworks(new[] { NetStandard1_4 }, NetStandard1_3);
            NetStandard1_5.CompatibilityFrameworks = CombineFrameworks(new[] { NetStandard1_5 }, NetStandard1_4);
            NetStandard1_6.CompatibilityFrameworks = CombineFrameworks(new[] { NetStandard1_6 }, NetStandard1_5);
            NetStandard2_0.CompatibilityFrameworks = CombineFrameworks(new[] { NetStandard2_0 }, NetStandard1_6);

            NetCoreApp1_0.CompatibilityFrameworks = new[] { NetCoreApp1_0 };
            NetCoreApp1_1.CompatibilityFrameworks = CombineFrameworks(new[] { NetCoreApp1_1 }, NetCoreApp1_0);
            NetCoreApp2_0.CompatibilityFrameworks = CombineFrameworks(new[] { NetCoreApp2_0 }, NetCoreApp1_1);
            NetCoreApp2_1.CompatibilityFrameworks = CombineFrameworks(new[] { NetCoreApp2_1 }, NetCoreApp2_0);
            NetCoreApp2_2.CompatibilityFrameworks = CombineFrameworks(new[] { NetCoreApp2_2 }, NetCoreApp2_1);
            NetCoreApp3_0.CompatibilityFrameworks = CombineFrameworks(new[] { NetCoreApp3_0 }, NetCoreApp2_2);
            NetCoreApp3_1.CompatibilityFrameworks = CombineFrameworks(new[] { NetCoreApp3_1 }, NetCoreApp3_0);

            Net11.CompatibilityFrameworks = new[] { Net11 };
            Net20.CompatibilityFrameworks = CombineFrameworks(new[] { Net20 }, Net11);
            Net35.CompatibilityFrameworks = CombineFrameworks(new[] { Net35 }, Net20);
            Net40.CompatibilityFrameworks = CombineFrameworks(new[] { Net40 }, Net35);
            Net403.CompatibilityFrameworks = CombineFrameworks(new[] { Net403 }, Net40);
            Net45.CompatibilityFrameworks = CombineFrameworks(new[] { Net45, NetStandard1_1, NetStandard1_0 }, Net403);
            Net451.CompatibilityFrameworks = CombineFrameworks(new[] { Net451, NetStandard1_2 }, Net45);
            Net452.CompatibilityFrameworks = CombineFrameworks(new[] { Net452 }, Net451);
            Net46.CompatibilityFrameworks = CombineFrameworks(new[] { Net46, NetStandard1_3 }, Net452);
            Net461.CompatibilityFrameworks = CombineFrameworks(new[] { Net461, NetStandard2_0, NetStandard1_6, NetStandard1_5, NetStandard1_4, Net46 }, Net46);
            Net462.CompatibilityFrameworks = CombineFrameworks(new[] { Net462 }, Net461);
            Net47.CompatibilityFrameworks = CombineFrameworks(new[] { Net47 }, Net462);
            Net471.CompatibilityFrameworks = CombineFrameworks(new[] { Net471 }, Net47);
            Net472.CompatibilityFrameworks = CombineFrameworks(new[] { Net472 }, Net471);
            Net48.CompatibilityFrameworks = CombineFrameworks(new[] { Net48 }, Net472);
        }

        public static readonly DotNetFramework[] SupportedFrameworks = new[]
        {
            NetStandard1_0, NetStandard1_1, NetStandard1_2, NetStandard1_3, NetStandard1_4, NetStandard1_5, NetStandard1_6,
            NetStandard2_0,
            NetCoreApp1_0, NetCoreApp1_1,
            NetCoreApp2_0, NetCoreApp2_1, NetCoreApp2_2,
            NetCoreApp3_0, NetCoreApp3_1,
            Net11, Net20, Net35,
            Net40, Net403, Net45, Net451, Net452, Net46, Net461, Net462, Net47, Net471, Net472, Net48,
        };

        public static DotNetFramework Parse(string tfm)
        {
            var framework = SupportedFrameworks.FirstOrDefault(x => x.TFM == tfm);
            if (framework is null)
                throw new NotSupportedException($"The TFM ({tfm}) dose not exist or temporarily does not support.");
            else return framework;
        }

    }
}
