namespace LitExplore.Entity.Filter;

public abstract class FilterDecorator<T> : Filter<T> {
    protected Filter<T> prv;
    
    // If prv is null, this is the last filter in the chain
    public FilterDecorator(Predicate<T> p, Filter<T>? _prv = null) : base(p) {
        prv = _prv ?? new EmptyFilter<T>();
    }

    /*
    public virtual IEnumerable<Filter<T>> GetHistory() {
        yield return base.GetHistory();
        yield return prv.GetHistory();
    }
    */

    public override IEnumerable<T> Apply(IEnumerable<T> tar) {
        return base.Apply(prv.Apply(tar));
    }
}