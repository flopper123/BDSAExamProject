namespace Interfaces;
public interface IFilterFactory
{
    public IFilter Create(IFilterOptions options);
}
