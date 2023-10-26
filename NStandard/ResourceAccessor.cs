using System.IO;
using System.Linq;
using System.Reflection;

namespace NStandard;

public class ResourceAccessor
{
    public Assembly Assembly { get; }
    public string Namespace { get; }
    public string[] ResourceNames { get; }

    public ResourceAccessor(Assembly assembly)
    {
        Assembly = assembly;
        var names = assembly.GetManifestResourceNames();
        ResourceNames = names;

        if (names.Any())
        {
            var name = names[0];
            var index = name.IndexOf('.');
            Namespace = name.Substring(0, index);
        }
        else Namespace = null;
    }

    public Stream OpenStream(string name)
    {
        if (Namespace is not null)
            return Assembly.GetManifestResourceStream($"{Namespace}.{name}");
        else return null;
    }

}
