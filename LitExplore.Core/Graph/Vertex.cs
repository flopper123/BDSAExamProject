namespace LitExplore.Core.Graph;

public class Vertex<T> : IVertex<T>
    where T : IEquatable<T>
{
    private IVertex<T> _parent = null!;

    public T Data { get; init; }

    // TO:DO add tests for depth
    public UInt64 Depth { get; set; }
    public IList<IVertex<T>> Children { get; set; } = null!;
    public IVertex<T> Parent {
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

    // worst case O(N), where N is size of subtree rooted at this vertex.
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

    public Vertex(T data)
    {
        Data = data;
        Depth = 1UL;
        Parent = this;
        Children = new List<IVertex<T>>();
    }

    /// <param name="data"> Source data to hold in vertex </param>
    /// <param name="parent"> A pointer to the parent for the constructed vertex </param>
    /// <exception cref="ArgumentException"> 
    /// Throws argument exception if input param @parent.Data equals other param @data.
    /// </exception>
    public Vertex(T data, IVertex<T> parent)
    {
        if (data.Equals(parent.Data)) {
            throw new ArgumentException("Vertex constructor received invalid argument.\n\t" +
                                        "Parent must not be itself; please use Vertex(T data) if you want to set a root vertex.\n");
        }

        Data = data;
        Parent = parent;
        Children = new List<IVertex<T>>();
    }

    public bool IsRoot() { return (Parent == this); }
    public bool IsLeaf() { return (Size == 1UL); }

    /// <summary>
    /// Depth-First-Search for needle. 
    /// </summary>
    /// <param name="needle"> Target to search for  </param>
    /// <returns></returns>
    public IVertex<T>? Find(T needle)
    {
        if (Data.Equals(needle)) return this;

        IVertex<T>? tar = null;

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
        return $"Vertex depth@{Depth} data@{Data.ToString()}";
    }
}