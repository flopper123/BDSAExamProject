namespace LitExplore.Entity.Filter;

using LitExplore.Core.Filter;
using LitExplore.Core.Filter.Filters;
using System.Reflection;

using static LitExplore.Core.Filter.FilterField;
using static StringSplitOptions;

internal static class StringExtension {
        
    public static int ToInt(this string str) {

        bool hasFound = false;
        int ret = 0;

        for (int i = 0; i < str.Length; i++) {

            char c = str[i];
            if (c >= '0' && c <= '9') {
                ret *= 10;
                ret += (int) (c - '0');
                hasFound = true;
            } else if (hasFound) {
                break;
            }
        }
        return ret;
    }
}

/// TO:DO Implement functionality to deserialize object
internal class FilterDeserializer
{
    static readonly string PARGS_SERIAL_METHOD = "DeserializePArgs";

    private (string, Object[]) DeserializeSingle(Assembly assembly, string fs)
    {
        // three fields so we split in three
        string[] fields = fs.Split(FIELD_SEPERATOR, 3, RemoveEmptyEntries);

        string cl_name = fields[NAME_I].Split(VALUE_SEPERATOR, 2, RemoveEmptyEntries | TrimEntries)[1];
        int depth = fields[DEPTH_I].Split(VALUE_SEPERATOR, 2, RemoveEmptyEntries | TrimEntries)[1]
                                   .ToInt();

        if (depth == 0) throw new ArgumentException("Second argument is invalid, expected depth to be a valid int");

        Type? cl_type = assembly.GetType(cl_name);

        if (cl_type == null) throw new ArgumentNullException($"Excpected Type of {cl_name}, but was not found in the current context of {assembly.FullName}");

        MethodInfo? pargs_serializer = cl_type.GetMethod(PARGS_SERIAL_METHOD);

        if (pargs_serializer == null)
        {
            var err_msg = $"Expected method \"{PARGS_SERIAL_METHOD}\" on typeof({cl_type})";
            throw new MissingMethodException($"Missing declartion of method: \n\t\t\t{err_msg}");
        }

        string arg_str = fields[P_ARGS_I].Split(VALUE_SEPERATOR, 2, RemoveEmptyEntries)[1];
        // ADD ON ARG_STR VIA FilterPArgsParser.ExtractPArgs(arg_str);

        Object?[] serializer_arg = new object?[] { arg_str };
        Object[] pargs = (Object[]) (pargs_serializer.Invoke(null, serializer_arg) ?? new Object[] {});

        return (cl_name, pargs);
    }  

    // No exceptions - GUARANTEED
    internal Filter<T> Deserialize<T>(Assembly assembly, Filter f) {
        // serialization
        string fs = f.Serialization;
        StringReader reader = new StringReader(f.Serialization);

        Filter<T> current = EmptyFilter<T>.Get();
        string? line;

        while ((line = reader.ReadLine()) != null) {
            (string fName, Object[] fPArgs) = DeserializeSingle(assembly, line);
            
            // Insert current filter into arguments
            Object[] pargs = new Object[fPArgs.Length + 1];
            fPArgs.CopyTo(pargs, 0);
            fPArgs[fPArgs.Length - 1] = current;

            current = FilterFactory.Create<T>(fName, fPArgs);
        }

        return current;
    }
}