
namespace LitExplore.Core.Graph;

public class Vertex : IVertex<Publication>
{

    public int Id { get; init; }
    public Publication Data { get; init; }

    public Vertex(int Id, Publication Data)
    {
        this.Id = Id;
        this.Data = Data;
    }
}