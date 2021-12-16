namespace LitExplore.Core.Publication;

using static LitExplore.Core.Filter.FilterOption;

public record PublicationNode : IEquatable<PublicationDtoTitle> {

    public List<PublicationNode> Parents { get; protected set; } = new List<PublicationNode>();
    public List<PublicationNode> Children { get; protected set; } = new List<PublicationNode>();
    public PublicationDtoDetails Details { get; protected set; } = null!;
    
    public PublicationNode(PublicationDtoDetails details) {
        this.Details = details;
    }

    protected IEnumerable<PublicationNode> GetSearchTargets(SearchDirection dir) 
    {
        if ((dir & SearchDirection.CHILDREN) != 0) foreach (var c in this.Children) yield return c;
        if ((dir & SearchDirection.PARENTS) != 0) foreach (var p in this.Parents) yield return p;
        
        // Add other search patterns as needed
    }

    // Action could be ADD to Graph for example which would add all encountered to graph 

    // Apply action to target in manér of options
    // For example if its used with AddToGraphDictionary,
    //      Adds all nodes connected to this path to @tar 
    public void InvokeRecursive(PublicationGraph tar,
                                GraphAction action,
                                UInt64 depth,
                                SearchDirection opts = SearchDirection.DEFAULT) 
    {
        // apply action to target
        action.Invoke(this.ToNodeDetails(depth), tar);

        // TO:DO implement so we can perform both child and parent search simoultanously
        //       Note! ToList is slow for large search ranges
        GetSearchTargets(opts).ToList()
                              .ForEach(t => t.InvokeRecursive(tar, action, depth + 1, opts));

    }

    public void UpdateDetails(PublicationDtoDetails update)
    {
        Details = new PublicationDtoDetails
        {
            // Always update title
            Title = update.Title,

            Author = (update.Author.Length > Details.Author.Length) ?
                      update.Author : Details.Author,

            // Longest
            Abstract = (update.Abstract.Length > Details.Abstract.Length) ?
                       update.Abstract : Details.Abstract,

            // Largest keywords
            Keywords = (update.Keywords.Count > Details.Keywords.Count) ?
                       update.Keywords : Details.Keywords,

            // Largest references
            References = (update.References.Count > Details.References.Count) ?
                         update.References : Details.References,

            // Add oldest time
            Time = (update.Time > Details.Time) ? Details.Time : update.Time,
        };
    }


    public bool Equals(PublicationDtoTitle? title) {
        if (title == null) return false; 
        return Details.Title.Equals(title.Title);
    }

    // Returns a list of all titles from parameter which this Node doesn't already hold as children.
    // The operation runs in parralel
    internal List<PublicationDtoTitle> MissingChildren(List<PublicationDtoTitle> titles) {
        
        var missing = new ConcurrentBag<PublicationDtoTitle>();
        Parallel.ForEach(titles, (PublicationDtoTitle t) =>
        {
            if (!Children.ContainsTitle(t)) missing.Add(t);
        });
        // Missing children from titles
        return missing.ToList();
    }
}