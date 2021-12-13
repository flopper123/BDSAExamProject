namespace LitExplore.Core.Publication;

class PublicationGraph : Graph<PublicationTitle, PublicationDetails>
{
    PublicationGraph() : base() {}

    public override void AddDetails(PublicationDetails details) { base._details.Add(details, details); }

    public override void Apply(Filter<NodeDetails<PublicationDetails>> filter)
    {
        // Apply filtering recursively in a depth first manér to all trees
        foreach (Tree<PublicationTitle> tree in this)
        {
            if (filter.ShouldRemove(ToDetails(tree.Root = null!))) {
                Roots.Remove(tree);
                continue;
            }
            filterChildren(tree.Root = null!, filter);
        }
    }

    public override NodeDetails<PublicationDetails> ToDetails(INode<PublicationTitle> key)
    {
        PublicationDetails? details = null;
        try { details = _details[key.Data]; } 
        catch (KeyNotFoundException e) {}
        return new NodeDetails<PublicationDetails> { Details = details, Depth = key.Depth, Size = key.Size };
    }

    public override UInt64 Delete(PublicationTitle v)
    {
        UInt64 ret = 0;
        foreach(Tree<PublicationTitle> t in base.Roots) {
            if (t.Root == null) throw new NullReferenceException("Empty tree detected! Resulted in a NullReferenceException");
            if (t.Root.Data.Equals(v)) {
                Roots.Remove(t);
                ret++;
            } else {
                if (t.Delete(v)) ret++;
            }
        }
        return ret;
    }
    
    /// Auxiliary method for doing recursive filtering calls.
    private void filterChildren(INode<PublicationTitle> n, Filter<NodeDetails<PublicationDetails>> filter) {
        for (int i = 0; i < n.Children.Count; i++) {
            INode<PublicationTitle> child = n.Children[i];
            if (filter.ShouldRemove(ToDetails(child))) 
            {
                n.Children.RemoveAt(i);
            } else filterChildren(child, filter);
        }
    }
}







