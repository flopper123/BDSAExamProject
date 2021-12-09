
namespace LitExplore.Core.Graph;

public interface IGraph<T,K>
{
    int NumberOfVertices();
    int NumberOfEdges();
    bool AddVertex(IVertex<T,K> vertex);//TO:DO overvje om input bare skal værre af tyben 'T,K'
    bool AddEdge(IVertex<T,K> from, IVertex<T,K> to);
    bool AddEdge(K fromId, K toId);
    IEnumerable<IVertex<T,K>> GetAdj(K Id);
    IEnumerable<IVertex<T,K>> GetAdj(IVertex<T,K> vertex);//TO:DO overvje om input bare skal værre af tyben 'T,K'
}
