namespace LitExplore.Core.Filter.Filters;

using LitExplore.Core.Publication.Action;

/// <summary>
/// Sets the graph to only include nodes in the point of view of the node with the given key
/// </summary>
public sealed class POV : FilterDecorator<PublicationGraph>
{
    // pargs[Args.Title] = typeof(PublicationDtoTitle) 
    // pargs[Args.DIRECTION] = typeof(FilterOption.SearchDirection) 
    private const uint MAX_DEPTH = 100_000;
    private static readonly string[] PARG_TYPES_STR = new string[] {
        "LitExplore.Core.Publication.PublicationDtoTitle",
        "LitExplore.Core.FilterOption.SearchDirection"
    };

    private enum Args
    {
        TITLE = 0,
        DIRECTION = 1,
    }

    // Accepts multiple arguments so we overwrite
    public override Object[] PredicateArgs
    {
        get
        {
            if (p_args == null) throw new FilterPArgsException(this.GetType().Name, PARG_TYPES_STR);

            return new Object[] { (PublicationDtoTitle) p_args[(int)Args.TITLE],
                                  (FilterOption.SearchDirection) p_args[(int) Args.DIRECTION] };
        }
    }

    // Determines the search direction from the nodes point of view. 
    private FilterOption.SearchDirection Direction
    {
        get
        {
            if (p_args == null) throw new FilterPArgsException(this.GetType(), PARG_TYPES_STR);
            return (FilterOption.SearchDirection)(p_args)[(int)Args.DIRECTION];
        }
    }

    // String constructors
    public POV(string key) 
        : this(new PublicationDtoTitle { Title = key })
    {}
    
    public POV(string key, Filter<PublicationGraph>? prv)
        : this(new PublicationDtoTitle { Title = key }, prv) 
    {}

    public POV(string key, FilterOption.SearchDirection dir)
        : this(new PublicationDtoTitle { Title = key }, dir) 
    {}
    
    public POV(string key, FilterOption.SearchDirection dir, Filter<PublicationGraph>? prv)
        : this(new PublicationDtoTitle { Title = key }, dir, prv) 
    {}


    public POV(PublicationDtoTitle key) 
        : this(key, null)
    {}

    // DTOTitle constructors
    public POV(PublicationDtoTitle key,
               Filter<PublicationGraph>? prv) 
        : this(key, FilterOption.SearchDirection.DEFAULT, prv) 
    {} 

    public POV(PublicationDtoTitle key,
               FilterOption.SearchDirection dir)
        : this(key, dir, null)
    {}

    public POV(PublicationDtoTitle key, 
               FilterOption.SearchDirection dir,  
               Filter<PublicationGraph>? prv) 
        : base(new Object[] { key, dir }, 
               prv) 
    {}

    protected override void Action(PublicationGraph gr)
    {
        PublicationGraph newGr = new PublicationGraph();
        
        Dictionary<string, PublicationNode> nodes = new Dictionary<string, PublicationNode>();
        if (p_args == null) throw new NullReferenceException("pargs is null");

        //(p_args)[(int) Args.TITLE]
        PublicationNode? pov = gr.GetNode((p_args)[(int) Args.TITLE].Title);

        // clear, if pov not found
        if (pov == null) { gr.Nodes = nodes; return; }

        pov.InvokeSearch(
            newGr,
            AddToGraphDictionary.Get(),
            MAX_DEPTH,  
            this.Direction
        );

        foreach (var n in newGr.GetNodes())
        {
            var prv = n.Children.Count() + n.Parents.Count();
            var removed = n.RemoveInvalid(newGr);
        }
        // set gr to newly build graph
        gr.Copy(newGr);
    }

    
    // Receives a seriealized string representation of the PArgs
    // and tries to parse them to an object array containing an instantiation of those objects
    public static Object[] DeserializePArgs(string pargs_serialized) 
    {

        string title = POV.PARG_TYPES_STR[(int) Args.TITLE];

        int i = 0;
        var ret = new Object[2];
    
        foreach((string t, string val) in ExtractArgs(pargs_serialized)) {
    
            // cant switch cuz of const restrictions
            if (t.Contains(POV.PARG_TYPES_STR[(int) Args.TITLE], StringComparison.OrdinalIgnoreCase)) 
            {
                ret[(int) Args.TITLE] = new PublicationDtoTitle { Title = val };

            } else if (t.Contains(POV.PARG_TYPES_STR[(int) Args.DIRECTION], StringComparison.OrdinalIgnoreCase)) 
            {    
                ret[(int) Args.DIRECTION] = Enum.Parse(typeof(FilterOption.SearchDirection), val);

            } else {
                throw new FilterPArgsException(typeof(POV).Name, PARG_TYPES_STR);                
            }

            i++;
        }
        return ret;
    }

    protected override IEnumerable<(string type, string str_vals)> getPArgsStringTuple()
    { 
        yield return (PARG_TYPES_STR[(int)Args.TITLE], (String) (p_args)[(int)Args.TITLE].Title);
        yield return ((PARG_TYPES_STR[(int)Args.DIRECTION], (p_args)[(int)Args.DIRECTION].ToString()));
    }
}