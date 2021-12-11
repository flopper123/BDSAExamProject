namespace LitExplore.Core.Filter.Filters;

using static LitExplore.Core.Filter.FilterPArgsParser;

public class MinRefsFilter : FilterDecorator<PublicationDto> {

    public MinRefsFilter(int min) : this(min, null) { }

    public MinRefsFilter(int min, Filter<PublicationDto>? _prv) 
        : base(dto => dto.References.Count >= min, min, _prv) {}
    
    protected override IEnumerable<(string type, string str_vals)> getPArgsStringTuple() {
        yield return ("System.Int", (p_args ?? 0).ToString());
    }

    // Receives a seriealized string representation of the PArgs
    // and tries to parse them to an object array containing an instantiation of those objects
    public static Object[] DeserializePArgs(string pargs_serialized) 
    {
        foreach((string t, string val) in ExtractArgs(pargs_serialized)) {
            if (t != "System.Int")
            {
                throw new ArgumentException("Expected one P_Arg argument of type System.Int");
            }
            return new Object[] { val };
        }
        return new Object[] { };
    }
}