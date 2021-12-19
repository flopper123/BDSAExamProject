namespace LitExplore.Controllers.Graph;

public record VisualGraphRelationNode : VisualGraphNode {
    public RelationsHandler Relations;

    public VisualGraphRelationNode(PublicationDtoDetails pub, (double x, double y) point) 
        : base(pub, point) 
    {
        Relations = new RelationsHandler();
    }

    // Creates relations between the input nodes and this
    public void AddRelations(IEnumerable<VisualGraphNode> nodes)
    {
        foreach (var n in nodes)
        {
            if (n == this) continue;
            this.AddRelation(n);
        }
    }

    public void AddRelation(VisualGraphNode n) {
        this.Relations.Add((n, GetRelation(n)));
    }

    // Returns relation between first pub to second
	public double /* AddRelation */ GetRelation(VisualGraphNode node)
	{
		// Collect
		//double title = GetTitleRelation(node);
		double refs  = GetReferenceRelation(node);

		// Weight
		double max = 1.5;
		double fac = refs * 1.5;

		// Normalize
		return fac / max; 
	}

	// Returns true if titles are the same
	public double GetTitleRelation(VisualGraphNode node)
	{
        // TO:DO consider adding smooth string comparison
        return this.Details.Title.Equals(node.Details.Title, StringComparison.OrdinalIgnoreCase) ? 1.0 : 0.0;
	}

	// First compared to second publication
	public double GetReferenceRelation(VisualGraphNode node)
	{
        // Return if 0
        if (this.Children.Count == 0 || node.Children.Count == 0) return 0.0;

        // Keeps track of not shared references
        var references = new HashSet<string>();

		// Find the amount of references they do not share
		this.Children
			.ToList()
			.ForEach( p => references.Add(p.Details.Title) );

		node.Children
			.ToList()
			.ForEach( p => references.Remove(p.Details.Title));

		// Calculate factor
		double p1RefCount = (double) this.Children.Count;
		double diff = p1RefCount - (double) references.Count;
		return diff / p1RefCount;
	}
}