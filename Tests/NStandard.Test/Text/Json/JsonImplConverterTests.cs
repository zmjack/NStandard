using NStandard.Text.Json;
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

        var json = JsonSerializer.Serialize(table, new JsonSerializerOptions
        {
            WriteIndented = true,
        });

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
        , json);
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

        var json = JsonSerializer.Serialize(table, new JsonSerializerOptions
        {
            WriteIndented = true,
        });

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
        , json);
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

        var json = JsonSerializer.Serialize(table, new JsonSerializerOptions
        {
            WriteIndented = true,
        });

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
        , json);
    }
}
