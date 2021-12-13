namespace LitExplore.Core.Graph;

public class Graph<T> : IGraph<T>
    where T : IEquatable<T>
{
    ulong IGraph<T>.Size => throw new NotImplementedException();

    IList<ITree<T>> IGraph<T>.Roots { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    ulong IGraph<T>.Add(INode<T> v)
    {
        throw new NotImplementedException();
    }

    ulong IGraph<T>.Delete(T v)
    {
        throw new NotImplementedException();
    }

    IEnumerator<ITree<T>> IEnumerable<ITree<T>>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
