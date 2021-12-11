namespace LitExplore.Entity.Filter;

using System.Text;
using LitExplore.Core.Filter;
using System.Reflection;

/// <summary>
/// FilterFactory class for static construction of filters from a classname and object array
/// </summary>
public class FilterFactory {

    static readonly Assembly _assembly = Assembly.Load("LitExplore.Core");
    private static FilterDeserializer deserializer = new FilterDeserializer();

    public static Filter<T> Deserialize<T>(string serialization) {
        return EmptyFilter<T>.Get();
    }

    public static Filter<T> Create<T>(String className, params Object[] args)
    {
        Filter<T>? filter = null;
        try
        {
            filter = (Filter<T>?)_assembly.CreateInstance(
                "LitExplore.Core.Filter.Filters." + className, true,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance,
                null, args, null, null);

            if (filter == null)
            {
                StringBuilder err_msg = new StringBuilder("Failed creation, filter is null..:");
                err_msg.Append($"\nFilter<{typeof(T)}>.Create(string, obj[])\n\t");
                err_msg.Append($"received obj[] (\n\t\t");
                foreach (object obj in args) {
                    err_msg.Append($"type@{obj.GetType()}~val@{obj.ToString()}\n\t\t");
                }
                err_msg.Append(")");
                throw new NullReferenceException(err_msg.ToString());
            }

        }
        catch (Exception e)
        {
            throw new ArgumentException("\nSomething went wrong during creation of filter..\n\t" +
                                        "Couldn't create the requested object", e);
        }

        return (Filter<T>)filter;
    }

    public static Filter<T> Create<T>(Filter dto) {
        return deserializer.Deserialize<T>(_assembly, dto);
    }
}