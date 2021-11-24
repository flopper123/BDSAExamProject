using System.Collections.Generic;
using LitExplore.Persistence.Entities;

namespace LitExplore.Interfaces
{
    public class Graph : IGraph<Publication>
    {
        public static Graph CreateInstance(Publication root, int depth)
        {
            return new Graph{Root = new Vertex(root), Depth = depth};
        }

        public ICollection<IVertex<Publication>> Vertisies { get; set; } = new List<IVertex<Publication>>();
        public ICollection<IEdge<Publication>> Edges { get; set; } = new List<IEdge<Publication>>();
        public IVertex<Publication> Root { get; set; }
        public int Depth { get; set; }
        
        public bool AddVertex(IVertex<Publication> vertex)
        {
            throw new System.NotImplementedException();
        }

        public IEdge<Publication> AddEdge(IVertex<Publication> from, IVertex<Publication> to)
        {
            throw new System.NotImplementedException();
        }

        public IEdge<Publication> AddEdge((IVertex<Publication>, IVertex<Publication>) edge)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IVertex<Publication>> GetAdj(IVertex<Publication> vertex)
        {
            throw new System.NotImplementedException();
        }

        public IGraph<Publication> Build(Publication root, int depth)
        {
            throw new System.NotImplementedException();
        }

        public IGraph<IVertex<Publication>> Build(int depth)
        {
            return null;
        }
    }
}