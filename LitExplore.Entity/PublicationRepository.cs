
using System.Linq;
using Interfaces;
using LitExplore.Core;
using Microsoft.EntityFrameworkCore;

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

    public async Task<PublicationDto> CreateAsync(PublicationCreateDto publication)
    {
        var toCreate = new Publication
        {
            Title = publication.Title,
            Author = publication.Author,
            Edition = publication.Edition,
            Pages = publication.Pages,
            Publisher = publication.Publisher,
            Year = publication.Year,
            References = await GetReferencesAsync(publication.References).ToListAsync()
        };

        _context.Publications.Add(toCreate);
        await _context.SaveChangesAsync();

        return new PublicationDto(
            toCreate.Id,
            toCreate.Title,
            toCreate.Author,
            toCreate.Year,
            toCreate.Type,
            toCreate.Publisher,
            toCreate.Pages,
            toCreate.Edition,
            toCreate.References.Select(p => new ReferenceDto
            {
                Id = p.Id,
                Title = p.Title
            }).ToHashSet()
            );
    }

    public async Task<IReadOnlyCollection<PublicationDto>> ReadAsync() // Not so async HMM??
    {
         _context.Publications
            .Select(async p =>
                new PublicationDto(

                    p.Id, p.Title, p.Author, p.Year,
                    p.Type, p.Publisher, p.Pages, p.Edition,
                    await GetRefDtoAsync(p).ToHashSetAsync()
                )
            ).ToList().AsReadOnly();
    }

    private async IAsyncEnumerable<ReferenceDto> GetRefDtoAsync(Publication pub)
    {
        foreach (var reference in pub.References)
        {
            yield return new ReferenceDto {Id = reference.Id, Title = reference.Title};
        }
    }


    public async Task<PublicationDto> ReadAsync(int publicationId)
    {
        throw new NotImplementedException();
    }

    public async Task<Status> UpdateAsync(int id, PublicationUpdateDto publication)
    {
        throw new NotImplementedException();
    }

    public async Task<Status> DeleteAsync(int publicationId)
    {
        throw new NotImplementedException();
    }
    
    private async IAsyncEnumerable<Reference> GetReferencesAsync(ISet<ReferenceDto> publicationReferences)
    {
        var existing = await _context.References.Select(r => r)
            .Where(r => publicationReferences.Any(tr => tr.Title == r.Title)).ToDictionaryAsync(r => r.Title);
        
        foreach (ReferenceDto referenceDto in publicationReferences)
        {
            yield return existing.TryGetValue(referenceDto.Title, out var r) ? r : new Reference {Title = referenceDto.Title}; // Might need to call create ref to context.. this will then create new references.
        }
         
    }
}