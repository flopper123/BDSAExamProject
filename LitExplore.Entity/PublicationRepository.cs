using Interfaces;
using LitExplore.Core;
using Microsoft.Extensions.Options;

namespace LitExplore.Entity;

public class PublicationRepository : IPublicationRepository
{

    private readonly ILitExploreContext _context;

    public PublicationRepository(ILitExploreContext context) => _context = context;
    public bool Create(PublicationCreateDto toCreate)
    {
        var publication = new Publication
        {
            Title = toCreate.Title,
            
            References = toCreate.References as HashSet<Reference>,
        };
        
        if (publication.Title != string.Empty)
        {
            return true;
        }
        else return false;
    }

    public bool Delete(int Id)
    {
        var entity = _context.Publications.Find(Id);
        
        Publication? ent = _context.Publications.Select(p => p).FirstOrDefault(f => f.Id == Id); // Why not this?

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

public interface IPublicationRepository
{
    Task<PublicationDto> CreateAsync(PublicationCreateDto publication);

    Task<IReadOnlyCollection<PublicationDto>> ReadAsync();
    Task<PublicationDto> ReadAsync(int publicationId); // Funky stuff with Option see Rasmus lecture #10 commit
    Task<Status> UpdateAsync(int id, PublicationUpdateDto publication);
    Task<Status> DeleteAsync(int publicationId);
}