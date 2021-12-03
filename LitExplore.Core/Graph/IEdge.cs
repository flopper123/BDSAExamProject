namespace LitExplore.Core.Graph;

public interface IEdge<T>
{
    public IVertex<T> GetFrom();
    public IVertex<T> GetTo();
}
