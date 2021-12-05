
namespace LitExplore.Core.Graph;

public class PublicationVertex : IVertex<string, PublicationDto>
{
    public PublicationDto Data { get; init; }
    public string Id { get; init; }


    public PublicationVertex(string Id, PublicationDto Data)
    {
        this.Id = Id;
        this.Data = Data;
    }

}