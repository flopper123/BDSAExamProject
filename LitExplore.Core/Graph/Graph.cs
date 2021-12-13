namespace LitExplore.Core.Graph;

using System.Collections;
using LitExplore.Core.Filter;

// A graph implemented as a forest
public abstract class Graph<Key, T> : IGraph<Key, T> // PublicationTilte , PublicationDetails
    where Key : IEquatable<Key>
    where T : IEquatable<Key>, IEquatable<T>
{
    // TODO: instead implement Dictionary<Key, (UInt32, T)> to allow for smart optimizations
    protected Dictionary<Key, T> _details;

    // Total amount of nodes in the graph
    public UInt64 Size { 
        get {
            UInt64 size = 0;
            foreach (ITree<Key> t in Roots) { size += t.Size; }
            return size;
        } 
    }

    // Returns an unordered list of all trees in the graph
    public IList<ITree<Key>> Roots { get; set; } = null!;

    public Graph() {
        Roots = new List<ITree<Key>>();
        _details = new Dictionary<Key, T>();
    }

    public abstract NodeDetails<T> ToDetails(INode<Key> key);

    public abstract void AddDetails(T details);

    /// <summary>
    /// Tries to delete Key from the graph. All nodes containing the value will be 
    /// deleted from the trees in the graph. 
    /// Asymptotic running time: O(N)
    /// </summary> 
    /// <returns> An number indicating how many roots Key was deleted from </returns>
    public abstract UInt64 Delete(Key v);

    public abstract void Apply(Filter<NodeDetails<T>> filter);

    /// <summary>
    /// Tries to add the given Node to the graph. The Node will be 
    /// added to all trees in the graph that contains v.parent
    /// Asymptotic running time: O(N)
    /// If no v.parent is contained in the graph, the node will be added as a new Root.
    /// 
    /// In case the graph is empty, vertex v will be added as a root node to the graph
    /// </summary> 
    /// <returns> An number indicating how many roots the node was added to </returns>
    public virtual UInt64 Add(INode<Key> v) {
        UInt64 adds = 0;
        if (v.IsRoot()) {
            Roots.Add(new Tree<Key>(v));
            adds++;
        } else {
            bool hasAdded = false;
            foreach (ITree<Key> root in Roots)
            {
                if (root.Add(v)) {
                    adds++;
                    hasAdded = true;
                }
            }
            if (hasAdded) {
                Roots.Add(new Tree<Key>(v));
                adds++;
            }
        }
        return adds;
    }

    /// <param name="key"></param>
    /// throws key not found if key isnt in the details
    public T GetDetails(Key key) {
        return _details[key];
    }
        
    IEnumerator<ITree<Key>> IEnumerable<ITree<Key>>.GetEnumerator() {
        foreach (var root in Roots)
        {
            yield return root;
        }
    }

    public IEnumerator GetEnumerator()
    {
        return this.GetEnumerator();
    }
}