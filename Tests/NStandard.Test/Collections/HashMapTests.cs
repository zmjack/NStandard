using NStandard.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NStandard.Test.Collections;

public class HashMapTests
{
    [Fact]
    public void Test1()
    {
        var map = new HashMap<string, string>
        {
            [null] = "n",
            ["A"] = "a",
        };
        Assert.True(map.ContainsKey(null));
        Assert.True(map.ContainsKey("A"));
        Assert.False(map.ContainsKey("B"));
        Assert.True(map.ContainsValue("n"));
        Assert.True(map.ContainsValue("a"));
        Assert.False(map.ContainsValue("b"));
        Assert.Equal("n", map[null]);
        Assert.Equal("a", map["A"]);
        Assert.ThrowsAny<KeyNotFoundException>(() => map["B"]);
        Assert.Equal(new[] { null, "A" }, map.Keys);
        Assert.Equal(new[] { "n", "a" }, map.Values);
        Assert.Equal(2, map.Count);

        map.Add("B", "b");
        Assert.True(map.ContainsKey(null));
        Assert.True(map.ContainsKey("A"));
        Assert.True(map.ContainsKey("B"));
        Assert.True(map.ContainsValue("n"));
        Assert.True(map.ContainsValue("a"));
        Assert.True(map.ContainsValue("b"));
        Assert.Equal("n", map[null]);
        Assert.Equal("a", map["A"]);
        Assert.Equal("b", map["B"]);
        Assert.Equal(new[] { null, "A", "B" }, map.Keys);
        Assert.Equal(new[] { "n", "a", "b" }, map.Values);
        Assert.Equal(3, map.Count);

        map.Remove("A");
        Assert.True(map.ContainsKey(null));
        Assert.False(map.ContainsKey("A"));
        Assert.True(map.ContainsKey("B"));
        Assert.True(map.ContainsValue("n"));
        Assert.False(map.ContainsValue("a"));
        Assert.True(map.ContainsValue("b"));
        Assert.Equal("n", map[null]);
        Assert.ThrowsAny<KeyNotFoundException>(() => map["A"]);
        Assert.Equal("b", map["B"]);
        Assert.Equal(new[] { null, "B" }, map.Keys);
        Assert.Equal(new[] { "n", "b" }, map.Values);
        Assert.Equal(2, map.Count);

        map.Remove(null);
        Assert.False(map.ContainsKey(null));
        Assert.False(map.ContainsKey("A"));
        Assert.True(map.ContainsKey("B"));
        Assert.False(map.ContainsValue("n"));
        Assert.False(map.ContainsValue("a"));
        Assert.True(map.ContainsValue("b"));
        Assert.ThrowsAny<KeyNotFoundException>(() => map[null]);
        Assert.ThrowsAny<KeyNotFoundException>(() => map["A"]);
        Assert.Equal("b", map["B"]);
        Assert.Equal(new[] { "B" }, map.Keys);
        Assert.Equal(new[] { "b" }, map.Values);
        Assert.Single(map);

        map.Clear();
        Assert.False(map.ContainsKey(null));
        Assert.False(map.ContainsKey("A"));
        Assert.False(map.ContainsKey("B"));
        Assert.False(map.ContainsValue("n"));
        Assert.False(map.ContainsValue("a"));
        Assert.False(map.ContainsValue("b"));
        Assert.ThrowsAny<KeyNotFoundException>(() => map[null]);
        Assert.ThrowsAny<KeyNotFoundException>(() => map["A"]);
        Assert.ThrowsAny<KeyNotFoundException>(() => map["B"]);
        Assert.Empty(map.Keys);
        Assert.Empty(map.Values);
        Assert.Empty(map);
    }

    [Fact]
    public void DuplicateTest()
    {
        var map = new HashMap<string, string>
        {
            [null] = "n",
            ["A"] = "a",
        };
        Assert.ThrowsAny<ArgumentException>(() => map.Add(null, ".n"));
        Assert.ThrowsAny<ArgumentException>(() => map.Add("A", ".a"));
    }

    [Fact]
    public void NullTest()
    {
        var map = new HashMap<string, string>();
        Assert.ThrowsAny<KeyNotFoundException>(() => map[null]);
        Assert.ThrowsAny<KeyNotFoundException>(() => map["A"]);
    }

    [Fact]
    public void CopyTest()
    {
        var map = new HashMap<string, string>
        {
            [null] = "n",
            ["A"] = "a",
        };
        var array = new KeyValuePair<string, string>[3];

        (map as ICollection<KeyValuePair<string, string>>).CopyTo(array, 1);
        Assert.Equal([null, "n", "a"], array.Select(x => x.Value).ToArray());

        map.Remove(null);
        Assert.ThrowsAny<ArgumentException>(() =>
        {
            (map as ICollection<KeyValuePair<string, string>>).CopyTo(array, 3);
        });
    }
}
