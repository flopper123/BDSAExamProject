

namespace LitExplore.Core.Graph;

public class PublicationEdge : IEdge<string, PublicationDto>
{
    private IVertex<string, PublicationDto> From;
    private IVertex<string, PublicationDto> To;

    public PublicationEdge(IVertex<string, PublicationDto> from,IVertex<string, PublicationDto> to)
    {
        From = from;
        To = to;
    }


    public IVertex<string, PublicationDto> GetFrom()
    {
        return From;
    }

    public IVertex<string, PublicationDto> GetTo()
    {
        return To;
    }
}