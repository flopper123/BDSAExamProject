namespace LitExplore.Entity.Repositories;

using LitExplore.Core.Filter;

/// <summary>
/// FilterRepository
/// </summary>
public class FilterRepository<T> : AbsRepository, IFilterRepository<T>
{
    
    public FilterRepository(ILitExploreContext ctx) : base(ctx) {}


    /// <summary>
    /// Attempts to async read the fiter saved for the given user@userid
    /// - If read was succesful it returns an instance of the filter mapped
    ///   to the userId,
    /// - If read fails, it returns a Task containing <null>
    /// </summary>
    /// <param name="userId"> The userId of the filter to retrieve </param>
    public async Task<Filter<T>?> ReadAsync(ulong userId)
    {
        UserFilter? uid_filter = (await _context.History.FindAsync(userId));
        return (uid_filter != null) ? FilterFactory.Deserialize<T>(uid_filter.Serialization) : 
                                      null;
    }

    /// <summary>
    /// Updates the context such that UserId points to Filter
    /// - If another entry already exists for the given user
    ///   it will overwrite the saved filter by replcaing it 
    ///   with @filter and return Status.UPDATED
    /// 
    /// - If no entry exists for the user, it will add the new 
    ///   entry and return Status.CREATED
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<Status> UpdateAsync(ulong userId, Filter<T> filter)
    {
        string fs = filter.Serialize();
        UserFilter? uid_filter = (await _context.History.FindAsync(userId));

        Status status = Status.Updated;

        if (uid_filter != null) uid_filter.Serialization = fs;
        else {
            await _context.History.AddAsync(
                new UserFilter { UserId = userId, Serialization = fs }
            );
            status = Status.Created;
        }

        await _context.SaveChangesAsync();
        
        return status; 
    }

    public override void Dispose() { }
}