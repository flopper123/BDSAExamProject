namespace LitExplore.Core.Graph;

public class Tree<T> : ITree<T>
    where T : IEquatable<T>
{
    private bool disposedValue;

    public UInt64 Size
    {
        get => (Root == null) ? 0 : Root.Size;
    }

    // ? since we can have a Tree, where root has been removed already
    public INode<T>? Root { get; set; }

    public Tree()
    {
        Root = null;
    }

    public Tree(INode<T> root)
    {
        Root = root;
    }

    public bool Add(INode<T> v)
    {
        if (Root == null) Root = v.Parent;
        else
        {

            T needle = v.Parent.Data;
            INode<T>? tar = Root.Find(needle);

            if (tar == null) return false;
            else
            {

                // Set parent of v to found target
                v.Parent = tar;
                tar.Children.Add(v);
            }
        }

        return true;
    }

    public bool Delete(INode<T> v)
    {
        if (Root == null)
        {
            return false;
        }

        //  Find v.Data
        INode<T>? tar = Root.Find(v.Data);

        if (tar == null)
        {
            return false;
        }

        tar.Parent.Children.Remove(tar);
        return true;
    }
    
    /// <summary>
    ///  Returns all data in the Tree. 
    ///  The data is returned in the order matching a depth-first-search of the tree in the Tree.
    /// </summary>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        foreach (var childData in getChildrenData(Root!))
        {
            yield return childData;
        }

        IList<T> getChildrenData(INode<T> child)
        {
            var tmp = new List<T>();
            tmp.Add(child.Data);
            if (!child.IsLeaf())
            {
                foreach (var grandChild in child.Children)
                {
                    tmp.AddRange(getChildrenData(grandChild));
                }
            }
            return tmp;
        }  
    }

    public IEnumerator GetEnumerator()
    {
        return this.GetEnumerator();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            Root = null;
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}