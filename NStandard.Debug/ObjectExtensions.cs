using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;

namespace NStandard.Debug
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ObjectExtensions
    {
        public static string GetDumpString<TSelf>(this TSelf @this)
        {
            using (var memory = new MemoryStream())
            using (var writer = new StreamWriter(memory))
            {
                Dump(@this, writer);
                writer.Flush();
                memory.Seek(0, SeekOrigin.Begin);
                return memory.ToArray().String();
            }
        }
        public static void Dump<TSelf>(this TSelf @this) => Dump(@this, Console.Out);
        public static void Dump<TSelf>(this TSelf @this, TextWriter writer)
        {
            var type = @this?.GetType();
            switch (type)
            {
                case null:
                case Type _ when type.IsBasicType():
                    Dump(@this, writer, null, 0);
                    break;

                default:
                    writer.WriteLine($"<{typeof(TSelf).GetSimplifiedName()}>");
                    Dump(@this, writer, null, 0);
                    break;
            }
        }

        private static void Dump(object instance, TextWriter writer, string name, int paddingLeft)
        {
            var type = instance?.GetType();
            switch (type)
            {
                case null:
                    if (name is null)
                        writer.WriteLine($"{" ".Repeat(paddingLeft)}<null>");
                    else if (name == string.Empty)
                        writer.WriteLine($"{" ".Repeat(paddingLeft)}<null>,");
                    else writer.WriteLine($"{" ".Repeat(paddingLeft)}{name}: <null>,");
                    break;

                case Type _ when type.IsBasicType():
                    if (name is null)
                        writer.WriteLine($"{" ".Repeat(paddingLeft)}<{type.GetSimplifiedName()}>{instance}");
                    else if (name == string.Empty)
                        writer.WriteLine($"{" ".Repeat(paddingLeft)}<{type.GetSimplifiedName()}>{instance},");
                    else writer.WriteLine($"{" ".Repeat(paddingLeft)}{name}: <{type.GetSimplifiedName()}>{instance},");
                    break;

                case Type _ when type.IsExtend<Array>():
                    writer.WriteLine($"{" ".Repeat(paddingLeft)}[");
                    var enumerator = (instance as IEnumerable).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Dump(enumerator.Current, writer, string.Empty, paddingLeft + 4);
                    }
                    writer.WriteLine($"{" ".Repeat(paddingLeft)}]");
                    break;

                default:
                    writer.WriteLine($"{" ".Repeat(paddingLeft)}{{");
                    var props = type.GetProperties();
                    foreach (var prop in props)
                    {
                        Dump(prop.GetValue(instance, null), writer, prop.Name, paddingLeft + 4);
                    }
                    writer.WriteLine($"{" ".Repeat(paddingLeft)}}},");
                    break;
            }
        }
    }
}
