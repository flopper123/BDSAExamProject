// TO:DO init project file UTIL
namespace LitExplore.Entity.Filter;

using System;
using System.Reflection;

static class ReflectionUtil
{
    // TO:DO add tests for both methods

    /// <summary>
    /// Returns all non generic types that implement the input open generic type in the current scope 
    /// of the parameter assembly.
    /// Abstract types and interfaces are excluded.
    /// </summary>
    /// <param name="gType"> An open generic type </param>
    /// <param name="assembly"> The assembly to search </param>
    /// <returns> </returns>
    public static IEnumerable<Type> GetAllConcreteTypes(Type gType, Assembly assembly)
    {
        IEnumerable<Type> ret;
        try
        {
            ret = (
                from t in assembly.GetTypes()
                where
                    (t.BaseType != null) &&
                    t.BaseType.IsGenericType &&
                    gType.IsAssignableFrom(t.BaseType.GetGenericTypeDefinition()) &&
                    !(t.BaseType.IsAbstract)
                select t
            );
        } catch (TargetInvocationException ex) {
            throw new TargetInvocationException(
                $"Something went wrong during Reflection search for type@{gType} in assembly@{assembly}", ex
            );
        }
        return ret;
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
    /// Thrown when a static field named @exp_name of type @exp_type 
    /// is not found on concrete classes that implement @src. 
    /// </exception>
    public static void StaticFieldAssertion(Type src, string exp_name, Type exp_type, Assembly tar) {
        string err_msg = $"ReflectionException: \nFailed assertion of availablity for typeof({src}):\n\t\t"
        
        foreach(Type t in ReflectionUtil.GetAllConcreteTypes(src, tar)) {
            FieldInfo? field = src.GetField(exp_name);
        
            if (field == null) 
            {
                err_msg += $"Missing declaration of field:\n\t\t\t";
                err_msg += $"Expected \"{exp_name}\"@typeof({exp_type} on typeof({src})" ;
            
                throw new MissingFieldException(err_msg);
            
            } else if (!field.IsStatic) 
            {
                err_msg += "$Missing static declaration of field\n\t\t";
                err_msg += $"Expected field: \"{exp_name}\"@typeof({exp_type} on typeof({src})" ;
            
                throw new MissingFieldException(err_msg);
            }
        }
    }
}