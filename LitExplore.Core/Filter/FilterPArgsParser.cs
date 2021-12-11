namespace LitExplore.Core.Filter;

using static StringSplitOptions;

public static class FilterField
{
    public static string NAME = "name";
    public static int NAME_I = 0;

    public static string DEPTH = "depth";
    public static int DEPTH_I = 1;

    public static string P_ARGS = "p_args";
    public static int P_ARGS_I = 2;

    public static char FIELD_SEPERATOR = '~';
    public static char VALUE_SEPERATOR = ':';
    public static string START = "{";
    public static string END = "}";
}

// PArg Fields
public static class FilterPArgField
{
    public static string TYPE = "type";
    public static string VALUE = "value";
    public static char FIELD_SEPERATOR = '~';
    public static char PARG_SEPERATOR = '|';
    public static char VALUE_SEPERATOR = ':';
    public static string LINE_END = ")";
    public static string LINE_START = "(";
}

public static class FilterPArgsParser {

    // Tries to parse the input pArgs serialization string to 
    // type, val pairs
    // Throws OutOfBoundsException if pArgs does not satisfy format
    public static IEnumerable<(string type, string value)> ExtractArgs(string pArgs)
    {
        foreach (string p_arg in pArgs.Split(FilterPArgField.PARG_SEPERATOR, RemoveEmptyEntries))
        {
            string[] splitByFields = p_arg.Split(FilterPArgField.FIELD_SEPERATOR, RemoveEmptyEntries);
            string type = splitByFields[0].Split(FilterPArgField.VALUE_SEPERATOR, RemoveEmptyEntries)[1];
            string value = splitByFields[1].Split(FilterPArgField.VALUE_SEPERATOR, RemoveEmptyEntries)[1];
            type = type.Replace(FilterPArgField.LINE_END, String.Empty);
            value = value.Replace(FilterPArgField.LINE_END, String.Empty);
            value = value.Replace(FilterField.END, String.Empty);
            yield return (type, value);
        }
    }
}