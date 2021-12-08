namespace LitExplore.Entity.Filter;

using System.Reflection;
using System.Linq;

using static AppDomain;
using static ReflectionUtil;

// TO:DO Reflection code to map ids of any filterdecoration to their constructor
public static class FilterMap {
    // TO:DO Unsure if it should be enumid to filter constructor instead
    private static Dictionary<EFilter, Type> eid_to_type = new Dictionary<EFilter, Type>();

    /// <summary>
    /// Generate EID_TO_TYPE på reflection on program start..
    /// </summary>
    static FilterMap() {
        InitMapByReflection();
    }

    static public IEnumerable<Type> GetConcreteFilters()
    {
        foreach (Assembly assembly in CurrentDomain.GetAssemblies())
        {
            foreach (Type t in GetAllConcreteTypes(typeof(Filter<>), assembly)) {
                yield return t;
            }    
            foreach (Type t in GetAllConcreteTypes(typeof(FilterDecorator<>), assembly)) {
                yield return t;
            }    
        }
    }

    private static void InitMapByReflection() {
        var types = FilterMap.GetConcreteFilters();

        // Call static getID on all filters


        // TO:DO Change to Reflection code to find all concrete implementing classes of Filter, 
        // and map their GetId to Filter. 
    }

    /// <summary>
    /// Retrieves the type of filter corresponding to the given enum id
    /// </summary>
    /// <param name="eid"> An UInt64 EFilter id </param>
    /// <returns> A Type implementing Filter<T> that corresponds to the given id </returns>
    /// <exception cref="System.InvalidCastException"> Thrown when input @eid isn't a valid EFilter value. </exception>
    /// <exception cref="System.KeyNotFoundException"> 
    /// If an EFilter without a mapping is requested. 
    /// ! Note this indicates a syntax error with the given EFilter id, as it should already 
    /// ! be automaticly added by reflection.
    /// </exception>
    public static Type Get(UInt64 eid) {
        return FilterMap.eid_to_type[(EFilter) eid];
    }

    /// <summary>
    /// Retrieves the type of filter corresponding to the given enum id
    /// </summary>
    /// <param name="eid"> An UInt64 EFilter id </param>
    /// <returns> A Type implementing Filter<T> that corresponds to the given id </returns>
    /// <exception cref="System.KeyNotFoundException"> 
    /// If an EFilter without a mapping is requested. 
    /// ! Note this indicates a syntax error with the given EFilter id, as it should already 
    /// ! be automaticly added by reflection.
    /// </exception>
    public static Type Get(EFilter eid) {
        return FilterMap.eid_to_type[eid];
    }
}