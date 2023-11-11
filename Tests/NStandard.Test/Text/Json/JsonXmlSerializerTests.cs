using System;
using System.Xml;
using Xunit;

namespace NStandard.Text.Json.Test;

public class JsonXmlSerializerTests
{
    private static readonly ValueTuple<string, string>[] tuples = new[]
    {
        ValueTuple.Create(
"""
<?xml version="1.0" encoding="utf-8"?><books xmlns="http://www.contoso.com/books"><book genre="novel" ISBN="1-861001-57-8" publicationdate="1823-01-28"><title>Pride And Prejudice</title><price>24.95</price></book><book genre="novel" ISBN="1-861002-30-1" publicationdate="1985-01-01"><title>The Handmaid's Tale</title><price>29.95</price></book><book genre="novel" ISBN="1-861001-45-3" publicationdate="1811-01-01"><title>Sense and Sensibility</title><price>19.95</price></book></books>
""",
"""
{"?xml":{"@version":"1.0","@encoding":"utf-8"},"books":{"@xmlns":"http://www.contoso.com/books","book":[{"@genre":"novel","@ISBN":"1-861001-57-8","@publicationdate":"1823-01-28","title":"Pride And Prejudice","price":"24.95"},{"@genre":"novel","@ISBN":"1-861002-30-1","@publicationdate":"1985-01-01","title":"The Handmaid's Tale","price":"29.95"},{"@genre":"novel","@ISBN":"1-861001-45-3","@publicationdate":"1811-01-01","title":"Sense and Sensibility","price":"19.95"}]}}
"""
            ),
        ValueTuple.Create(
"""
<?xml version="1.0" encoding="utf-8"?><xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" attributeFormDefault="unqualified" elementFormDefault="qualified" targetNamespace="http://www.contoso.com/books"><xs:element name="books"><xs:complexType><xs:sequence><xs:element maxOccurs="unbounded" name="book"><xs:complexType><xs:sequence><xs:element name="title" type="xs:string" /><xs:element name="price" type="xs:decimal" /></xs:sequence><xs:attribute name="genre" type="xs:string" use="required" /><xs:attribute name="ISBN" type="xs:string" use="required" /><xs:attribute name="publicationdate" type="xs:date" use="required" /></xs:complexType></xs:element></xs:sequence></xs:complexType></xs:element></xs:schema>
""",
"""
{"?xml":{"@version":"1.0","@encoding":"utf-8"},"xs:schema":{"@xmlns:xs":"http://www.w3.org/2001/XMLSchema","@attributeFormDefault":"unqualified","@elementFormDefault":"qualified","@targetNamespace":"http://www.contoso.com/books","xs:element":{"@name":"books","xs:complexType":{"xs:sequence":{"xs:element":{"@maxOccurs":"unbounded","@name":"book","xs:complexType":{"xs:sequence":{"xs:element":[{"@name":"title","@type":"xs:string"},{"@name":"price","@type":"xs:decimal"}]},"xs:attribute":[{"@name":"genre","@type":"xs:string","@use":"required"},{"@name":"ISBN","@type":"xs:string","@use":"required"},{"@name":"publicationdate","@type":"xs:date","@use":"required"}]}}}}}}}
"""
            ),
        ValueTuple.Create(
"""
<xs:schema xmlns:xs="url"><xs:element xmlns:xs2="url" /></xs:schema>
""",
"""
{"xs:schema":{"@xmlns:xs":"url","xs:element":{"@xmlns:xs2":"url"}}}
"""
            ),
        ValueTuple.Create(
"""
<?xml version="1.0" encoding="utf-8" standalone="yes"?><table><v><c>cell</c><c anchor="data">cell-with-property</c></v></table>
""",
"""
{"?xml":{"@version":"1.0","@encoding":"utf-8","@standalone":"yes"},"table":{"v":{"c":["cell",{"@anchor":"data","#text":"cell-with-property"}]}}}
"""
            ),
    };

    [Fact]
    public void SerializeTest()
    {
        foreach (var (xml, json) in tuples)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            Assert.Equal(json, JsonXmlSerializer.SerializeXmlNode(doc).Replace(@"\u0027", "'"));
        }
    }

    [Fact]
    public void DeserializeTest()
    {
        foreach (var (xml, json) in tuples)
        {
            Assert.Equal(xml, JsonXmlSerializer.DeserializeXmlNode(json).OuterXml);
        }
    }

    [Fact]
    public void NullNodeTest()
    {
        var xml = JsonXmlSerializer.DeserializeXmlNode("""
{
    "sheets": [{
        "name": null,
        "comment": "default"
    }]
}
"""
        ).OuterXml;
        Assert.Equal(@"<sheets><comment>default</comment></sheets>", xml);
    }

}
