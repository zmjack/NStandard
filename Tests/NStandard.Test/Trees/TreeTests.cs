using Xunit;

namespace NStandard.Trees.Test;

public class TreeTests
{
    private class CA
    {
        public int Value { get; set; }
        public int? Parent { get; set; }
        public CA[] CAs { get; set; }

        public override string ToString() => Value.ToString();
    }

    private readonly Tree<CA> Tree1 = Tree.From(new CA
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

    private readonly Tree<CA> Tree2 = Tree.From(new[]
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
        foreach (var tree in new[] { Tree1, Tree2 })
        {
            Assert.Equal(1, tree.Root.Model.Value);
            Assert.Equal([1, 2, 3],
            (
                from x in tree.NonLeaves
                select x.Model.Value
            ).ToArray());
            Assert.Equal([11, 4, 5],
            (
                from x in tree.Leaves
                select x.Model.Value
            ).ToArray());
            Assert.Equal([1, 2, 3, 11, 4, 5],
            (
                from x in tree.RecursiveNodes
                select x.Model.Value
            ).ToArray());
        }
    }

    [Fact]
    public void Test2()
    {
        Func<TreeNode<CA>, bool> IsValidNode(params int[] values)
        {
            bool Find(TreeNode<CA> current)
            {
                return values.Contains(current.Model.Value)
                    || current.Any(x => Find(x));
            }
            return Find;
        }

        var tree = Tree1.RecursiveNodes.Where(IsValidNode(4, 11));
        Assert.Equal([1, 2, 3, 11, 4], tree.Select(x => x.Model.Value).ToArray());
    }

    [Fact]
    public void VisiteTest()
    {
        Assert.Equal([1, 2, 3, 11, 5],
        (
            from x in Tree1.Visit(x => x.Model.Value % 2 == 1 || x.Model.Value == 2).RecursiveNodes
            select x.Model.Value
        ).ToArray());
    }
}
