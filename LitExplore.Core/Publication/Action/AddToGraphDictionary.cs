namespace LitExplore.Core.Publication.Action;

public class AddToGraphDictionary : GraphAction
{

    private static AddToGraphDictionary? _this;

    private static Action<NodeDetails<PublicationNode>, PublicationGraph> _Act =
        delegate (NodeDetails<PublicationNode> d, PublicationGraph t)
        {
            PublicationNode? n = d.Details;
            if (n == null) return;
            t.Nodes.Add(n.Details.Title, n);
        };

    protected AddToGraphDictionary() : base(AddToGraphDictionary._Act) { }

    public static AddToGraphDictionary Get()
    {
        if (_this == null) _this = new AddToGraphDictionary; 
        return _this;
    }
}