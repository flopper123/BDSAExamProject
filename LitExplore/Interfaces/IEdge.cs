
namespace LitExplore.Interfaces
{
    public interface IEdge<T>
    {
        public IVertex<T> From { get; set; }
        public IVertex<T> To { get; set; }
        //public IVertex<T> GetFrom();
        //public IVertex<T> GetTo();
    }
}
