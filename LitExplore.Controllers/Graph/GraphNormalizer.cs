
namespace LitExplore.Controllers.Graph;

public class GraphNormalizer {

  // Makes sure that max is always 1 and min always 0
  public VisualGraph Normalize(VisualGraph graph) {
    // x axis
    double minx = graph.Aggregate(1.0, (acc, curr) => Math.Min(acc, curr.Node.Point.x), v => v);
    double maxx = graph.Aggregate(0.0, (acc, curr) => Math.Max(acc, curr.Node.Point.x), v => v);

    double xfac = maxx - minx;

    // y axis
    double miny = graph.Aggregate(1.0, (acc, curr) => Math.Min(acc, curr.Node.Point.y), v => v);
    double maxy = graph.Aggregate(0.0, (acc, curr) => Math.Max(acc, curr.Node.Point.y), v => v);

    double yfac = maxy - miny;

    // Make sure we divide by 0
    if (xfac == 0 || yfac == 0) return graph;

    // Inverse factor
    xfac = 1/xfac;
    yfac = 1/yfac;

    // Update all nodes
    graph.ForEach(rn => {
      (double x, double y) point = (
        (rn.Node.Point.x * xfac) - (minx * xfac),
        (rn.Node.Point.y * yfac) - (miny * yfac)
      );
      rn.Node.Point = point;
   
    });

    // Return updated graph
    return graph;
  }
}
