using NStandard.Text.Json;
using System.Text.Json;
using Xunit;

namespace NStandard.Test.Text.Json;

public class JsonImplConverterTests
{
    [JsonImplConverter<ITableCell>]
    public interface ITableCell
    {
        string Value { get; set; }
    }

    public class Table : ITableCell
    {
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
""", json);
    }
}
