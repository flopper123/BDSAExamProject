namespace LitExplore.Core.Publication;

class PublicationGraph : Graph<PublicationDtoTitle, PublicationDtoDetails>
{
    PublicationGraph() : base() {}

    public override void AddDetails(PublicationDtoDetails details) { base._details.Add(details, details); }

    public override void Apply(Filter<NodeDetails<PublicationDtoDetails>> filter)
    {
        // Apply filtering recursively in a depth first manér to all trees
        foreach (Tree<PublicationDtoTitle> tree in this)
        {
            if (filter.ShouldRemove(ToDetails(tree.Root = null!))) {
                Roots.Remove(tree);
                continue;
            }
            filterChildren(tree.Root = null!, filter);
        }
    }

    public override NodeDetails<PublicationDtoDetails> ToDetails(INode<PublicationDtoTitle> key)
    {
        PublicationDtoDetails? details = null;
        try { details = _details[key.Data]; } catch (KeyNotFoundException) {}
        return new NodeDetails<PublicationDtoDetails> { Details = details, Depth = key.Depth, Size = key.Size };
    }

    public override UInt64 Delete(PublicationDtoTitle v)
    {
        UInt64 ret = 0;
        foreach(Tree<PublicationDtoTitle> t in base.Roots) {
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
    private void filterChildren(INode<PublicationDtoTitle> n, Filter<NodeDetails<PublicationDtoDetails>> filter) {
        for (int i = 0; i < n.Children.Count; i++) {
            INode<PublicationDtoTitle> child = n.Children[i];
            if (filter.ShouldRemove(ToDetails(child))) 
            {
                n.Children.RemoveAt(i);
            } else filterChildren(child, filter);
        }
    }
}







