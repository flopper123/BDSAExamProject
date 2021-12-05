namespace LitExplore.Entity.Filter;

public class FilterOptions<T> {
    Dictionary<uint, Predicate<T>> id_to_predicate = new Dictionary<uint, Predicate<T>>();

    Predicate<T> Get(uint id) {
        return id_to_predicate[id];
    }
}