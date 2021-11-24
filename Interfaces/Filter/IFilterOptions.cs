using System.Collections.Generic;

namespace Interfaces;
public interface IFilterOptions : IDable
{
    public IList<IFilter> GetData();
    public IList<IFilter> GetTypes();
}
