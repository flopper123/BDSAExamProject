namespace Interfaces;
public interface IVertex<T> : IDable
{
    public T Data { get; init; }
}
