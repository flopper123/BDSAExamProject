namespace LitExplore.Controllers.Graph;

using LitExplore.Core;

public record VisualGraphNode : PublicationNode
{
    public (double x, double y) Point;  
    
    public VisualGraphNode(PublicationDtoDetails pub, (double x, double y) point) : base(pub) {
      Point = point;
    }
} 