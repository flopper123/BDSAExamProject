using System;
using System.Security.Principal;
using System.util;
using LitExplore.Interfaces;
using LitExplore.Persistence.Entities;

namespace LitExplore
{
    public class PublicationBroker : IBroker<IVertex<Publication>>
    {
        public IBroker<IVertex<Publication>> Broker { get; } = new PublicationBroker();

        public void Subscribe(Action<IVertex<Publication>> action)
        {
            throw new NotImplementedException();
        }

        public void Publish(IVertex<Publication> to)
        {
            throw new NotImplementedException();
        }
    }
}