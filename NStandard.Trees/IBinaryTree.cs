using System;
using System.Collections.Generic;
using System.Text;

namespace NStandard.Trees
{
    public interface IBinaryTree<TNode, TModel>
    {
        TModel Model { get; set; }

        TNode LeftNode { get; set; }
        TNode RightNode { get; set; }

    }
}
