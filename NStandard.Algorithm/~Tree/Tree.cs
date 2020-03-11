using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NStandard.Algorithm
{
    public class Tree<TModel> : Tree<Tree<TModel>, TModel>
    {
        public Tree() { }
        public Tree(TModel model) : base(model) { }
    }

    public partial class Tree<TSelf, TModel> : ICloneable
        where TSelf : Tree<TSelf, TModel>, new()
    {
        public Tree()
        {
            var modelType = typeof(TModel);
            if (modelType.IsClass)
            {
                switch (modelType.FullName)
                {
                    case string s when s == typeof(string).FullName:
                        break;

                    default:
                        Model = (TModel)modelType.GetConstructor(new Type[0]).Invoke(null);
                        _Key = GetHashCode().ToString();
                        break;
                }
            }
        }

        public Tree(TModel model) => Model = model;

        public static TSelf Create<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : TModel, ITreeEntity
        {
            var pendingNodeQueue = new Queue<TSelf>();

            entities
                .Where(entity => entity.Parent is null)
                .OrderBy(entity => entity.Index)
                .Then(self =>
                {
                    foreach (var entity in self)
                    {
                        pendingNodeQueue.Enqueue(new TSelf
                        {
                            Id = entity.Id,
                            Model = entity,
                        }.Then(_ => new TSelf().CreateForProperties(_, entity)));
                    }
                });

            var node = new TSelf().Then(_ => _.AddRange(pendingNodeQueue.ToArray()));

            while (pendingNodeQueue.Any())
            {
                var pendingNode = pendingNodeQueue.Dequeue();

                entities
                    .Where(entity => entity.Parent == pendingNode.Id)
                    .OrderBy(entity => entity.Index)
                    .Then(self =>
                    {
                        foreach (var entity in self)
                        {
                            var item = new TSelf
                            {
                                Id = entity.Id,
                                Model = entity,
                            }.Then(_ => new TSelf().CreateForProperties(_, entity));

                            pendingNode.Add(item);
                            pendingNodeQueue.Enqueue(item);
                        }
                    });
            }

            return node;
        }

        public Guid Id { get; set; }
        public TSelf Parent { get; private set; }
        public TModel Model { get; set; }

        private string _Key;
        public virtual string Key { get => _Key; set => _Key = value; }

        public bool IsTreeNode => Children.Any();
        public bool IsLeafNode => !Children.Any();
        public bool IsRoot => Parent is null;

        public HashSet<TSelf> Children { get; private set; } = new HashSet<TSelf>();
        public IEnumerable<TSelf> Trees => Children.Where(x => x.IsTreeNode);
        public IEnumerable<TSelf> Leafs => Children.Where(x => x.IsLeafNode);

        public IEnumerable<TSelf> RecursiveChildren
        {
            get
            {
                foreach (var child in Children)
                {
                    yield return child;
                    if (child.IsTreeNode)
                        foreach (var child_ in child.RecursiveChildren)
                            yield return child_;
                }
            }
        }
        public IEnumerable<TSelf> RecursiveTrees
        {
            get
            {
                foreach (var tree in Trees)
                {
                    yield return tree;
                    if (tree.IsTreeNode)
                        foreach (var tree_ in tree.RecursiveTrees)
                            yield return tree_;
                }
            }
        }
        public IEnumerable<TSelf> RecursiveLeafs
        {
            get
            {
                foreach (var child in Children)
                {
                    if (child.IsTreeNode)
                        foreach (var leaf in child.RecursiveLeafs)
                            yield return leaf;
                    else yield return child;
                }
            }
        }

        public virtual char Separator => '/';
        public string Path
        {
            get
            {
                var path = new Stack<TSelf>();
                path.Push(this as TSelf);
                for (var node = this; node.Parent != null; node = node.Parent)
                    path.Push(node.Parent);

                return string.Join(Separator.ToString(), path.Select(node => node.Key));
            }
        }

        public Tree<TSelf, TModel> Find(string path) => Find(path.TrimStart(Separator).Split(Separator));
        public Tree<TSelf, TModel> Find(params string[] pathKeys)
        {
            var currentNode = this;
            foreach (var key in pathKeys)
            {
                currentNode = currentNode.Children.FirstOrDefault(x => x.Key == key);
                if (currentNode is null) return null;
            }
            return currentNode;
        }

        public void Clear() => Children.Clear();

        public void Add(TSelf node)
        {
            node.Parent = this as TSelf;
            Children.Add(node);
        }
        public void AddRange(TSelf[] nodes)
        {
            nodes.AsParallel().ForAll(node => node.Parent = this as TSelf);
            nodes.Each(node => Add(node));
        }
        public void AddEntry(string path, TSelf node) => AddEntry(path.TrimStart(Separator).Split(Separator), node);
        public void AddEntry(string[] pathKeys, TSelf node)
        {
            var ubound = pathKeys.UBound();
            var currentNode = this;
            foreach (var kv in pathKeys.AsKvPairs())
            {
                var key = kv.Value;
                var subNode = currentNode.Children.FirstOrDefault(x => x.Key == key);
                if (subNode is null)
                {
                    if (kv.Key < ubound)
                    {
                        var newNode = new TSelf();
                        if (!key.IsNullOrEmpty())
                            newNode.Key = key;

                        currentNode.Add(newNode);
                        currentNode = newNode;
                    }
                    else
                    {
                        if (key.IsNullOrEmpty())
                            currentNode.Add(node);
                        else currentNode.Add(node.Then(_ => _.Key = key));
                    }
                }
                else currentNode = subNode;
            }
        }

        public TSelf Clone() => (this as ICloneable).Clone() as TSelf;
        object ICloneable.Clone()
        {
            return new TSelf
            {
                Id = Id,
                Model = Model,
            };
        }

        public string Description => GetDescription(x => x.Key);

        public string GetDescription(Func<Tree<TSelf, TModel>, string> selector)
        {
            var tree = new StringBuilder();
            if (!IsRoot)
                tree.AppendLine(selector(this));

            foreach (var child in Children)
            {
                if (!IsRoot)
                    tree.AppendLine("  " + child.GetDescription(selector).Replace("\r\n", "\r\n  "));
                else tree.AppendLine(child.GetDescription(selector));
            }

            if (tree.Length > 0)
                return tree.ToString().Slice(0, -2);
            else return "";
        }

        public TSelf this[string key]
        {
            get => Children.First(child => child.Key == key);
            set { value.Key = key; Add(value); }
        }

        protected virtual void CreateForProperties(TSelf node, object entity) { }

        public TRet Simplify<TRet>(Func<Tree<TSelf, TModel>, TRet> selector)
        {
            var ret = selector(this);
            var treeChildrenProps = typeof(TRet).GetProperties().Where(x => x.HasAttribute<TreeChildrenAttribute>());
            var treeChildrenFields = typeof(TRet).GetFields().Where(x => x.HasAttribute<TreeChildrenAttribute>());

            foreach (var prop in treeChildrenProps)
                prop.SetValue(ret, Children.Select(x => x.Simplify(selector)).ToArray());
            foreach (var field in treeChildrenFields)
                field.SetValue(ret, Children.Select(x => x.Simplify(selector)).ToArray());

            return ret;
        }

    }
}
