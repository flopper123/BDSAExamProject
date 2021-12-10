namespace LitExplore.Entity.Filter;

using System.Reflection;

/// <summary>
/// FilterFactory class for static construction of filters from a classname and object array
/// </summary>
public static class Filter {

    private static Assembly _assembly = Assembly.Load("LitExplore.Entity");

    public static Filter<T> Create<T>(String className, params Object[]? args)
    {
        Filter<T>? filter = null;
        try
        {
            filter = (Filter<T>?)_assembly.CreateInstance(
                "LitExplore.Entity.Filter." + className, true,
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

    public static Filter<T> Create<T>(FilterDto dto) {
        throw new NotImplementedException();
    }
}
