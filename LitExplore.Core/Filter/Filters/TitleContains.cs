namespace LitExplore.Core.Filter.Filters;

using LitExplore.Core.Publication.Action;

/// <summary>
/// Case-Insensitive contains search Title of a PublicationDto
/// </summary>
public class TitleContains : FilterDecorator<PublicationGraph>
{   
    private static string PARG_TYPE = "System.String";

    public TitleContains(string key, Filter<PublicationGraph>? _prv = null)
        : base(key, _prv)
    {}
    
    protected override void Action(PublicationGraph gr) 
    {
        IEnumerable<string> deletions =  
            gr.GetNodes()             
               .Select(t => t.Details.Title)
               .Where(t => !t.Contains(p_args));
        
        foreach (var del in deletions) gr.Delete(del);
    }


    // Receives a seriealized string representation of the PArgs
    // and tries to parse them to an object array containing an instantiation of those objects
    public static Object[] DeserializePArgs(string pargs_serialized) 
    {
        foreach((string t, string val) in ExtractArgs(pargs_serialized)) {
            if (t != PARG_TYPE)
            {
                throw new ArgumentException($"Expected one P_Arg argument of type {PARG_TYPE}");
            }
            return new Object[] { val };
        }
        return new Object[] { };
    }
    
    protected override IEnumerable<(string type, string str_vals)> getPArgsStringTuple() {
        yield return ("System.String", (p_args ?? "null").ToString());
    }
}
