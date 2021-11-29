using Interfaces;
using LitExplore.Entity;
namespace Graph
{
    public class Graph : IGraph<Publication>
    {
        private List<IVertex<Publication>> Vertices;
        private IList<IEdge<Publication>> Edges;
        public Graph()
        {
            Vertices = new List<IVertex<Publication>>();
            Edges = new List<IEdge<Publication>>();
        }
        public bool AddVertex(IVertex<Publication> vertex)
        {
            if (vertex.Data.Title == "" ||
                vertex.Data.Title == null ||
                vertex.Id < Vertices.Count) return false;
            Vertices.Add(vertex);
            return true;
        }
        //TO:DO burde nok ikke bare retrune bool
        public bool AddEdge(IVertex<Publication> from, IVertex<Publication> to)
        {
            if (from.Equals(to)) return false;
            var _from = Vertices.FirstOrDefault(v => v.Data.Title == from.Data.Title);
            if (from == null) return false;
            else if (!AddVertex(from)) return false;
            else _from = from;
            var _to = Vertices.FirstOrDefault(v => v.Data.Title == to.Data.Title);
            if (to == null) return false;
            else if (!AddVertex(to)) return false;
            else _to = to;

            Edges.Add(new Edge(_from, _to));

            return true;
        }
        public bool AddEdge(int fromId, int toId)
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
        public int NumberOfVertices() =>
            Vertices.Count;
        public int NumberOfEdges() =>
            Edges.Count;
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
}