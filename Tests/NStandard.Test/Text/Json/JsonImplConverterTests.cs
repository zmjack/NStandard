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

    [JsonAs<Table, ITableCell>]
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

    [JsonImpl<ILength, Cls>]
    public interface ILength
    {
        int Length { get; set; }
    }

    [JsonImpl<INameable>]
    public interface INameable
    {
        string Name { get; set; }
    }

    [JsonAs<Cls, ILength>]
    public class Cls : ILength, INameable, IEnumerable<int>
    {
        public int Length
        {
            get => (this as ILength).Length;
            set => (this as ILength).Length = value * 10;
        }
        int ILength.Length { get; set; }

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
        ILength a = new Cls
        {
            Length = 10,
        };
        Assert.Equal(
"""
{
  "Length": 100
}
""",
        Json(a));

        INameable b = new Cls
        {
            Length = 10,
        };
        Assert.Equal(
"""
{
  "Length": 100
}
""",
        Json(b));

        var c = new Cls
        {
            Length = 10,
        };
        Assert.Equal(
"""
{
  "Length": 100
}
""",
        Json(c));
    }

    [Fact]
    public void ImplTest2()
    {
        var cls = JsonSerializer.Deserialize<ILength>(
"""
{
  "name": "Back",
  "length": 10
}
""", new JsonSerializerOptions(JsonSerializerDefaults.Web));

        Assert.Equal(new Cls
        {
            Length = 10,
        }, cls);
    }
}
