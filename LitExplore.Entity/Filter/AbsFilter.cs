namespace LitExplore.Entity.Filter;

using System.Reflection;

/// <summary>
/// ! Warning All concrete implementing classes are expected to have a static variable named 
/// !         FilterFrameworkChecks.EXP_ID_VAR_NAME of type FilterFrameworkChecks.EXP_TYPE_OF_EID 
/// !         staticly available
/// </summary>
public abstract class Filter<T> {
    // The top-level predicate this filter applies.
    protected Predicate<T> predicate;

    static Filter() {
        FilterIdFrameworkChecks.Assert(Assembly.Load("LitExplore.Entity"));
    }

    // How many filters this filter applies
    public virtual UInt32 Depth {
        get { return 1; }
        protected set {}
    }

    public Filter(Predicate<T> predicate) {
        this.predicate = predicate;
    }

    /// <summary>
    /// Return the identifaction key for the given filter as an EFilter.
    /// Should simply return the required stati implementation.
    /// This is only here to ensure that the user notices the requirement 
    /// for the ID framework. 
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

    override public string ToString() {
        return $"Filter #{GetId().ToString()}:{GetId()} depth@{Depth}";
    }

}