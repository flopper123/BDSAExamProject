namespace LitExplore.Core.Graph;

using LitExplore.Core.Filter;

// A graph implemented as a forest
public interface IGraph<Key, T> : IEnumerable<ITree<Key>>
    where Key : IEquatable<Key>
{
    // Total amount of nodes in the graph
    UInt64 Size { get; }

    // Returns an unordered list of all trees in the graph
    IList<ITree<Key>> Roots { get; set; }

    void Apply(Filter<NodeDetails<T>> filter);

    /// <summary>
    /// Tries to add the given Node to the graph. The Node will be 
    /// added to all trees in the graph that contains v.parent
    /// Asymptotic running time: O(N)
    /// 
    /// In case the graph is empty, vertex v will be added as a root node to the graph
    /// </summary> 
    /// <returns> An number indicating how many roots the node was added to </returns>
    UInt64 Add(INode<Key> v);

    /// <summary>
    /// Tries to delete Key from the graph. All nodes containing the value will be 
    /// deleted from the trees in the graph. 
    /// Asymptotic running time: O(N)
    /// </summary> 
    /// <returns> An number indicating how many roots Key was deleted from </returns>
    UInt64 Delete(Key v);
}