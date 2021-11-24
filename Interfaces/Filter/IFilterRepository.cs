namespace Interfaces;
public interface IFilterRepository
{
    public IRepository<IFilterOptions> GetFilterOptions(); 
    public IRepository<IFilterHistory> GetFilterHistory();
}