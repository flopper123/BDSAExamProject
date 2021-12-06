
namespace LitExplore.Core.Graph;
public interface IGraph<K, V>
{
    int NumberOfVertices();
    int NumberOfEdges();
    bool AddVertex(IVertex<K, V> vertex);
    bool AddEdge(IVertex<K, V> from, IVertex<K, V> to);
    bool AddEdge(K fromId, K toId);
    bool AddEdge(IVertex<K, V> from, K toId);
    bool AddEdge(K fromId, IVertex<K, V> to);
    bool AddEdge(IEdge<K, V> edge);
    IVertex<K, V> GetVertex(K Id);
    IEnumerable<IVertex<K, V>> GetAdj(K Id);
    IEnumerable<IVertex<K, V>> GetAdj(IVertex<K, V> vertex);
    IEnumerable<IVertex<K, V>> DegreesOfSeperation(IVertex<K, V> startVertex, int degree);
}
