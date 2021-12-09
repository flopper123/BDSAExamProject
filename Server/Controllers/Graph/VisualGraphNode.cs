namespace LitExplore.Server.Controllers.Graph;

using LitExplore.Core;

public class VisualGraphNode
{
  public PublicationDto Publication { get; init; } = null!;

  public (double x, double y) Point { get; set; }

  public RelationsHandler Relations;

  public VisualGraphNode(PublicationDto pub, (double x, double y) point) {
    Publication = pub;
    Point = point;
    Relations = new RelationsHandler();
  }

} 