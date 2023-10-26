using NStandard.Trees;
using System.Linq;
using Xunit;

namespace NStandard.Test.Trees;

public class AvlTreeTests
{
    [Fact]
    public void Test1()
    {
        /*
         *              13
         *            /    \
         *           10     15
         *          /  \      \
         *         5    11     16
         *        / \
         *       4   8
         *      /
         *     3
         */
        var tree = new AvlTree<int>(13)
        {
            LeftNode = new AvlTree<int>(10)
            {
                LeftNode = new AvlTree<int>(5)
                {
                    LeftNode = new AvlTree<int>(4)
                    {
                        LeftNode = new AvlTree<int>(3),
                    },
                    RightNode = new AvlTree<int>(8),
                },
                RightNode = new AvlTree<int>(11)
            },
            RightNode = new AvlTree<int>(15)
            {
                RightNode = new AvlTree<int>(16),
            }
        };

        /*
         *              13
         *            /    \
         *           5      15
         *          / \       \
         *         4   10      16
         *        /   /  \
         *       3   8    11
         */
        tree.GetInOrderNodes().First(x => x.Model == 5).RightTotate();

        Assert.Equal(new[] { 3, 4, 5, 8, 10, 11, 13, 15, 16 }, tree.GetInOrderNodes().Select(x => x.Model));
        Assert.Equal(new[] { 13, 5, 4, 3, 10, 8, 11, 15, 16 }, tree.GetPreOrderNodes().Select(x => x.Model));
        Assert.Equal(new[] { 3, 4, 8, 11, 10, 5, 16, 15, 13 }, tree.GetPostOrderNodes().Select(x => x.Model));
    }

}
