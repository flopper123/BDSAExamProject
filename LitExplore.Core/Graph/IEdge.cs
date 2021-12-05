namespace LitExplore.Core.Graph;

public interface IEdge<K,V>
{
    public IVertex<K,V> GetFrom();
    public IVertex<K,V> GetTo();
}
