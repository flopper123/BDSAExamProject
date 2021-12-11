namespace LitExplore.Entity.Filter;

using System.Text;
using LitExplore.Core.Filter;
using System.Reflection;

/// <summary>
/// FilterFactory class for static construction of filters from a classname and object array
/// </summary>
public class FilterFactory {

    static readonly Assembly _assembly = Assembly.Load("LitExplore.Core");
    static readonly string EXP_PATH = $"LitExplore.Core.Filter.Filters";
    private static FilterDeserializer deserializer = new FilterDeserializer();

    public static Filter<T> Deserialize<T>(string fs) {
        return deserializer.Deserialize<T>(_assembly, fs);
    }

    static void PrintArr(Object[] arr, string name) {
        Console.WriteLine($"{name}: Printing size@{arr.Length} object arr ");
        for (int i = 0; i < arr.Length; i++) {
            Console.WriteLine($"\t type@{arr[i].GetType()} ~ val@{arr[i].ToString()}");
        }
    }
    public static Filter<T> Create<T>(String className, params Object[] args)
    {

        var name = className;
        if (!className.Contains(EXP_PATH)) {
            name = $"{EXP_PATH}.{className}";
        }
        Filter<T>? filter = null;
        try
        {
            Console.WriteLine($"Trying to Instance Class: {name} : With args@{args.Length}"); // ${null}??
            Console.WriteLine($"\ttype@{args[0].GetType()} ~ value@{args[0].ToString()}");
            filter = (Filter<T>?)_assembly.CreateInstance(
                name, true,
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
        Console.WriteLine("Finished :D ");
        /*
        Trying to Instance Class: LitExplore.Core.Filter.Filters.TitleFilter : With args@1
        type@System.String ~ value@0xDEADBEEF
        Trying to Instance Class: LitExplore.Core.Filter.Filters.TitleFilter : With args@1
        type@System.String ~ value@0xDEADBEEF
        */
        return (Filter<T>)filter;
    }

    public static Filter<T> Create<T>(Filter dto) {
        return deserializer.Deserialize<T>(_assembly, dto.Serialization);
    }
}