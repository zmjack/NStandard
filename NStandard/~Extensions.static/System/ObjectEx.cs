using System.Linq;

namespace NStandard;

public static class ObjectEx
{
    public static void AcceptPropValues(object source, object provider) => AcceptPropValues(source, provider, null);
    public static void AcceptPropValues(object source, object provider, string[] names)
    {
        var sourceProps = source.GetType().GetProperties();
        var providerProps = provider.GetType().GetProperties();

        if (names is null)
            names = sourceProps.Select(x => x.Name).Intersect(providerProps.Select(x => x.Name)).ToArray();

        foreach (var name in names)
        {
            var sourceProp = sourceProps.First(x => x.Name == name);
            var providerProp = providerProps.First(x => x.Name == name);

            if (sourceProp.PropertyType == providerProp.PropertyType)
                sourceProp.SetValue(source, providerProp.GetValue(provider));
        }
    }

    /// <summary>
    /// Determines whether the specified objects are equal to each other.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objs"></param>
    /// <returns></returns>
    public static bool CrossEquals<T>(params T[] objs)
    {
        if (objs.Length < 2) return true;

        foreach (var window in objs.Slide(2, true))
        {
            if (!window[0].Equals(window[1])) return false;
        }
        return true;
    }

}
