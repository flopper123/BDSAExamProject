namespace Interfaces;
public interface IFilter : IDable
{
    public IGraph filter(IGraph graph);
    public IFilterOptions GetOptions();
}
