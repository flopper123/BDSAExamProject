

namespace LitExplore.Core.Graph;

public class Edge : IEdge<Publication>
{
    private IVertex<Publication> From;
    private IVertex<Publication> To;

    public Edge(IVertex<Publication> from, IVertex<Publication> to)
    {
        From = from;
        To = to;
    }
    public IVertex<Publication> GetFrom()
    {
        return From;
    }

    public IVertex<Publication> GetTo()
    {
        return To;
    }
}