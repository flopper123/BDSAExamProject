namespace LitExplore.Entity.Filter;

using System.Reflection;

using static ReflectionUtil;
using static AppDomain;

// TO:DO Assert the checks run, and work as intended
static class FilterIdFrameworkChecks {
    public static readonly string EXP_ID_VAR_NAME = "Id";
    public static readonly Type EXP_TYPE_OF_EID = typeof(EFilter);

    // Run filter reflection checks on start up through Filter static constructor
    public static void Assert() {
        AssertStaticIdVariable();
    }

    /// <summary> 
    /// Asserts that all classes that implement Filter<T> or FilterDecorator<T>
    /// have a public static var of type FilterReflectionChecks.EXP_TYPE_OF_EID 
    /// named FilterReflectionChecks.EXP_ID_VAR_NAME)
    /// <summary/>
    /// <exception cref="System.Reflection.MissingFieldException"> 
    /// Thrown when a static field named @EXP_ID_VAR_NAME of type @EXP_TYPE_OF_EID 
    /// is not found on concrete classes of the framework. 
    /// </exception>
    private static void AssertStaticIdVariable() {
        foreach(Assembly a in CurrentDomain.GetAssemblies()) {
            foreach(Type t in FilterMap.GetConcreteFilters()) {
                ReflectionUtil.StaticFieldAssertion(
                    t,
                    EXP_ID_VAR_NAME,
                    EXP_TYPE_OF_EID,
                    a
                );
            }   
        }
    }    


}