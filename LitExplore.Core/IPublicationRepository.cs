namespace LitExplore.Core;

public interface IPublicationRepository
{
    Task<PublicationDto> CreateAsync(PublicationCreateDto publication);

    Task<IReadOnlyCollection<PublicationDto>> ReadAsync();
    Task<PublicationDto> ReadAsync(int publicationId); // Funky stuff with Option see Rasmus lecture #10 commit
    Task<Status> UpdateAsync(int id, PublicationUpdateDto publication);
    Task<Status> DeleteAsync(int publicationId);
}