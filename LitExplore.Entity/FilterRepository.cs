using Interfaces;

namespace LitExplore.Entity;

public class FilterRepository : IRepository<IFilter>
{

    public ICollection<IFilterOptions> FilterCollections { get; set; }

    public bool Create(IFilter Created)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public IFilter Read(int Id)
    {
        throw new NotImplementedException();
    }

    public bool Update(IFilter Updated)
    {
        throw new NotImplementedException();
    }
}