namespace LitExplore.Controllers.Graph;

using LitExplore.Core;

public class VisualGraphNode
{
  public PublicationDto Publication { get; init; } = null!;

  public (double x, double y) Point { get; set; }

  // Helper methods
  public string Title { get => Publication.Title; }
  public ISet<ReferenceDto> References { get => Publication.References; }

  public RelationsHandler Relations;

  public VisualGraphNode(PublicationDto pub, (double x, double y) point) {
    Publication = pub;
    Point = point;
    Relations = new RelationsHandler();
  }

} 