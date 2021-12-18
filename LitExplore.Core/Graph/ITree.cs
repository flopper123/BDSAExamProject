namespace LitExplore.Core.Graph;


// T refers to the data object the Node holds
// Creates a directed Tree from a specific root of type T
public interface ITree<T> : IEnumerable<INode<T>>, IDisposable
    where T : IEquatable<T>
{
    // Number of total nodes in the tree
    UInt64 Size { get; }

    /// <summary>
    /// Returns a pointer to the root Node of the Tree
    /// </summary> 
    INode<T>? Root { get; set; }

    /// <summary>
    /// Tries to add the given Node to the Tree. The Node will be 
    /// added to the Tree representation of the v.parent.
    /// if v.parent is contained in the Tree.
    /// if not it returns false, and does nothing.
    /// Asymptotic running time: O(N)
    /// 
    /// In case the Tree is empty, Node v will be added as a root node to the Tree
    /// </summary> 
    /// <returns> A boolean indicating whether the addition was succesful </returns>
    bool Add(INode<T> v);

    /// <summary>
    /// Tries to delete all nodes containing T from the Tree and all of those nodes children.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    bool Delete(T v);
}