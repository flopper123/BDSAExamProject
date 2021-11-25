using Interfaces;

public class FilterRepository : IRepository<IFilter>
{

    public ICollection<IFilterOptions> FilterCollections { get; set; }
    private readonly ILitExploreContext _context;

    public FilterRepository(ILitExploreContext context) => _context = context;
    public bool Create(IFilter Created)
    {
        var filter = new Filter{
            sequence = Created.GetOptions().ToString(),
        };
        if (filter.sequence != "")
        {
            return true;
        }
        else return false;
    }

    public bool Delete(int Id)
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