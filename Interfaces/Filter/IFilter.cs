using LitExplore.Core.Graph;
namespace Interfaces;
public interface IFilter : IDable
{
    public IGraph<IFilter,string> filter(IGraph<IFilter,string> graph);
    public IFilterOptions GetOptions();
}
