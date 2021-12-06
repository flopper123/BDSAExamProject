namespace LitExplore.Core.Graph;
/*
public interface IGraphHandler<T> {
    IVertex<T> ToVertex(T type);
}
*/

// T refers to the data object the Vertex holds
// Creates a directed graph from a specific root of type T
public interface IGraph<T> : IEnumerable<T>, IDisposable
    where T : IEquatable<T>
{
    // Number of total vertices in the given graph
    UInt64 Size { get; }
    IVertex<T> Root { get; protected set; }

    /// <summary>
    /// Tries to add the given vertex to the graph. The vertex will be 
    /// added to the graph representation of the v.parent.
    /// if v.parent is contained in the graph.
    /// if not it returns false, and does nothing.
    /// Asymptotic running time: O(N)
    /// </summary> 
    /// <returns> A boolean indicating whether the addition was succesful </returns>
    bool Add(IVertex<T> v);

    /// <summary>
    /// Tries to delete the given vertex from the graph.
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    bool Delete(IVertex<T> v);
    /*
    {
        Should also set depth of the vertex
        IVertex<T> par = Root.find(v.Parent());
        return if-succesful(par.GetChildren().Add(par));
    }
    */
}