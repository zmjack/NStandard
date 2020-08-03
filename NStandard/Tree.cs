using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NStandard
{
    public static class Tree
    {
        public static Tree<TModel> Parse<TModel>(TModel model, Func<TModel, ICollection<TModel>> childrenGetter)
        {
            void AddChildren(Tree<TModel> tree)
            {
                var children = childrenGetter(tree.Model);

                if (children?.Any() ?? false)
                {
                    tree.AddChildren(children);
                    foreach (var child in tree.Children)
                        AddChildren(child);
                }
            }

            var tree = new Tree<TModel>(model);
            AddChildren(tree);
            return tree;
        }

        public static Tree<TModel>[] Parse<TModel>(IEnumerable<TModel> models, Func<TModel, ICollection<TModel>> childrenGetter)
        {
            return models.Select(x => Parse(x, childrenGetter)).ToArray();
        }

        public static Tree<TModel>[] ParseRange<TModel, TKey>(IEnumerable<TModel> models, Func<TModel, TKey> keySelector, Func<TModel, TKey> parentGetter)
            where TModel : new()
        {
            if (!typeof(TKey).IsNullable()) throw new ArgumentException($"The argument({nameof(parentGetter)} must return a nullable type.");

            void AddChildren(Tree<TModel> tree)
            {
                var children = models.Where(x => parentGetter(x).Equals(keySelector(tree.Model)));

                if (children?.Any() ?? false)
                {
                    tree.AddChildren(children);
                    foreach (var child in tree.Children)
                        AddChildren(child);
                }
            }

            var trees = models.Select(x => new Tree<TModel>(x)).ToArray();
            foreach (var tree in trees) AddChildren(tree);
            return trees;
        }

    }

    public class Tree<TModel>
    {
        public Tree<TModel> Parent { get; private set; }
        public HashSet<Tree<TModel>> Children { get; private set; } = new HashSet<Tree<TModel>>();

        public TModel Model { get; private set; }

        public Tree(TModel model)
        {
            Model = model;
        }

        public void AddChild(TModel model)
        {
            var tree = new Tree<TModel>(model)
            {
                Parent = this,
            };
            Children.Add(tree);
        }

        public void AddChildren(IEnumerable<TModel> models)
        {
            foreach (var model in models) AddChild(model);
        }

        public IEnumerable<Tree<TModel>> GetNodes()
        {
            foreach (var node in Children)
            {
                yield return node;

                if (node.Children.Any())
                {
                    foreach (var node_ in node.GetNodes())
                        yield return node_;
                }
            }
        }

        public IEnumerable<Tree<TModel>> SelectNonLeafs()
        {
            foreach (var node in Children.Where(x => x.Children.Any()))
            {
                yield return node;

                foreach (var node_ in node.SelectNonLeafs())
                    yield return node_;
            }
        }

        public IEnumerable<Tree<TModel>> SelectLeafs()
        {
            foreach (var node in Children)
            {
                if (node.Children.Any())
                {
                    foreach (var leaf in node.SelectLeafs())
                        yield return leaf;
                }
                else yield return node;
            }
        }

    }
}
