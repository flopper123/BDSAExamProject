namespace Interfaces;
public interface IGraph<T>
{
    public bool AddVertex(IVertex<T> vertex);
    public IEdge<T> AddEdge(IVertex<T> from, IVertex<T> to);
    public IEnumerable<IVertex<T>> GetAdj(IVertex<T> vertex);
}
