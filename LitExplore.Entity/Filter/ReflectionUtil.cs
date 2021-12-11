// TO:DO init project file UTIL
namespace LitExplore.Entity.Filter;

using System;
using System.Reflection;

using static System.Console;

public static class ReflectionUtil
{
    /// <summary>
    /// Returns all non-generic/abstract types that implement the input open generic type in the current scope 
    /// of the parameter assembly.
    /// Abstract types and interfaces are excluded.
    /// </summary>
    /// <param name="gType"> An open generic type </param>
    /// <param name="assembly"> The assembly to search </param>
    public static IEnumerable<Type> GetAllConcreteTypes(Type gType, Assembly assembly)
    {
        return (
                from t in assembly.GetTypes()
                where
                    !t.IsGenericType &&
                    !t.IsAbstract && 
                    (t.BaseType != null) &&
                    t.BaseType.IsGenericType &&
                    gType.IsAssignableFrom(t.BaseType.GetGenericTypeDefinition())
                select t
        );
    }

    /// <summary>
    /// Asserts that all concrete types implementing src@type
    /// in the tar@assembly has needle@type pubicly and staticly available. 
    /// Throws an exception if assertion fails.
    /// </summary>
    /// <param name="src"> A generic type. The assertion checks concrete implementing classes of @src.</param>
    /// <param name="exp_name"> The name of the expected static variable </param>
    /// <param name="exp_type"> The type of the expected static variable named param @exp_name </param>
    /// <param name="tar"> The assembly to search
    /// <exception cref="System.Reflection.MissingFieldException"> 
    /// Thrown when a field named @exp_name is not found
    /// </exception>
    /// <exception cref="System.Reflection.FieldAccessException"> 
    /// Thrown when the field named @exp_name of type @exp_type
    /// is not static. 
    /// </exception>
    /// <exception cref="System.TypeAccessException"> 
    /// Thrown when type of the field named @exp_name is not @exp_type.
    /// </exception>
    public static void StaticFieldAssertion(Type src, string exp_name, Type exp_type, Assembly tar) {
        string err_msg = $"\nReflectionException: \nFailed assertion of availablity for typeof({src}):\n\t\t";

        foreach(Type t in ReflectionUtil.GetAllConcreteTypes(src, tar)) {

            FieldInfo? field = t.GetField(exp_name);
    
            if (field == null) 
            {
                err_msg += $"Missing declaration of field:\n\t\t\t";
                err_msg += $"Expected field \"{exp_name}\"@typeof({exp_type}) on typeof({src})" ;
            
                throw new MissingFieldException(err_msg);

            } else if (field.FieldType != exp_type) {

                err_msg += $"Wrong type of \"{exp_name}\"@field:\n\t\t\t";
                err_msg += $"Expected typeof({exp_type}) but found typeof({field.GetType()}))";
                throw new TypeAccessException(err_msg);

            } else if (!field.IsStatic) 
            {
                err_msg += "$Missing static declaration of field\n\t\t";
                err_msg += $"Expected static field: \"{exp_name}\"@typeof({exp_type}) on typeof({src})" ;
                throw new FieldAccessException(err_msg);
            }
        }
    }
}