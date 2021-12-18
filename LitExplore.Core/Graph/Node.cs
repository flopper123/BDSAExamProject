namespace LitExplore.Core.Graph;

public class Node<T> : INode<T>
    where T : IEquatable<T>
{
    private INode<T> _parent = null!;

    public T Data { get; init; }

    // TO:DO add tests for depth
    public UInt64 Depth { get; set; }
    public IList<INode<T>> Children { get; set; } = null!;
    public INode<T> Parent {
        get { 
            return _parent; 
        }
        set {
            _parent = value;
            if (!IsRoot()) {
                Depth = value.Depth + 1UL;
            }
        }
    }

    // worst case O(N), where N is size of subtree rooted at this Node.
    public UInt64 Size
    {
        get
        {
            UInt64 size = 1UL;
            foreach (var child in Children)
            {
                size += child.Size;
            }
            return size;
        }
    }

    public Node(T data)
    {
        Data = data;
        Depth = 1UL;
        Parent = this;
        Children = new List<INode<T>>();
    }

    /// <param name="data"> Source data to hold in Node </param>
    /// <param name="parent"> A pointer to the parent for the constructed Node </param>
    /// <exception cref="ArgumentException"> 
    /// Throws argument exception if input param @parent.Data equals other param @data.
    /// </exception>
    public Node(T data, INode<T> parent)
    {
        if (data.Equals(parent.Data)) {
            throw new ArgumentException("Node constructor received invalid argument.\n\t" +
                                        "Parent must not be itself; please use Node(T data) if you want to set a root Node.\n");
        }

        Data = data;
        Parent = parent;
        Children = new List<INode<T>>();
    }

    public bool IsRoot() { return (Parent == this); }
    public bool IsLeaf() { return (Size == 1UL); }

    /// <summary>
    /// Depth-First-Search for needle. 
    /// </summary>
    /// <param name="needle"> Target to search for  </param>
    /// <returns></returns>
    public INode<T>? Find(T needle)
    {
        if (Data.Equals(needle)) return this;

        INode<T>? tar = null;

        foreach (var child in this.Children)
        {
            tar = child.Find(needle);

            if (tar != null)
            {
                break;
            }
        }

        return tar;
    }

    public override string ToString()
    {
        return $"Node depth@{Depth} data@{Data.ToString()}";
    }
}