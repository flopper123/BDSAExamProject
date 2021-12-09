namespace LitExplore.Entity.Filter;

using System.Reflection;
using System.Linq;

using static AppDomain;
using static FilterIdFrameworkChecks;
using static ReflectionUtil;

// TO:DO Reflection code to map ids of any filterdecoration to their constructor
public static class FilterFactory {

    private static Assembly _assembly = Assembly.Load("LitExplore.Entity");

    // TO:DO Unsure if it should be enumid to filter constructor instead
    private static Dictionary<EFilter, Type> eid_to_type = new Dictionary<EFilter, Type>();

    public static Dictionary<EFilter, Type> GetMap() {
        return eid_to_type;
    }

    /// <summary>
    /// Generate EID_TO_TYPE by reflection on program start..
    /// </summary>
    static FilterFactory() {
        
        InitMapByReflection();
        // Add generic types manually
        eid_to_type.Add(EFilter.PUB, typeof(EmptyFilter<PublicationDto>));

    }

    static public IEnumerable<Type> GetConcreteFilters(Assembly assembly)
    {
        foreach (Type t in GetAllConcreteTypes(typeof(Filter<>), assembly)) {
            yield return t;
        }    
        foreach (Type t in GetAllConcreteTypes(typeof(FilterDecorator<>), assembly)) {
            yield return t;
        }    
    }

    private static void InitMapByReflection() {
        
        IEnumerable<Type> types = FilterFactory.GetConcreteFilters(_assembly);
        
        foreach(Type type in types) {
            FieldInfo? field = type.GetField(EXP_ID_VAR_NAME, BindingFlags.Public | BindingFlags.Static);

            if (field == null) {

                string err_msg = $"ReflectionException for type ({type}):\n\t";
                err_msg += $"Missing declaration of field:\n\t\t";
                err_msg += $"Expected \"{EXP_ID_VAR_NAME}\"@typeof({EXP_TYPE_OF_EID}) on typeof({type})\n" ;
                throw new MissingFieldException(err_msg);
            }

            EFilter? id_tmp = (EFilter?) field.GetValue(null);
            if (id_tmp == null)
            {                
                string err_msg = $"ReflectionException for type ({type}):\n\t";
                err_msg += $"Failed retrieval of {EXP_ID_VAR_NAME} as UInt64:\n\t\t";
                err_msg += $"Expected \"{EXP_ID_VAR_NAME}\"@typeof({EXP_TYPE_OF_EID} on typeof({type})\n" ;
                throw new TargetException(err_msg);   
            }

            // Should never throw cast exception
            EFilter eid = (EFilter) id_tmp;
            eid_to_type.Add(eid, type);
        }
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
        return FilterFactory.eid_to_type[(EFilter) eid];
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
        return FilterFactory.eid_to_type[eid];
    }
}