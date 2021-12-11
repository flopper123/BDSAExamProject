
namespace LitExplore.Controllers.Graph;

using LitExplore.Core;


public class VisualGraph : List<(VisualGraphNode node, RelationsHandler relations)>
{
  public static VisualGraph FromList(List<(VisualGraphNode, RelationsHandler)> list)
  {
    VisualGraph vg = new VisualGraph();
    vg.AddRange(list);
    return vg;
  }

}