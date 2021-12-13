namespace LitExplore.Core.Graph;

public interface INode<T>
    where T : IEquatable<T>
{
    /// <returns> 
    /// The data associated with this INode, i.e. 
    /// the data this vertex contains.
    /// </returns>
    T Data { get; init; }

    /// <returns> The parent of this Vertex </returns>
    INode<T> Parent { get; set; }

    /// <returns> Returns an IList containing all children of the given vertex </returns>
    IList<INode<T>> Children { get; set; }

    /// <summary> 
    /// Searches the tree rooted at this vertex for a vertex for which
    /// vertex.Data.Equals(needle). If the search is succesful it returns 
    /// a pointer to the vertex, else it returns null.
    /// </summary>
    /// <returns> 
    /// If find is succesful, a pointer to the requested vertex. 
    /// If find is not succesful, null is returned.
    /// </returns>
    INode<T>? Find(T needle);

    /// <returns> Returns the depth of this vertex in the current graph </returns>
    UInt64 Depth { get; set; }

    /// <returns> Returns the size of the subtree rooted at this vertex </returns>
    UInt64 Size { get; }

    /// <returns> Returns true if this vertex is a root node (i.e. parent is itself.) <returns/>
    bool IsRoot();

    /// <returns> Returns true if this vertex is a leaf node. Root nodes with no children will return True </returns>
    bool IsLeaf();
}