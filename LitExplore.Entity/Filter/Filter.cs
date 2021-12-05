namespace LitExplore.Entity.Filter;

public abstract class IFilterOption<T> {
    Predicate<T>();
}

public abstract Filter<T> : IFilter<T> {
    protected Predicate<T> predicate;

    public Filter(Predicate<T> predicate) {
        this.predicate = predicate;
    }

/// <summary>
/// Return an ordered ienumerable where the starting element is the first applied 
/// filter in this sequence of filters.
/// </summary>
/// <returns></returns>
public abstract IEnumerable<Filter<T>> GetHistory();

/// <summary>
/// Applies the predicate to the input @tar, and returns
/// a subset of @tar. All elements in the returned subset,
/// upholds the filter predicate. 
/// </summary>
/// <param name="tar"> Target of filter </param>
/// <returns> An enumerable of all elements that uphold src predicate </returns>
public abstract IEnumerable<T> Apply(IEnumerable<T> tar);
}

public EmptyFilter : Filter<T> 
{

}



static Map<uint64, FilterOption> id_to_foption {

}

public abstract FilterDecoration<T> : Filter<T> {
    protected Filter<T> prv;

    public FilterDecoration<T>(Predicate<t> predicate, Filter<T> prv) : base(Predicate) {
        prv = prv;
    }

    public override virtual IEnumerable<T> Apply(IEnumerable<T> tar) {
        return base.Apply(prv.Apply(tar))
    }
}


// Filter builder..
public class Filter {
    Filter<Publication> New(FilterOption opt) {
        ...
    } 
}


Filter.New(SearchFilter()).New
Filter.New())


