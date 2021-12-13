namespace LitExplore.Core.Graph;

public interface IGraph<T> : IEnumerable<ITree<T>>
    where T : IEquatable<T>
{
    // Total amount of nodes in the graph
    UInt64 Size { get; }

    // Returns an unordered list of all trees in the graph
    IList<ITree<T>> Roots { get; set; }

    /// <summary>
    /// Tries to add the given Node to the graph. The Node will be 
    /// added to all trees in the graph that contains v.parent
    /// Asymptotic running time: O(N)
    /// 
    /// In case the graph is empty, vertex v will be added as a root node to the graph
    /// </summary> 
    /// <returns> An number indicating how many roots the node was added to </returns>
    UInt64 Add(INode<T> v);

    /// <summary>
    /// Tries to delete T from the graph. All nodes containing the value will be 
    /// deleted from the trees in the graph. 
    /// Asymptotic running time: O(N)
    /// </summary> 
    /// <returns> An number indicating how many roots T was deleted from </returns>
    UInt64 Delete(T v);
}