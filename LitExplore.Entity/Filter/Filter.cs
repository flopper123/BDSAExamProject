namespace LitExplore.Entity.Filter;

public abstract class Filter<T> {
    // The top-level predicate this filter applies.
    protected Predicate<T> predicate;

    public Filter(Predicate<T> predicate) {
        this.predicate = predicate;
    }

    /// <summary>
    /// Return the identifaction key for the given filter as an EFilter
    /// </summary>
    public abstract EFilter GetId();

    /// <summary>
    /// Return an ordered ienumerable where the starting element is the first applied 
    /// filter in this sequence of filters.
    /// </summary>
    /// <returns> The filter history as an generic IEnumerable<Filter<T>> </returns>
    public virtual IEnumerable<Filter<T>> GetHistory() {
        yield return this;
    }
    
    /// <summary>
    /// Applies the predicate to the input @tar, and returns
    /// a subset of @tar. All elements in the returned subset,
    /// upholds the filter predicate. 
    /// </summary>
    /// <param name="tar"> Target of filter </param>
    /// <returns> An enumerable of all elements that uphold src predicate </returns>
    public virtual IEnumerable<T> Apply(IEnumerable<T> tar) {
        return tar.Where(v => predicate(v));
    }
}