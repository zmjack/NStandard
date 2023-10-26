using System;
using Xunit;

namespace NStandard.Test;

public class ChainPropertyTests
{
    public class Book
    {
        public string Name { get; set; }
        public Editor Author { get; set; }
    }

    public class Editor
    {
        public string Description { get; set; }
    }

    private static readonly Book _book = new Book
    {
        Name = "abc",
        Author = new Editor
        {
            Description = "author",
        }
    };

    [Fact]
    public void Test0()
    {
        Assert.Throws<ArgumentException>(() => typeof(Book).GetChainProperty());
    }

    [Fact]
    public void Test1()
    {
        var prop = typeof(Book).GetChainProperty(nameof(Book.Author));
        var author = prop.GetValue(_book);
        Assert.Equal(_book.Author, author);
    }

    [Fact]
    public void Test2()
    {
        var prop = typeof(Book).GetChainProperty(nameof(Book.Author), nameof(Editor.Description));
        var desc = prop.GetValue(_book.Author);
        Assert.Equal(_book.Author.Description, desc);
    }
}
