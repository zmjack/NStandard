namespace NStandard.Trees;

public class BinaryTree<TModel> : IBinaryTree<BinaryTree<TModel>, TModel>
{
    public BinaryTree() { }
    public BinaryTree(TModel model)
    {
        Model = model;
    }

    public TModel? Model { get; set; }
    public BinaryTree<TModel>? Parent { get; set; }

    private BinaryTree<TModel>? _left;
    public BinaryTree<TModel>? LeftNode
    {
        get => _left;
        set
        {
            if (value is not null)
            {
                value.Parent = this;
                _left = value;
            }
            else
            {
                if (_left is not null)
                {
                    _left.Parent = null;
                }
                _left = null;
            }
        }
    }

    private BinaryTree<TModel>? _right;
    public BinaryTree<TModel>? RightNode
    {
        get => _right;
        set
        {
            if (value is not null)
            {
                value.Parent = this;
                _right = value;
            }
            else
            {
                if (_right is not null)
                {
                    _right.Parent = null;
                }
                _right = null;
            }
        }
    }

}
