namespace LitExplore.Core.Graph;

public class Graph<T> : IGraph<T>
    where T : IEquatable<T>
{
    private bool disposedValue;

    public UInt64 Size
    {
        get => (Root == null) ? 0 : Root.Size;
    }

    // ? since we can have a graph, where root has been removed already
    public IVertex<T>? Root { get; set; }

    public Graph()
    {
        Root = null;
    }

    public Graph(IVertex<T> root)
    {
        Root = root;
    }

    public bool Add(IVertex<T> v)
    {
        if (Root == null) Root = v.Parent;
        else
        {

            T needle = v.Parent.Data;
            IVertex<T>? tar = Root.Find(needle);

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

    public bool Delete(IVertex<T> v)
    {
        if (Root == null)
        {
            return false;
        }

        //  Find v.Data
        IVertex<T>? tar = Root.Find(v.Data);

        if (tar == null)
        {
            return false;
        }

        tar.Parent.Children.Remove(tar);
        return true;
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        foreach (var childData in getCildrenData(Root!))
        {
            yield return childData;
        }
        
        IList<T> getCildrenData(IVertex<T> child)
        {
            var tmp = new List<T>();
            tmp.Add(child.Data);
            if (!child.IsLeaf())
            {
                foreach (var grandChild in child.Children)
                {
                    tmp.AddRange(getCildrenData(grandChild));
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