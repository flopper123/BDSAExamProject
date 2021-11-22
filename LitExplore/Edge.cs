using LitExplore.Interfaces;
using LitExplore.Persistence.Entities;

namespace LitExplore
{
    public class Edge : IEdge<Reference>
    {
        public IVertex<Reference> From { get; set; }
        public IVertex<Reference> To { get; set; }
    }
}