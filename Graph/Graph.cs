using Interfaces;
using LitExplore.Entity;
namespace Graph
{
    public class Graph : IGraph<Publication>
    {
        private List<Vertex>? Vertices;
        private List<Edge>? Edges;
        public bool AddVertex(IVertex<Publication> vertex)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<IVertex<Publication>> GetAdj(int Id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<IVertex<Publication>> GetAdj(IVertex<Publication> vertex)
        {
            throw new NotImplementedException();
        }
        public bool AddEdge(IVertex<Publication> from, IVertex<Publication> to)
        {
            throw new NotImplementedException();
        }
        public bool AddEdge(int fromId, int toId)
        {
            throw new NotImplementedException();
        }
        public int NumberOfVertices() =>
            throw new NotImplementedException();
        public int NumberOfEdges() =>
            throw new NotImplementedException();
    }
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
    public class Edge : IEdge<Publication>
    {
        public IVertex<Publication> GetFrom()
        {
            throw new NotImplementedException();
        }

        public IVertex<Publication> GetTo()
        {
            throw new NotImplementedException();
        }
    }
}