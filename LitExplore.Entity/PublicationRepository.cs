using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LitExplore.Entity;

public class PublicationRepository : AbsRepository, IPublicationRepository
{  
    public PublicationRepository(ILitExploreContext ctx) : base(ctx) {}

    /// <summary>
    /// Creates and inserts new publication and its references into the DB
    /// </summary>
    /// <param ="publication"></param>
    /// <returns>name"PublicationDto"</returns>
    public async Task<PublicationDto?> CreateAsync(PublicationCreateDto publication) // Should never return null..
    {
        var pub = new Publication
        {
            Title = publication.Title!,
            References = publication.References.Select(refDto => new Reference {Title = refDto.Title})
                .ToList(),
            // should await references, but for now we just set it to empty GetReferencesAsync(publication.References).ToListAsync() // But Why though??
        };

        _context.Publications.Add(pub);
        await _context.SaveChangesAsync();
        
        return new PublicationDto
        {
            Title = pub.Title,
            References = pub.References.Select(r => new ReferenceDto {Title = r.Title}).ToHashSet()
        };
    }

    /// <summary>
    ///     Tries to asynchronously retrieve a publication with the given parameter @pubtitle
    ///     from the database and returns it as a @PublicationDto.
    /// </summary>
    /// <param name="pubTitle"> Title/key of the publication </param>
    /// <returns> An task with the appropriate PublicationDto on success or  </returns>
    public async Task<PublicationDto?> ReadAsync(string pubTitle) // Not so async HMM??
    {
        var pub =
            from p in _context.Publications
            where  p.Title.Equals(pubTitle) // why ? Title should not be null in the DB If it is Pub doesnt exists should return NotFound
            select new PublicationDto
            {
                Title = p.Title,
                References = p.References.Select(r => new ReferenceDto
                {
                    Title = r.Title
                }).ToHashSet() // This fails because it needs to be fetched from _context.References
            };
            
        return await pub.FirstOrDefaultAsync(); // should return 404--or-equivalent if not found.
    }

    // Read all publications async
    public async Task<IReadOnlyCollection<PublicationDto>> ReadAsync() // Not so async HMM??
    {
        var select = await _context.Publications.Select(p => new PublicationDto
        {
            Title = p.Title,
            References = p.References
                          .Select(r => new ReferenceDto { Title = r.Title })
                          .ToHashSet()})
                          .ToListAsync();

        return select.AsReadOnly();
    }


    // input: Publication 
    // output: All references for the given publication as an async enumerable
    private async IAsyncEnumerable<ReferenceDto> GetRefDtoAsync(Publication pub) // This might actually be a way to get references async. such that we can await in the constrution.
    {
        // search for matching reference in publication dbset
        Dictionary<string, Reference> refToPub =
            await _context.References.Where(r => pub.Title.Equals(r.Title))
                .ToDictionaryAsync(r => (r.Title));

        foreach (var reference in pub.References)
        {
            yield return refToPub.TryGetValue(reference.Title, out var r)
                // TO:DO add try catch if null -- // If this was null it means Reference doesnt exists in the DB, We need to make it.
                ? new ReferenceDto {Title = r.Title}
                : AddMissingReferenceToDbAndReturnDto(reference.Title);
        }
    }

    private ReferenceDto AddMissingReferenceToDbAndReturnDto(string referenceTitle)
    {
        //Should make sure that It Already doesnt exists.
        //** code TO:DO **//

        Reference reference = new Reference
        {
            Title = referenceTitle
        };

        _context.References.Add(reference);
        _context.SaveChangesAsync();

        return new ReferenceDto {Title = referenceTitle};
    }

    // Modify existing publication
    public async Task<Status> UpdateAsync(PublicationUpdateDto publication)
    {
        Publication? toUpdate = await _context.Publications.FindAsync(publication.Title);

        if (toUpdate is null)
        {
            return Status.NotFound;
        } 
        // no variables to update in current version
        var res = await _context.SaveChangesAsync();
        return Status.Updated; // TO:DO Handle Errors. Handled one already :D
    }

    public async Task<Status> DeleteAsync(string pubTitle)
    {
        throw new NotImplementedException();
    }

    public override void Dispose() { }
}