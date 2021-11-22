


namespace LitExplore.Interfaces
{
    public interface IFilter : IDable
    {
        public IGraph<T> Filter<T>(ref IGraph<T> graph);
        public IFilterOptions GetOptions();
    }
}
