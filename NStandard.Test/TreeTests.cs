using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace NStandard
{
    public class TreeTests
    {
        private class CA
        {
            public int Value { get; set; }
            public int? Parent { get; set; }
            public CA[] CAs { get; set; }
        }

        [Fact]
        public void Test1()
        {
            var model = new CA
            {
                Value = 1,
                CAs = new[]
                {
                    new CA
                    {
                        Value = 2,
                        CAs = new[]
                        {
                            new CA { Value = 3 },
                            new CA { Value = 4 },
                        }
                    },
                    new CA { Value = 5 },
                }
            };

            var tree = Tree.Parse(model, x => x.CAs);
            Assert.Equal(1, tree.Model.Value);
            Assert.Equal(new[] { 2 }, tree.SelectNonLeafs().Select(x => x.Model.Value).ToArray());
            Assert.Equal(new[] { 3, 4, 5 }, tree.SelectLeafs().Select(x => x.Model.Value).ToArray());
            Assert.Equal(new[] { 2, 3, 4, 5 }, tree.GetNodes().Select(x => x.Model.Value).ToArray());
        }

        [Fact]
        public void Test2()
        {
            var models = new[]
            {
                new CA { Value = 1, Parent = null },
                new CA { Value = 2, Parent = 1 },
                new CA { Value = 3, Parent = 2 },
                new CA { Value = 4, Parent = 2 },
                new CA { Value = 5, Parent = 1 },
            };

            var tree = Tree.ParseRange(models, x => x.Value, x => x.Parent).First();
            Assert.Equal(1, tree.Model.Value);
            Assert.Equal(new[] { 2 }, tree.SelectNonLeafs().Select(x => x.Model.Value).ToArray());
            Assert.Equal(new[] { 3, 4, 5 }, tree.SelectLeafs().Select(x => x.Model.Value).ToArray());
            Assert.Equal(new[] { 2, 3, 4, 5 }, tree.GetNodes().Select(x => x.Model.Value).ToArray());
        }

    }
}
