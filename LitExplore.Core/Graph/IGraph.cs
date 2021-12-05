
namespace LitExplore.Core.Graph;

public interface IGraph<K,V>
{
    int NumberOfVertices();
    int NumberOfEdges();
    bool AddVertex(IVertex<K,V> vertex);//TODO: overvje om input bare skal værre af tyben 'K,V'
    bool AddEdge(IVertex<K,V> from, IVertex<K,V> to);
    bool AddEdge(K fromId, K toId);
    IEnumerable<IVertex<K,V>> GetAdj(K Id);
    IEnumerable<IVertex<K,V>> GetAdj(IVertex<K,V> vertex);//TODO: overvje om input bare skal værre af tyben 'K,V'
}
