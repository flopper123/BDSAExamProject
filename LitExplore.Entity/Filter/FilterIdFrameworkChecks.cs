namespace LitExplore.Entity.Filter;

using System.Reflection;

using static ReflectionUtil;
using static AppDomain;

// TO:DO Assert the checks run, and work as intended
public static class FilterIdFrameworkChecks {
    
    public static readonly string EXP_ID_VAR_NAME = "Id";
    public static readonly Type EXP_TYPE_OF_EID = typeof(EFilter);

    /// <summary> 
    /// Asserts that all classes that implement Filter<T> or FilterDecorator<T>
    /// have a public static var of type FilterReflectionChecks.EXP_TYPE_OF_EID 
    /// named FilterReflectionChecks.EXP_ID_VAR_NAME)
    /// </summary>
    /// <param name="assembly"> The assembly to search </param>
    /// <exception cref="System.Reflection.MissingFieldException"> 
    /// Thrown when a field named @EXP_ID_VAR_NAME is not found
    /// </exception>
    /// <exception cref="System.Reflection.FieldAccessException"> 
    /// Thrown when the field named @EXP_ID_VAR_NAME of type @EXP_TYPE_OF_EID
    /// is not static. 
    /// </exception>
    /// <exception cref="System.TypeAccessException"> 
    /// Thrown when type of the field named @EXP_ID_VAR_NAME is not @EXP_TYPE_OF_EID.
    /// </exception>
    public static void Assert(Assembly assembly) {
        AssertStaticIdVariable(assembly);
    }

    private static void AssertStaticIdVariable(Assembly assembly) {
        ReflectionUtil.StaticFieldAssertion(
            typeof(Filter<>),
            EXP_ID_VAR_NAME,
            EXP_TYPE_OF_EID,
            assembly
        );
        ReflectionUtil.StaticFieldAssertion(
            typeof(FilterDecorator<>),
            EXP_ID_VAR_NAME,
            EXP_TYPE_OF_EID,
            assembly
        );
    }   
}