using Xunit;

namespace NStandard.Trees.Test;

public class TreesTests
{
    private class CA
    {
        public int Value { get; set; }
        public int? Parent { get; set; }
        public CA[] CAs { get; set; }

        public override string ToString() => Value.ToString();
    }

    private readonly Tree<CA> Tree1 = Tree.Parse(new CA
    {
        Value = 1,
        CAs =
        [
            new CA
            {
                Value = 2,
                CAs =
                [
                    new CA
                    {
                        Value = 3,
                        CAs =
                        [
                            new CA
                            {
                                Value = 11,
                            }
                        ]
                    },
                    new CA { Value = 4 },
                ]
            },
            new CA { Value = 5 },
        ]
    }, x => x.CAs);

    private readonly Tree<CA> Tree2 = Tree.ParseRange(new[]
    {
        new CA { Value = 1, Parent = null },
        new CA { Value = 2, Parent = 1 },
        new CA { Value = 3, Parent = 2 },
        new CA { Value = 4, Parent = 2 },
        new CA { Value = 5, Parent = 1 },
        new CA { Value = 11, Parent = 3 },
    }, x => x.Value, x => x.Parent).First();

    [Fact]
    public void Test1()
    {
        Assert.Equal(1, Tree1.Model.Value);
        Assert.Equal([2, 3], Tree1.SelectNonLeafs().Select(x => x.Model.Value).ToArray());
        Assert.Equal([11, 4, 5], Tree1.SelectLeafs().Select(x => x.Model.Value).ToArray());
        Assert.Equal([2, 3, 11, 4, 5], Tree1.GetNodes().Select(x => x.Model.Value).ToArray());
    }

    [Fact]
    public void Test2()
    {
        Assert.Equal(1, Tree2.Model.Value);
        Assert.Equal([2, 3], Tree2.SelectNonLeafs().Select(x => x.Model.Value).ToArray());
        Assert.Equal([11, 4, 5], Tree2.SelectLeafs().Select(x => x.Model.Value).ToArray());
        Assert.Equal([2, 3, 11, 4, 5], Tree2.GetNodes().Select(x => x.Model.Value).ToArray());
    }

    [Fact]
    public void Test3()
    {
        Func<Tree<CA>, bool> IsValidNode(params int[] values)
        {
            bool Find(Tree<CA> current)
            {
                if (values.Contains(current.Model.Value)) return true;
                else return current.Children.Any(x => Find(x));
            }
            return Find;
        }

        var tree = Tree1.Filter(IsValidNode(4, 11));
        Assert.Equal([2, 3, 11, 4], tree.GetNodes().Select(x => x.Model.Value).ToArray());
    }
}
