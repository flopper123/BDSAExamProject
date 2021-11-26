namespace LitExplore.Core
{
    public interface IPublicationRepository
    {
        Task<PublicationDto> CreateAsync(PublicationCreateDto publication);
        Task<IReadOnlyCollection<PublicationDto>> ReadAsync();
        Task<PublicationDto> ReadAsync(string pubTitle); // Funky stuff with Option see Rasmus lecture #10 commit
        Task<Status> UpdateAsync(PublicationUpdateDto publication);
        Task<Status> DeleteAsync(string pubTitle);
    }
}