namespace LitExplore.Entity.Filter;

using LitExplore.Core.Filter;
using System.Reflection;

/// <summary>
/// FilterFactory class for static construction of filters from a classname and object array
/// </summary>
public class FilterFactory {

    private static Assembly _assembly = Assembly.Load("LitExplore.Core");

    public static Filter<T> Create<T>(String className, params Object[]? args)
    {
        Filter<T>? filter = null;
        try
        {
            filter = (Filter<T>?)_assembly.CreateInstance(
                "LitExplore.Core.Filter." + className, true,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance,
                null, args, null, null);

            if (filter == null) throw new NullReferenceException("Failed creation, filter is null");

        }
        catch (Exception e)
        {
            throw new ArgumentException("\nSomething went wrong during creation of filter..\n\t" +
                                        "Couldn't create the requested object", e);
        }

        return (Filter<T>)filter;
    }

    public static Filter<T> Create<T>(Filter dto) {
        throw new NotImplementedException();
    }
}