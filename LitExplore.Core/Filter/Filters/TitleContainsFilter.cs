namespace LitExplore.Core.Filter.Filters;

/// <summary>
/// Case-Insensitive contains search Title of a PublicationNode Node
/// </summary>
public class TitleContainsFilter : FilterDecorator<NodeDetails<PublicationNode>>
{
    public TitleContainsFilter(string key) : this(key, null) { }

    public TitleContainsFilter(string key, Filter<NodeDetails<PublicationNode>>? _prv) : 
        base( (n) => { 
                return n.Details != null &&
                       n.Details.Details.Title.Contains(key, StringComparison.OrdinalIgnoreCase);
            }, 
            key, 
            _prv
        ){}
    
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
