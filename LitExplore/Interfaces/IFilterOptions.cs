using System.Collections;

namespace LitExplore.Interfaces
{
    public interface IFilterOptions : IDable
    {
        public IList GetData();
        public IList GetTypes();
    }
}
