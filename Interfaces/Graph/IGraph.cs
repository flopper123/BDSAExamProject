namespace Interfaces;
public interface IGraph
{
    public bool AddVertex(IVertex vertex);
    public IEdge AddEdge(IVertex from, IVertex to);
    public IEnumerable<IVertex> GetAdj(IVertex vertex);
}
