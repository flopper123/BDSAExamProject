
namespace LitExplore.Controllers.Graph;

using LitExplore.Core;


public class RelationsHandler : List<(VisualGraphNode node, double factor)>
{
  public static RelationsHandler FromList(List<(VisualGraphNode, double)> list) {
    RelationsHandler rh = new RelationsHandler();
    rh.AddRange(list);
    return rh; 
  }

}