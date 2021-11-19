
using System.Collections.Generic;

namespace LitExplore.Interfaces
{
    public interface IFilterHistory : IDable
    {
        public IFilter Pop();
        public bool Push(IFilter filter);
        public Stack<IFilter> Reset();
        public IFilter Peek();
        public IEnumerable<IFilter> ToArray();
        
        
    }
}