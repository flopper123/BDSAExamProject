using System.Runtime.CompilerServices;

using System.Linq;
using Interfaces;
using LitExplore.Core;
using Microsoft.EntityFrameworkCore;

namespace LitExplore.Entity
{
    public class PublicationRepository : IPublicationRepository
    {
        private readonly ILitExploreContext _context;
        
        public PublicationRepository(ILitExploreContext context) => _context = context;

        // Add a new publication
        public async Task<PublicationDto?> CreateAsync(PublicationCreateDto publication)
        {
            var pub = new Publication
            {
                Title = publication.Title,
                Author = publication.Author,
                Edition = publication.Edition,
                Pages = publication.Pages,
                Publisher = publication.Publisher,
                Year = publication.Year,
                References = publication.References.Select( refDto => new Reference {Title = refDto.title })
                                                    .ToList<Reference>(),
                // should await references, but for now we just set it to empty GetReferencesAsync(publication.References).ToListAsync() // But Why thoug??
            };
            
            _context.Publications.Add(pub);
            await _context.SaveChangesAsync();

            return new PublicationDto(
                pub.Title,
                pub.Author,
                pub.Year,
                pub.Publisher,
                pub.Pages,
                pub.Edition,
                pub.References.Select(r => new ReferenceDto(r.Title)).ToHashSet()
            );
        }

        /// <summary>
        ///     Tries to asyncly retrieve a publication with the given parameter @pubtitle
        ///     from the database and returns it as a @PublicationDto.
        /// </summary>
        /// <param name="pubTitle"> Title/key of the publication </param>
        /// <returns> An task with the appropriate PublicationDto on success or  </returns>
        public async Task<PublicationDto?> ReadAsync(string pubTitle) // Not so async HMM??
        {
            var pub =  
                from p in _context.Publications
                where p.Title != null && p.Title.Equals(pubTitle) // why ? Title should not be null in the DB If it is Pub doesnt exists should return NotFound
                select new PublicationDto(
                             p.Title,
                             p.Author,
                             p.Year,
                             p.Publisher,
                             p.Pages,
                             p.Edition,
                             p.References.Select(r => new ReferenceDto(r.Title)).ToHashSet() // This fails because it needs to be fetched from _context.References
                );
            return await pub.FirstOrDefaultAsync();
        }

         // Read all publications async
        public async Task<IReadOnlyCollection<PublicationDto>> ReadAsync() // Not so async HMM??
        {
            throw new NotImplementedException();
        }

/*
        // input: Publication 
        // output: All references for the given publication as an async enumerable
        private async IAsyncEnumerable<ReferenceDto?> GetRefDtoAsync(Publication pub)
        {
            // search for matching reference in publication dbset
            Dictionary<string, Reference> refToPub =
                await _context.References.Where(r => pub.Title != null && pub.Title.Equals(r.Title))
                                         .ToDictionaryAsync(r => (r.Title == null));

            foreach (var reference in pub.References)
            {
                yield return refToPub.TryGetValue(reference.Title, out var r)
                    // TO:DO add try catch if null 
                    ? new ReferenceDto { Title = r.Title } : null;
            }
        }
*/
        // Modify existing publication
        public async Task<Status> UpdateAsync(PublicationUpdateDto publication)
        {
            throw new NotImplementedException();
        }

        public async Task<Status> DeleteAsync(string pubTitle)
        {
            throw new NotImplementedException();
        }

        /*
        private async IAsyncEnumerable<Reference> GetReferencesAsync(ISet<ReferenceDto> publicationReferences)
        {
            var existing = await _context.References.Select(r => r)
                .Where(r => publicationReferences.Any(tr => tr.Title == r.Title)).ToDictionaryAsync(r => r.Title);

            foreach (ReferenceDto referenceDto in publicationReferences)
            {
                yield return existing.TryGetValue(referenceDto.Title, out var r) ? r : new Reference { Title = referenceDto.Title }; // Might need to call create ref to context.. this will then create new references.
            }
        }
        */
    }
}