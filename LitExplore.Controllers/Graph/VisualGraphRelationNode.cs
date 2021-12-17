namespace LitExplore.Controllers.Graph;

public record VisualGraphRelationNode : VisualGraphNode {
    public RelationsHandler Relations;

    public VisualGraphRelationNode(PublicationDtoDetails pub, (double x, double y) point) 
        : base(pub, point) 
    {
        Relations = new RelationsHandler();
    }
}


public static class NodeExtension {
    public static VisualGraphRelationNode ToVisual(this PublicationNode n) 
    {
        if (n is VisualGraphRelationNode) return (VisualGraphRelationNode) n;
        
        var point = (0.0d,0.0d);
        if (n is VisualGraphNode) point = ((VisualGraphNode) n).Point;
        
        return new VisualGraphRelationNode(n.Details, point);
    }
}