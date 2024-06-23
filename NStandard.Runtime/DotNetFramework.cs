using System;
using System.Linq;

namespace NStandard.Runtime;

//TODO: Long-term Maintenance
//TODO: To support more frameworks
/// <summary>
/// <see href="https://docs.microsoft.com/en-us/nuget/reference/target-frameworks" />
/// </summary>
public class DotNetFramework
{
    public string Name { get; }
    public string Abbreviation { get; }
    /// <summary>
    /// Target Framework Moniker
    /// </summary>
    public string TFM { get; }
    public Version Version { get; }
    public bool Supported { get; }
    public DotNetFramework[] Compatibility { get; private set; }
    public int Order { get; }

    internal DotNetFramework(string name, string abbreviation, string tfm, Version version, bool supported, int order)
    {
        Name = name;
        Abbreviation = abbreviation;
        TFM = tfm;
        Version = version;
        Supported = supported;
        Order = order;
    }

    public override string ToString() => TFM;

    public static DotNetFramework Net11 { get; } = new(".NET Framework", "net", "net11", new Version(1, 1, 0), true, 101);
    public static DotNetFramework Net20 { get; } = new(".NET Framework", "net", "net20", new Version(2, 0, 0), true, 102);
    public static DotNetFramework Net35 { get; } = new(".NET Framework", "net", "net35", new Version(3, 5, 0), true, 103);
    public static DotNetFramework Net40 { get; } = new(".NET Framework", "net", "net40", new Version(4, 0, 0), true, 104);
    public static DotNetFramework Net403 { get; } = new(".NET Framework", "net", "net403", new Version(4, 0, 3), true, 105);
    public static DotNetFramework Net45 { get; } = new(".NET Framework", "net", "net45", new Version(4, 5, 0), true, 106);
    public static DotNetFramework Net451 { get; } = new(".NET Framework", "net", "net451", new Version(4, 5, 1), true, 107);
    public static DotNetFramework Net452 { get; } = new(".NET Framework", "net", "net452", new Version(4, 5, 2), true, 108);
    public static DotNetFramework Net46 { get; } = new(".NET Framework", "net", "net46", new Version(4, 6, 0), true, 109);
    public static DotNetFramework Net461 { get; } = new(".NET Framework", "net", "net461", new Version(4, 6, 1), true, 110);
    public static DotNetFramework Net462 { get; } = new(".NET Framework", "net", "net462", new Version(4, 6, 2), true, 111);
    public static DotNetFramework Net47 { get; } = new(".NET Framework", "net", "net47", new Version(4, 7, 0), true, 112);
    public static DotNetFramework Net471 { get; } = new(".NET Framework", "net", "net471", new Version(4, 7, 1), true, 113);
    public static DotNetFramework Net472 { get; } = new(".NET Framework", "net", "net472", new Version(4, 7, 2), true, 114);
    public static DotNetFramework Net48 { get; } = new(".NET Framework", "net", "net48", new Version(4, 8, 0), true, 115);

    public static DotNetFramework NetStandard10 { get; } = new(".NET Standard", "netstandard", "netstandard1.0", new Version(1, 0, 0), true, 200);
    public static DotNetFramework NetStandard11 { get; } = new(".NET Standard", "netstandard", "netstandard1.1", new Version(1, 1, 0), true, 201);
    public static DotNetFramework NetStandard12 { get; } = new(".NET Standard", "netstandard", "netstandard1.2", new Version(1, 2, 0), true, 202);
    public static DotNetFramework NetStandard13 { get; } = new(".NET Standard", "netstandard", "netstandard1.3", new Version(1, 3, 0), true, 203);
    public static DotNetFramework NetStandard14 { get; } = new(".NET Standard", "netstandard", "netstandard1.4", new Version(1, 4, 0), true, 204);
    public static DotNetFramework NetStandard15 { get; } = new(".NET Standard", "netstandard", "netstandard1.5", new Version(1, 5, 0), true, 205);
    public static DotNetFramework NetStandard16 { get; } = new(".NET Standard", "netstandard", "netstandard1.6", new Version(1, 6, 0), true, 206);
    public static DotNetFramework NetStandard20 { get; } = new(".NET Standard", "netstandard", "netstandard2.0", new Version(2, 0, 0), true, 207);
    public static DotNetFramework NetStandard21 { get; } = new(".NET Standard", "netstandard", "netstandard2.1", new Version(2, 1, 0), true, 208);

    public static DotNetFramework NetCoreApp10 { get; } = new(".NET 5+ (and .NET Core)", "netcoreapp", "netcoreapp1.0", new Version(1, 0, 0), true, 301);
    public static DotNetFramework NetCoreApp11 { get; } = new(".NET 5+ (and .NET Core)", "netcoreapp", "netcoreapp1.1", new Version(1, 1, 0), true, 302);
    public static DotNetFramework NetCoreApp20 { get; } = new(".NET 5+ (and .NET Core)", "netcoreapp", "netcoreapp2.0", new Version(2, 0, 0), true, 303);
    public static DotNetFramework NetCoreApp21 { get; } = new(".NET 5+ (and .NET Core)", "netcoreapp", "netcoreapp2.1", new Version(2, 1, 0), true, 304);
    public static DotNetFramework NetCoreApp22 { get; } = new(".NET 5+ (and .NET Core)", "netcoreapp", "netcoreapp2.2", new Version(2, 2, 0), true, 305);
    public static DotNetFramework NetCoreApp30 { get; } = new(".NET 5+ (and .NET Core)", "netcoreapp", "netcoreapp3.0", new Version(3, 0, 0), true, 306);
    public static DotNetFramework NetCoreApp31 { get; } = new(".NET 5+ (and .NET Core)", "netcoreapp", "netcoreapp3.1", new Version(3, 1, 0), true, 307);

    // .NET 5.0 and later has NetCoreApp identifier
    public static DotNetFramework Net50 { get; } = new(".NET 5+ (and .NET Core)", "net", "net5.0", new Version(5, 0, 0), true, 401);
    public static DotNetFramework Net60 { get; } = new(".NET 5+ (and .NET Core)", "net", "net6.0", new Version(6, 0, 0), true, 402);
    public static DotNetFramework Net70 { get; } = new(".NET 5+ (and .NET Core)", "net", "net7.0", new Version(7, 0, 0), true, 403);
    public static DotNetFramework Net80 { get; } = new(".NET 5+ (and .NET Core)", "net", "net8.0", new Version(8, 0, 0), true, 404);

    static DotNetFramework()
    {
        static DotNetFramework[] Combine(DotNetFramework[] @new, DotNetFramework compatibility)
        {
            return [.. @new, .. compatibility.Compatibility];
        }

        NetStandard10.Compatibility = [NetStandard10];
        NetStandard11.Compatibility = Combine([NetStandard11], NetStandard10);
        NetStandard12.Compatibility = Combine([NetStandard12], NetStandard11);
        NetStandard13.Compatibility = Combine([NetStandard13], NetStandard12);
        NetStandard14.Compatibility = Combine([NetStandard14], NetStandard13);
        NetStandard15.Compatibility = Combine([NetStandard15], NetStandard14);
        NetStandard16.Compatibility = Combine([NetStandard16], NetStandard15);
        NetStandard20.Compatibility = Combine([NetStandard20], NetStandard16);
        NetStandard21.Compatibility = Combine([NetStandard21], NetStandard20);

        Net11.Compatibility = [Net11];
        Net20.Compatibility = Combine([Net20], Net11);
        Net35.Compatibility = Combine([Net35], Net20);
        Net40.Compatibility = Combine([Net40], Net35);
        Net403.Compatibility = Combine([Net403], Net40);
        Net45.Compatibility = Combine([Net45, NetStandard11, NetStandard10], Net403);
        Net451.Compatibility = Combine([Net451, NetStandard12], Net45);
        Net452.Compatibility = Combine([Net452], Net451);
        Net46.Compatibility = Combine([Net46, NetStandard13], Net452);
        Net461.Compatibility = Combine([Net461, NetStandard20, NetStandard16, NetStandard15, NetStandard14, Net46], Net46);
        Net462.Compatibility = Combine([Net462], Net461);
        Net47.Compatibility = Combine([Net47], Net462);
        Net471.Compatibility = Combine([Net471], Net47);
        Net472.Compatibility = Combine([Net472], Net471);
        Net48.Compatibility = Combine([Net48], Net472);

        NetCoreApp10.Compatibility = [NetCoreApp10, NetStandard10];
        NetCoreApp11.Compatibility = Combine([NetCoreApp11], NetCoreApp10);
        NetCoreApp20.Compatibility = Combine([NetCoreApp20, NetStandard20], NetCoreApp11);
        NetCoreApp21.Compatibility = Combine([NetCoreApp21], NetCoreApp20);
        NetCoreApp22.Compatibility = Combine([NetCoreApp22], NetCoreApp21);
        NetCoreApp30.Compatibility = Combine([NetCoreApp30, NetStandard21], NetCoreApp22);
        NetCoreApp31.Compatibility = Combine([NetCoreApp31], NetCoreApp30);

        Net50.Compatibility = Combine([Net50], NetCoreApp31);
        Net60.Compatibility = Combine([Net60], Net50);
        Net70.Compatibility = Combine([Net70], Net60);
        Net80.Compatibility = Combine([Net80], Net70);
    }

    public static readonly DotNetFramework[] SupportedFrameworks =
    [
        NetStandard10,
        NetStandard11,
        NetStandard12,
        NetStandard13,
        NetStandard14,
        NetStandard15,
        NetStandard16,
        NetStandard20,
        NetStandard21,
        Net11,
        Net20,
        Net35,
        Net40,
        Net403,
        Net45,
        Net451,
        Net452,
        Net46,
        Net461,
        Net462,
        Net47,
        Net471,
        Net472,
        Net48,
        NetCoreApp10,
        NetCoreApp11,
        NetCoreApp20,
        NetCoreApp21,
        NetCoreApp22,
        NetCoreApp30,
        NetCoreApp31,
        Net50,
        Net60,
        Net70,
        Net80,
    ];

    public static DotNetFramework Parse(string tfm)
    {
        var framework = SupportedFrameworks.FirstOrDefault(x => x.TFM == tfm);
        if (framework is null)
            throw new NotSupportedException($"The TFM ({tfm}) dose not exist or temporarily does not support.");
        else return framework;
    }

}
