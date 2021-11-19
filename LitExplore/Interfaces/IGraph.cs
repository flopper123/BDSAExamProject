using System.Collections.Generic;

namespace LitExplore.Interfaces
{
    public interface IGraph<T>
    {
        public bool AddVertex(IVertex<T> vertex);
        public IEdge<T> AddEdge(IVertex<T> from, IVertex<T> to);
        public IEdge<T> AddEdge((IVertex<T>, IVertex<T>) edge);
        public IEnumerable<IVertex<T>> GetAdj(IVertex<T> vertex);
    }
}


