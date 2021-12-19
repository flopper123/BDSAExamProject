namespace LitExplore.Core.Publication;

using LitExplore.Core.Publication.Action;
using static LitExplore.Core.Filter.FilterOption;

public record PublicationNode : IEquatable<PublicationDtoTitle> {

    public List<PublicationNode> Parents { get; protected set; } = new List<PublicationNode>();
    public List<PublicationNode> Children { get; protected set; } = new List<PublicationNode>();
    public PublicationDtoDetails Details { get; protected set; } = null!;
    
    public PublicationNode(PublicationDtoDetails details) {
        this.Details = details;
    }

    private static bool shouldVisit(SearchDirection opts, UInt32 depthSeen, UInt32 curDepth) {
        bool visit = true;
        if (opts.HasFlag(SearchDirection.VISIT_ONCE)) visit = (depthSeen == uint.MaxValue);
        else if (opts.HasFlag(SearchDirection.VISIT_MINDEPTH)) visit = (depthSeen > curDepth);
        return visit;
    }


    protected static List<PublicationNode> GetSearchTargets(IEnumerable<PublicationNode> src, SearchDirection opts) {
        HashSet<PublicationNode> l = new HashSet<PublicationNode>();
        foreach (var n in src)
        {
            foreach (var c in n.GetSearchTargets(opts)) {
                l.Add(c);
            }
        }
        return l.ToList();
    }

    // Apply direction checks and return a list of all that satisfy
    protected List<PublicationNode> GetSearchTargets(SearchDirection opts) 
    {
        HashSet<PublicationNode> set = new HashSet<PublicationNode>();
        // Add other search patterns as needed
        if (opts.HasFlag(SearchDirection.CHILDREN)) foreach (var c in this.Children) set.Add(c);
        if (opts.HasFlag(SearchDirection.PARENTS)) foreach (var p in this.Parents) set.Add(p);
        return set.ToList();
    }

    // Apply action to target in manér of options
    // For example if its used with AddToGraphDictionary,
    //      Adds all nodes connected to this path to @gr 
    public void InvokeSearch(PublicationGraph gr,
                             GraphAction act,
                             UInt32 max_depth,
                             SearchDirection opts = SearchDirection.DEFAULT) 
    {        
        // add static cache
        this.bfs(gr, act, max_depth, opts);
    }

    private void bfs(PublicationGraph gr,
                     GraphAction act,
                     UInt32 max_depth,
                     SearchDirection opts = SearchDirection.DEFAULT)
    {
        // titles of visitors to depth visited
        var visited = new Dictionary<string, UInt32>();
        visited[this.Details.Title] = 0;
        act.Invoke(this.ToNodeDetails(0UL), gr);

        var depth = 1u;
        var targets = this.GetSearchTargets(opts);

        while (max_depth > depth && targets.Count() != 0)
        {
            int i = 0;
            
            var lastVisited = new List<PublicationNode> ();
            // All the nodes at the next layer that satisfies search opts
            foreach (var c in targets)
            {
                i++;
                uint prvDepth;
                if (!visited.TryGetValue(c.Details.Title, out prvDepth)) {
                    prvDepth = uint.MaxValue;
                    visited[c.Details.Title] = depth;
                }
                else {
                    if (prvDepth > depth) visited[c.Details.Title] = depth;
                }

                if (!shouldVisit(opts, prvDepth, depth)) continue;
                // if (i > 10000) throw new Exception("i == 10000 : depth#" + depth + "  max_depth#" + max_depth + " visited so far:#" + visited.Count());
                lastVisited.Add(c);
                act.Invoke(c.ToNodeDetails(depth), gr);
            }
            depth++;
            
            targets = PublicationNode.GetSearchTargets(lastVisited, opts);
        }
    }

    // TO:DO Implement functionality to avoid visiting same node twice in a cycle graph. 
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
        
        var missing = new List<PublicationDtoTitle>();
        foreach (var t in titles) if (!Children.ContainsTitle(t)) missing.Add(t);
        
        // Missing children from titles
        return missing;
    }
}
 