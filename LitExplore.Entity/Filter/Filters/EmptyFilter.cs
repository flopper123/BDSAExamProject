namespace LitExplore.Entity.Filter;

<<<<<<< HEAD
using System.Reflection;

=======
>>>>>>> master
/// <summary>
/// The predicate of an empty filter is always true, hence it doesnt filter.. 
/// The class is constructed as a generic singleton,
/// so there is never any need to have multiple filters. Without the singleton instance
/// we need to implement equality such that two empty filters wont be equal.
/// </summary>
/// <typeparam name="T"> The type of object to filter </typeparam>
public class EmptyFilter<T> : Filter<T> 
{
    protected static EmptyFilter<T>? _this;

    protected EmptyFilter() : base(t => true) {}

<<<<<<< HEAD
    public override UInt32 Depth {
        get { return 0; }
    }

    public override EFilter GetId() {
        UInt64 id = EFilter.NONE | (UInt64) typeof(T).GetFilterType();
        try
        {
            return (EFilter)id;
            // if (UInt32) EFiler.None | (UInt32) typeof(T).GetFilterType()
        } catch (InvalidCastException ex) {
            string msg = $"Reflection Cast Exception: EFilter type not found for uint64:#${id}." +
                         "Probably an error in FilterEnum definitions";
            throw new InvalidCastException(msg, ex);
        }
=======
    public override EFilter GetId() {
        if (typeof(T) == typeof(PublicationDto)) return EFilter.PUB;
        else return EFilter.NONE;
>>>>>>> master
    }

    /// <summary>
    /// Returns a T singleton instance of empty filter. 
    /// </summary>
    /// <returns></returns>
    public static EmptyFilter<T> Get() {
        if (_this == null) {
            _this = new EmptyFilter<T>();
        }
        return _this;
    }
}