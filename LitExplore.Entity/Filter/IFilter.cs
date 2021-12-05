namespace LitExplore.Entity.Filter;

public interface IFilter<T>
{
    FilterOption<T> GetOption();
    IEnumerable<T> Apply(IEnumerable<T> t);
}
