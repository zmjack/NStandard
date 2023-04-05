using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NStandard.Test
{
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

        [Fact]
        public void Test()
        {
            var prop = typeof(Book).GetChainProperty(nameof(Book.Author), nameof(Editor.Description));
            var author = new Editor
            {
                Description = "abc",
            };
            var desc = prop.GetValue(author);
            Assert.Equal("abc", desc);
        }
    }
}
