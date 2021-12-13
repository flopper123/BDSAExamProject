namespace LitExplore.Core.Publication;

public interface IPublicationRepository
{
    Task<PublicationDetails?> CreateAsync(PublicationDetails publication);
    Task<IReadOnlyCollection<PublicationDetails>> ReadAsync();
    Task<PublicationDetails?> ReadAsync(string pubTitle); // Funky stuff with Option see Rasmus lecture #10 commit
    Task<Status> UpdateAsync(PublicationDetails publication);
    Task<Status> DeleteAsync(string pubTitle);
}