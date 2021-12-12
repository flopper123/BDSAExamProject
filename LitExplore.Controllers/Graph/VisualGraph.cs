
namespace LitExplore.Controllers.Graph;

using LitExplore.Core;


public class VisualGraph : List<VisualGraphRelationNode>
{
  public static VisualGraph FromList(List<(VisualGraphNode node, RelationsHandler relations)> list)
  {
    VisualGraph vg = new VisualGraph();

    var transformed = list.Select(n => new VisualGraphRelationNode {
      Node = n.node,
      Relations = n.relations,
    }).ToList();

    vg.AddRange(transformed);
    return vg;
  }

}