
namespace LitExplore.Core.Graph;

public class Vertex : IVertex<PublicationDto, string>
{

    public PublicationDto Data { get; init; }
    public string Id { get; init; }

    public Vertex(string Id, PublicationDto Data)
    {
        this.Id = Id;
        this.Data = Data;
    }
}