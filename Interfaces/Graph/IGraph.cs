namespace Interfaces;
public interface IGraph<T>
{
    int NumberOfVertices();
    int NumberOfEdges();
    bool AddVertex(IVertex<T> vertex);//TO:DO overvje om input bare skal værre af tyben 'T'
    bool AddEdge(IVertex<T> from, IVertex<T> to);
    bool AddEdge(int fromId, int toId);
    IEnumerable<IVertex<T>> GetAdj(int Id);
    IEnumerable<IVertex<T>> GetAdj(IVertex<T> vertex);//TO:DO overvje om input bare skal værre af tyben 'T'
}
