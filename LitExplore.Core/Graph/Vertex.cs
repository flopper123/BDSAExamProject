namespace LitExplore.Core.Graph;

public class Vertex<T> : IVertex<T>
    where T : IEquatable<T>
{
    
    public T Data { get; init; }
    public UInt64 Depth { get; set; }
    public IList<IVertex<T>> Children { get; set; }
    public IVertex<T> Parent { get; set; }

    // worst case O(N), where N is size of subtree rooted at this vertex.
    public UInt64 Size
    {
        get
        {
            UInt64 size = 1UL;
            foreach(var child in Children) 
            {
                size += child.Size; 
            } 
            return size;
        }
    }

    public Vertex(T data)
    {
        Data = data;
        Depth = 0;
        Parent = this;
        Children = new List<IVertex<T>>();
    }

    public bool IsRoot() { return (Parent == this); }
    public bool IsLeaf() { return (Size == 1UL); }

    /// <summary>
    /// Depth-First-Search for needle. 
    /// </summary>
    /// <param name="needle"> Target to search for  </param>
    /// <returns></returns>
    public IVertex<T>? Find(T needle) {
        if (Data.Equals(needle)) return this;

        IVertex<T>? tar = null;

        foreach (var child in this.Children) {
            tar = child.Find(needle);
            
            if (tar != null) {
                break;
            }
        }

        return tar;
    }
}