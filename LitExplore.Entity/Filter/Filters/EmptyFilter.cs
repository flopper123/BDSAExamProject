namespace LitExplore.Entity.Filter;

public class EmptyFilter<T> : Filter<T> 
{
    public EmptyFilter() : base(t => true) {}
}