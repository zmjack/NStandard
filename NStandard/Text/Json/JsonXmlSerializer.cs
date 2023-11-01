#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;
using System.Xml.Linq;

namespace NStandard.Text.Json;

/// <summary>
/// Provides methods for converting between XML and JSON.
/// </summary>
public static class JsonXmlSerializer
{
    private static StructTuple<string, string> GetPrefixNameTuple(string name)
    {
        if (name.Contains(':'))
        {
            var parts = name.Split(':');
            return StructTuple.Create(parts[0], parts[1]);
        }
        else return StructTuple.Create((string)null, name);
    }

    private static XmlElement CreateElement(XmlDocument doc, Dictionary<string, string> namespaces, string name)
    {
        var (prefix, pureName) = GetPrefixNameTuple(name);
        if (prefix is null)
        {
            return doc.CreateElement(name);
        }
        else
        {
            var url = namespaces[prefix];
            return doc.CreateElement(prefix, pureName, url);
        }
    }

    private static JsonNode SerializeXmlNodeCore(XmlNode node)
    {
        if (node.FirstChild is not null && node.FirstChild.NodeType == XmlNodeType.Text)
        {
            var text = node.ChildNodes[0].InnerText;
            if (node.Attributes?.Count > 0)
            {
                var textObj = new JsonObject();
                foreach (XmlAttribute attr in node.Attributes)
                {
                    textObj.Add($"@{attr.Name}", attr.Value);
                }
                textObj.Add("#text", text);
                return textObj;
            }
            else return text;
        }

        var obj = new JsonObject();
        if (node is XmlDeclaration declaration)
        {
            var version = declaration.Version;
            if (!string.IsNullOrWhiteSpace(version)) obj.Add("@version", version);
            var encoding = declaration.Encoding;
            if (!string.IsNullOrWhiteSpace(encoding)) obj.Add("@encoding", encoding);
            var standalone = declaration.Standalone;
            if (!string.IsNullOrWhiteSpace(standalone)) obj.Add("@standalone", standalone);
        }
        else if (node.Attributes?.Count > 0)
        {
            foreach (XmlAttribute attr in node.Attributes)
            {
                obj.Add($"@{attr.Name}", attr.Value);
            }
        }

        foreach (var group in node.ChildNodes.OfType<XmlNode>().GroupBy(x => x.Name))
        {
            if (group.Skip(1).Any())
            {
                var subNode = new JsonArray();
                foreach (var _node in group)
                {
                    var jsonNode = SerializeXmlNodeCore(_node);
                    subNode.Add(jsonNode);
                }
                obj.Add(group.Key, subNode);
            }
            else
            {
                var subNode = group.First();
                var jsonNode = SerializeXmlNodeCore(subNode);
                if (subNode.NodeType == XmlNodeType.XmlDeclaration)
                {
                    obj.Add($"?{subNode.Name}", jsonNode);
                }
                else
                {
                    obj.Add(subNode.Name, jsonNode);
                }
            }
        }
        return obj;
    }

    /// <summary>
    /// Serializes the <see cref="XmlNode" /> to a JSON string.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public static string SerializeXmlNode(XmlNode? node)
    {
        if (node is null) return null;

        var jsonNode = SerializeXmlNodeCore(node);
        return JsonSerializer.Serialize(jsonNode);
    }

    /// <summary>
    /// Serializes the xml to a JSON string.
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static string SerializeXmlNode(
#if NET7_0_OR_GREATER
        [StringSyntax(StringSyntaxAttribute.Xml)]
#endif
        string? xml
    )
    {
        if (xml is null) return null;

        var doc = new XmlDocument();
        doc.LoadXml(xml);
        var jsonNode = SerializeXmlNodeCore(doc);
        return JsonSerializer.Serialize(jsonNode);
    }

    private static void WriteXmlNode(XmlDocument doc, Dictionary<string, string> namespaces, XmlNode super, JsonObject obj)
    {
        foreach (var pair in obj)
        {
            var name = pair.Key;
            var node = pair.Value;

            if (name.StartsWith("?"))
            {
                var version = node["@version"]?.ToString();
                var encoding = node["@encoding"]?.ToString();
                var standalone = node["@standalone"]?.ToString();
                var declaration = doc.CreateXmlDeclaration(version, encoding, standalone);
                super.AppendChild(declaration);
            }
            else if (name.StartsWith("@"))
            {
                var pureName = name.Substring(1);
                var attr = doc.CreateAttribute(pureName);
                attr.Value = node.ToString();
                super.Attributes.Append(attr);
            }
            else if (name.StartsWith("#"))
            {
                if (name == "#text")
                {
                    var textNode = doc.CreateTextNode(node.ToString());
                    super.AppendChild(textNode);
                }
            }
            else
            {
                if (node is JsonObject jsonObject)
                {
                    var nsClear = new List<string>();
                    foreach (var prop in jsonObject.Where(x => x.Key.StartsWith("@xmlns")))
                    {
                        var (_, nsPrefix) = GetPrefixNameTuple(prop.Key);
                        namespaces.Add(nsPrefix, prop.Value.ToString());
                        nsClear.Add(nsPrefix);
                    }

                    var element = CreateElement(doc, namespaces, name);
                    WriteXmlNode(doc, namespaces, element, jsonObject);
                    super.AppendChild(element);

                    foreach (var ns in nsClear)
                    {
                        namespaces.Remove(ns);
                    }
                }
                else if (node is JsonArray jsonArray)
                {
                    foreach (var item in jsonArray)
                    {
                        var element = CreateElement(doc, namespaces, name);
                        if (item is JsonObject itemObject)
                        {
                            WriteXmlNode(doc, namespaces, element, itemObject);
                        }
                        else
                        {
                            var textNode = doc.CreateTextNode(item.ToString());
                            element.AppendChild(textNode);
                        }
                        super.AppendChild(element);
                    }
                }
                else
                {
                    var element = CreateElement(doc, namespaces, name);
                    var textNode = doc.CreateTextNode(node.ToString());
                    element.AppendChild(textNode);
                    super.AppendChild(element);
                }
            }
        }
    }

    /// <summary>
    /// Deserializes the <see cref="XmlNode" /> from a JSON string.
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static XmlDocument DeserializeXmlNode(
#if NET7_0_OR_GREATER
        [StringSyntax(StringSyntaxAttribute.Json)]
#endif
        string? json
    )
    {
        if (json is null) return null;

        var doc = new XmlDocument();
        var node = JsonSerializer.Deserialize<JsonNode>(json);
        var namespaces = new Dictionary<string, string>();
        WriteXmlNode(doc, namespaces, doc, node.AsObject());
        return doc;
    }
}
#endif
