namespace LitExplore.Core.Graph;

public interface IVertex<T>
    where T : IEquatable<T>
{
    /// <returns> Returns the parent of this Vertex </returns>
    IVertex<T> GetParent();

    /// <returns> Returns an enumerable containing all children of the given vertex </returns>
    IList<IVertex<T>> GetChildren(); // References to Publications-Vertex<"Publication">

    /// <summary>
    /// Adds a new child to the Vertex 
    /// </summary>
    /// <param name="newChild"></param>
    void AddChild(IVertex<T> newChild);
    
    /// <summary>
    /// Deletes the given vertex, if the vertex is a child. 
    /// </summary>
    /// <param name="newChild"></param>
    bool Delete(IVertex<T> delete);

    /// <returns> Returns the depth of this vertex in the current graph <returns/>
    UInt64 Depth { get; set; }

    /// <returns> Returns the size of the subtree rooted at this vertex <returns/>
    UInt64 Size { get; }

    // <returns> Returns true if this vertex is a root node (i.e. parent is itself.) <returns/>
    bool IsRoot();

    /// <returns> 
    /// The data associated with this IVertex, i.e. 
    /// the data this vertex contains.
    /// </returns>
    T Data { get; init; }
}