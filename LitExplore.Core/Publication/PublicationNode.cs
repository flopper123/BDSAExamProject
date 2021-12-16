namespace LitExplore.Core.Publication;

public record PublicationNode : IEquatable<PublicationDtoTitle> {

    public List<PublicationNode> Parents { get; protected set; } = new List<PublicationNode>();
    public List<PublicationNode> Children { get; protected set; } = new List<PublicationNode>();
    public PublicationDtoDetails Details { get; protected set; } = null!;
    
    public PublicationNode(PublicationDtoDetails details) {
        this.Details = details;
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

    /// <param name="filter"> Filter to apply </param>
    /// <param name="visisted"> A dictionary holding what titles are previously visisted </param>
    /// <param name="depth"></param>
    public void FilterChildren(Filter<NodeDetails<PublicationNode>> filter, 
                               Dictionary<string, UInt32> visitHistory,
                               UInt32 depth) {

        UInt32 prv_depth = UInt32.MaxValue;
        
        bool hasVisisted = visitHistory.TryGetValue(this.Details.Title, out prv_depth);
        if (hasVisisted && prv_depth < depth) return; // if previously visisted at a lower depth
        if (!hasVisisted) visitHistory.Add(this.Details.Title, depth);

        depth += 1U;
        // filter children recursively
        foreach (PublicationNode child in this.Children) {
        
            var details = new NodeDetails<PublicationNode>() {
                Details = child,
                Depth = depth,
            };

            if (filter.ShouldRemove(details))
            {
                this.Children.Remove(child);
                child.Parents.Remove(this);

            } else child.FilterChildren(filter, visitHistory, depth);
        }

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