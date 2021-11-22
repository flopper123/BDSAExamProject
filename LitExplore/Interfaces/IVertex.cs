namespace LitExplore.Interfaces
{
    public interface IVertex<T> : IDable
    {
        public T Vert { get;}
        public T GetData();
    }

}
