namespace LitExplore.Core.Filter;

using System.Reflection;

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
    
    public override UInt32 Depth {
        get { return 0; }
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