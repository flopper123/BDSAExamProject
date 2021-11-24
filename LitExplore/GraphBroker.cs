using System;
using LitExplore.Interfaces;
using LitExplore.Persistence.Entities;

namespace LitExplore
{
    public class GraphBroker : IBroker<IGraph<IVertex<Publication>>> 
    {
        public void Subscribe(Action<IGraph<IVertex<Publication>>> action)
        {
            throw new NotImplementedException();
        }

        public void Publish(IGraph<IVertex<Publication>> to)
        {
            throw new NotImplementedException();
        }
    }
}