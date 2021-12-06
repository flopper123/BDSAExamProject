namespace LitExplore.Core.Graph;

public class Vertex<T> : IVertex<T>
    where T : IEquatable<T>
{
    protected IVertex<T> _parent;
    protected IList<IVertex<T>> _children;

    public T Data { get; init; }
    public UInt64 Depth { get; set; }

    // worst case O(N), where N is size of subtree rooted at this vertex.
    public UInt64 Size
    {
        get
        {
            UInt64 size = 1UL;
            foreach(var child in GetChildren()) 
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
        _parent = this;
        _children = new List<IVertex<T>>();
    }

    public void AddChild(IVertex<T> newChild)
    {
        _children.Add(newChild);
    }

    public IList<IVertex<T>> GetChildren()
    {
        return _children;
    }

    public bool IsRoot()
    {
        return (_parent == this);
    }

    public IVertex<T> GetParent()
    {
        return _parent;
    }

    public bool Delete(IVertex<T> tar)
    {
        var children = this.GetChildren();
        int i = 0;
        for (i = 0; i < children.Count; i++)
        {
            var child = children[i];
            if (tar.Data.Equals(child.Data))
            {
                children.RemoveAt(i);
                return true;
            }
        }
        return false;
    }
}