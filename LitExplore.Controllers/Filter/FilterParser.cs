namespace LitExplore.Controllers.Filter;

using System.Text;

// For parsing filters from user input
public sealed class FilterParser 
{
    private static FilterParser? _this;
    internal const char PARG_SEPERATOR = '^';

    private FilterParser() {}

    public static FilterParser Get() {
        if (_this == null) _this = new FilterParser();
        return _this;
    }

    // Parse from user input filter name and fpargs
    public static (string n, Object[] pargs) Parse(string fname, string fpargs) {
        string name = fname.Sanitize();
        List<string> split_pargs = SplitPargs(fpargs).Select(parg => parg.Sanitize())
                                                     .ToList();
        var type_str = "";
        switch (name.ToLower())
        {
            case "titlecontains":
            case "pov":
                if (split_pargs.Count() != 1) { type_str = "string"; break; }
                return (name, new object[] { split_pargs[0] });
            case "minrefs":
                if (split_pargs.Count() != 1) { type_str = "int"; break; }
                return (name, new object[] { int.Parse(split_pargs[0]) });
            default:
                throw new NotImplementedException($"Unknown type {name} : No such filter is implemented");
        }

        throw new ArgumentException($"Expected #1 argument of type {type_str}, but received: #{split_pargs.Count}");
        return ("", new object[0]);
    }

    private static List<string> SplitPargs(string upargs) {
        var pargs = upargs.Split(PARG_SEPERATOR, StringSplitOptions.RemoveEmptyEntries /*| StringSplitOptions.TrimEntries*/).ToList();
        for (int i = 0; i < pargs.Count(); i++) pargs[i] = pargs[i].Sanitize();
        return pargs;
    }
}

public static class UserInputExtension {

    public static string Sanitize(this string userInput) {
        return new string(userInput.Where(uin => Char.IsLetterOrDigit(uin) || Char.IsWhiteSpace(uin) || uin == ':' || uin == '-')
                                   .ToArray());
    }
}