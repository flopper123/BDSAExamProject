


namespace LitExplore.Interfaces
{
    public interface IFilter : IDable
    {
        public IGraph<T> Filter(IGraph<T> graph);
        public IFilterOptions GetOptions();
    }
}
