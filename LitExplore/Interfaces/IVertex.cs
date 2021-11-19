namespace LitExplore.Interfaces
{
    public interface IVertex<out T> : IDable
    {
        public T GetData();
    }

}
