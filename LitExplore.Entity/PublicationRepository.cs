using Interfaces;

namespace LitExplore.Entity;

public class PublicationRepository : IRepository<IPublication>
{

    private readonly ILitExploreContext _context;

    public PublicationRepository(ILitExploreContext context) => _context = context;
    public bool Create(IPublication Created)
    {
        var publication = new Publication
        {
            Title = Created.Title,
            References = Created.GetRefs().ToList(),
        };
        if (publication.Title != "")
        {
            return true;
        }
        else return false;
    }

    public bool Delete(int Id)
    {
        var entity = _context.Publications.Find(Id);

        if (entity == null)
        {
            return false;
        }

        _context.Publications.Remove(entity);
        _context.SaveChanges();

        return true;
    }
    public IPublication Read(int Id)
    {
        throw new NotImplementedException();
    }

    public bool Update(IPublication Updated)
    {
        throw new NotImplementedException();
    }
}