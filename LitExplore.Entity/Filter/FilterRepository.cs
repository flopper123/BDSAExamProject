namespace LitExplore.Entity.Filter;

using System.Threading.Tasks;
using LitExplore.Core.Filter;

/// <summary>
/// FilterRepository
/// </summary>
public class FilterRepository<T> : AbsRepository, IFilterRepository<T>
{
    
    public FilterRepository(ILitExploreContext ctx) : base(ctx) {}

    public override void Dispose() { }

    public Task<Filter<T>> ReadAsync(ulong userId)
    {
        throw new NotImplementedException();
    }

    public Task<Status> UpdateAsync(ulong userId, Filter<T> filter)
    {
        throw new NotImplementedException();
    }
}