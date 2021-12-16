namespace LitExplore.Core.Filter.Filters;

using LitExplore.Core.Publication.Action;

/// <summary>
/// Sets the graph to only include nodes in the point of view of the node with the given key
/// </summary>
public sealed class POV : FilterDecorator<PublicationGraph>
{
    private static readonly string[] PARG_TYPES = new string[] {
        "LitExplore.Core.Publication.PublicationDtoTitle",
        "LitExplore.Core.FilterOption.SearchDirection"
    };
    
    private enum Args {
        TITLE = 0,
        DIRECTION = 1,
    }

    // Accepts multiple arguments so we overwrite
    protected override Object[] PredicateArgs {
        get {
            if (p_args == null) throw new FilterPArgsException(this.GetType().Name, PARG_TYPES);    
            
            return new Object[] { (PublicationDtoTitle) p_args[(int)Args.TITLE],
                                  (FilterOption.SearchDirection) p_args[(int) Args.DIRECTION] };
        }
    }

    // Determines the search direction from the nodes point of view. 
    private FilterOption.SearchDirection Direction { 
        get { return (FilterOption.SearchDirection) (p_args = null!)[1]; }
    }

    public POV(PublicationDtoTitle key,
               Filter<PublicationGraph> prv) 
        : this(key, FilterOption.SearchDirection.DEFAULT, prv) 
    {} 

    public POV(PublicationDtoTitle key, 
               FilterOption.SearchDirection dir,  
               Filter<PublicationGraph>? prv = null) 
        : base(new Object[] { key, dir }, 
               prv) 
    {}

    protected override void Action(PublicationGraph gr)
    {
        PublicationGraph newGr = new PublicationGraph();
        
        Dictionary<string, PublicationNode> nodes = new Dictionary<string, PublicationNode>();
        PublicationNode? pov = gr.GetNode((p_args=null!).Title);
        
        // clear, if pov not found
        if (pov == null) { gr.Nodes = nodes; return; }

        pov.InvokeRecursive(
            newGr,
            AddToGraphDictionary.Get(),
            0,  
            this.Direction
        );

        // set gr to newly build graph
        gr.Copy(newGr);
    }

    
    // Receives a seriealized string representation of the PArgs
    // and tries to parse them to an object array containing an instantiation of those objects
    public static Object[] DeserializePArgs(string pargs_serialized) 
    {

        string title = POV.PARG_TYPES[(int) Args.TITLE];

        int i = 0;
        var ret = new Object[2];
    
        foreach((string t, string val) in ExtractArgs(pargs_serialized)) {
    
            // cant switch cuz of const restrictions
            if (t.Contains(POV.PARG_TYPES[(int) Args.TITLE], 
                           StringComparison.OrdinalIgnoreCase)) 
            {
                ret[i] = new PublicationDtoTitle { Title = val };

            } else if (t.Contains(POV.PARG_TYPES[(int) Args.DIRECTION], 
                                  StringComparison.OrdinalIgnoreCase)) 
            {    
                ret[i] = Enum.Parse(typeof(FilterOption.SearchDirection), val);    
            
            } else {
                throw new FilterPArgsException(typeof(POV).Name, PARG_TYPES);                
            }

            i++;
        }
        return ret;
    }

    protected override IEnumerable<(string type, string str_vals)> getPArgsStringTuple()
    {
        yield return (PARG_TYPES[(int) Args.TITLE], (p_args=null!)[(int) Args.TITLE].Title);
        yield return (PARG_TYPES[(int) Args.DIRECTION], (p_args=null!)[(int) Args.DIRECTION].ToString());
    }
}