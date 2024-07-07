using NStandard.Drawing;
using NStandard.Text.Json;
using System.Collections;
using System.Drawing;
using System.Text.Json;
using Xunit;

namespace NStandard.Test.Text.Json;

public class JsonImplConverterTests
{
    public interface ITableCell
    {
        ITableCell[] Cells { get; set; }
        string Value { get; set; }
    }

    [JsonImpl<Table, ITableCell>]
    public class Table : ITableCell
    {
        public string Name { get; } = "Table";
        public ITableCell[] Cells { get; set; }
        public string Value { get; set; }
    }

    private readonly JsonSerializerOptions _option = new()
    {
        WriteIndented = true,
    };
    private string Json<T>(T obj) => JsonSerializer.Serialize(obj, _option);
    private T FromJson<T>(string json) => JsonSerializer.Deserialize<T>(json, _option);

    public class SimpleModel
    {
        public RgbaColor Color { get; set; }
    }

    [Fact]
    public void JsonValue()
    {
        Assert.Equal(
"""
{
  "Color": "rgba(11,22,33,0.5019)"
}
""",
        Json(new SimpleModel
        {
            Color = new(Color.FromArgb(128, 11, 22, 33))
        }));

        var model = FromJson<SimpleModel>(
"""
{
  "Color": "rgba(11,22,33,0.5019)"
}
"""
        );
        Assert.Equal(11, model.Color.R);
        Assert.Equal(22, model.Color.G);
        Assert.Equal(33, model.Color.B);
        Assert.Equal(128, model.Color.A);
    }

    [Fact]
    public void Test1()
    {
        var table = new Table
        {
            Cells =
            [
                new Table
                {
                    Cells =
                    [
                        new Table
                        {
                            Value = "a",
                        }
                    ],
                    Value = "b",
                }
            ],
            Value = "c",
        };

        Assert.Equal("""
{
  "Cells": [
    {
      "Cells": [
        {
          "Cells": null,
          "Value": "a"
        }
      ],
      "Value": "b"
    }
  ],
  "Value": "c"
}
"""
        , Json(table));
    }

    public class TableV2 : ITableCell
    {
        public string ClassName { get; } = "TableV2";
        public ITableCell[] Cells { get; set; }
        public string Value { get; set; }
    }

    [Fact]
    public void Test2()
    {
        var table = new TableV2
        {
            Cells =
            [
                new TableV2
                {
                    Cells =
                    [
                        new TableV2
                        {
                            Value = "a",
                        }
                    ],
                    Value = "b",
                }
            ],
            Value = "c",
        };

        Assert.Equal("""
{
  "ClassName": "TableV2",
  "Cells": [
    {
      "Cells": [
        {
          "Cells": null,
          "Value": "a"
        }
      ],
      "Value": "b"
    }
  ],
  "Value": "c"
}
"""
        , Json(table));
    }

    [Fact]
    public void InterfaceTest()
    {
        ITableCell table = new TableV2
        {
            Cells =
            [
                new TableV2
                {
                    Cells =
                    [
                        new TableV2
                        {
                            Value = "a",
                        }
                    ],
                    Value = "b",
                }
            ],
            Value = "c",
        };

        Assert.Equal("""
{
  "Cells": [
    {
      "Cells": [
        {
          "Cells": null,
          "Value": "a"
        }
      ],
      "Value": "b"
    }
  ],
  "Value": "c"
}
"""
        , Json(table));
    }


    [JsonImpl<ILength>]
    public interface ILength
    {
        int Length { get; set; }
    }

    [JsonImpl<INameable>]
    public interface INameable
    {
        string Name { get; set; }
    }

    [JsonImpl<Cls, IJson>]
    public class Cls : ILength, INameable, Cls.IJson, IEnumerable<int>
    {
        public interface IJson
        {
            string Name { get; set; }
        }

        public int Length { get; set; }
        public string Name { get; set; } = nameof(Cls);

        public IEnumerator<int> GetEnumerator()
        {
            return Array.Empty<int>().AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    [Fact]
    public void ImplTest()
    {
        ILength a = new Cls();
        Assert.Equal(
"""
{
  "Name": "Cls"
}
""",
        Json(a));

        INameable b = new Cls();
        Assert.Equal(
"""
{
  "Name": "Cls"
}
""",
        Json(b));

        var c = new Cls();
        Assert.Equal(
"""
{
  "Name": "Cls"
}
""",
        Json(c));
    }
}
