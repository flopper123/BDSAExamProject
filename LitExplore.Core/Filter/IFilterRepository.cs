namespace LitExplore.Core.Filter;

// Maps users to filters.
// By converting a database representation to actually usable filters
public interface IFilterRepository<T>
{
    /// <summary>
    /// Reads Filter associated with the user.
    /// </summary>
    /// <param name="userId"> The ID of the user </param>
    /// <returns> 
    /// An applicable Filter of type T, which can be applied to an 
    /// Enumerable<T> 
    /// If the userId doesn't have a saved filter, the method returns an instance 
    /// of EmptyFilter<T>
    /// </returns>
    Task<Filter<T>> ReadAsync(UInt64 userId);


    /// <summary>
    /// Attempts to update the database such that the user with the given @userid,
    /// maps to the new filter.
    /// The update is destructive in the sense that it overrides previously set 
    /// filters for that user in the database.
    /// If there is no user history, a new userId with the given filter is added to the db.
    /// </summary>
    /// <param name="userId"> Key: The userId </param>
    /// <param name="filter"> Value: The value of the filter to map to the userId </param>
    /// <returns> A status indicating the resolve of the update </returns>
    Task<Status> UpdateAsync(UInt64 userId, Filter<T> filter);
}