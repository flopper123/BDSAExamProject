namespace LitExplore.Core.Graph;

public interface IVertex<T, K>
{
    public T Data { get; init; }
    public K Id { get; init; }
}
