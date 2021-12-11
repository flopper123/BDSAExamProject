namespace LitExplore.Core.Filter.Filters;

using System.Text;
using static LitExplore.Core.Filter.FilterPArgsParser;
using static FilterPArgField;

/// <summary>
/// Case-Insensitive contains search Title of a PublicationDto
/// </summary>
public class TitleFilter : FilterDecorator<PublicationDto>
{
    public TitleFilter(string key) : this(key, null) { }

    public TitleFilter(string key, Filter<PublicationDto>? _prv)
        : base(dto => dto.Title.Contains(key, StringComparison.OrdinalIgnoreCase), key, _prv)
    {}
    
    // Receives a seriealized string representation of the PArgs
    // and tries to parse them to an object array containing an instantiation of those objects
    public static Object[] DeserializePArgs(string pargs_serialized) 
    {
        foreach((string t, string val) in ExtractArgs(pargs_serialized)) {
            if (t != "System.String")
            {
                throw new ArgumentException("Expected one P_Arg argument of type System.String");
            }
            return new Object[] { val };
        }
        return new Object[] { };
    }
    
    protected override IEnumerable<(string type, string str_vals)> getPArgsStringTuple() {
        yield return ("System.String", (p_args ?? "null").ToString());
    }
}
