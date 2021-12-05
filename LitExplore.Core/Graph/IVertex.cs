namespace LitExplore.Core.Graph;

public interface IVertex<K,V>
{
    public V Data { get; init; }
    public K Id { get; init; }
}
