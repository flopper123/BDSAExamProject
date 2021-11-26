
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
                    //Id = p.Id,
                    Title = p.Title
                }).ToHashSet()
            );
        }

        // Read all publications async
        public async Task<IReadOnlyCollection<PublicationDto>> ReadAsync() // Not so async HMM??
        {
            /*_context.Publications
               .Select(async p =>
                   new PublicationDto(

                       p.Id, p.Title, p.Author, p.Year,
                       p.Type, p.Publisher, p.Pages, p.Edition,
                       await GetRefDtoAsync(p).ToHashSetAsync()
                   )
               ).ToList().AsReadOnly();*/

            throw new NotImplementedException();
        }
        public async Task<PublicationDto> ReadAsync(string pubTitle) // Not so async HMM??
        {
            throw new NotImplementedException();
        }

        // input: Publication 
        // output: All references for the given publication as an async enumerable
        private async IAsyncEnumerable<ReferenceDto?> GetRefDtoAsync(Publication pub)
        {
            // search for matching reference in publication dbset
            Dictionary<string, Reference> refToPub =
                await _context.References.Where(r => pub.Title.Equals(r.Title))
                                         .ToDictionaryAsync(r => r.Title);

            foreach (var reference in pub.References)
            {
                yield return refToPub.TryGetValue(reference.Title, out var r)
                    // TO:DO add try catch if null 
                    ? new ReferenceDto { Title = r.Title } : null;
            }
        }

        // Modify existing publication
        public async Task<Status> UpdateAsync(string pubTitle, PublicationUpdateDto publication)
        {
            throw new NotImplementedException();
        }

        public async Task<Status> DeleteAsync(string pubTitle)
        {
            throw new NotImplementedException();
        }

        private async IAsyncEnumerable<Reference> GetReferencesAsync(ISet<ReferenceDto> publicationReferences)
        {
            var existing = await _context.References.Select(r => r)
                .Where(r => publicationReferences.Any(tr => tr.Title == r.Title)).ToDictionaryAsync(r => r.Title);

            foreach (ReferenceDto referenceDto in publicationReferences)
            {
                yield return existing.TryGetValue(referenceDto.Title, out var r) ? r : new Reference { Title = referenceDto.Title }; // Might need to call create ref to context.. this will then create new references.
            }
        }
    }
}