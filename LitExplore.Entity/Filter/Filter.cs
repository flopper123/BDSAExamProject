namespace LitExplore.Entity.Filter;

interface IFilter<T>
{
    public IEnumerable<T> Apply(IEnumerable<T> t);
}

public abstract Filter<T> : IFilter<T> {
    protected Predicate<T> predicate;

    public Filter(Predicate<T> predicate) {
        this.predicate = predicate;
    }

    /// <summary>
    /// Applies the predicate to the input @tar, and returns
    /// a subset of @tar. All elements in the returned subset,
    /// upholds the filter predicate. 
    /// </summary>
    /// <param name="tar"> Target of filter </param>
    /// <returns> An enumerable of all elements that uphold src predicate </returns>
    public abstract IEnumerable<T> Apply(IEnumerable<T> tar);
}

public abstract FilterDecoration<T> : Filter<T> {
    protected Filter<T> prv;

    public override virtual IEnumerable<T> Apply(IEnumerable<T> )
}
