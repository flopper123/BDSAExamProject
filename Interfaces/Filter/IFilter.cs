namespace Interfaces;
public interface IFilter : IDable
{
    public IGraph<IFilter> filter(IGraph<IFilter> graph);
    public IFilterOptions GetOptions();
}
