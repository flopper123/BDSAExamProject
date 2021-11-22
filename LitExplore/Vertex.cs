using LitExplore.Interfaces;
using LitExplore.Persistence.Entities;

namespace LitExplore
{
    public class Vertex : IVertex<Publication>
    {
        public Publication Vert { get;}
        public Vertex(Publication publication)
        {
            Vert = publication;
        }

        public int GetId()
        {
           return Vert.Id; // Special? or should be specially hashet for the vert.?
        }
        

        public Publication GetData() // Could this not be a property instead?
        {
            return Vert;
        }
    }
}