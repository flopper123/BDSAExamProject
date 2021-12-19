namespace LitExplore.Controllers.Graph;

public class GraphNormalizer {

	// Makes sure that max is always 1 and min always 0
	public VisualGraph Normalize(VisualGraph graph) {
        
        var nodes = graph
            .GetNodes()
            .Cast<VisualGraphRelationNode>()
            .ToList();

        // x axis    
        double minx = nodes.Aggregate(1.0, (acc, curr) => Math.Min(acc, curr.Point.x), v => v);
    	double maxx = nodes.Aggregate(1.0, (acc, curr) => Math.Max(acc, curr.Point.x), v => v);
        
        // y axis
        double miny = nodes.Aggregate(1.0, (acc, curr) => Math.Min(acc, curr.Point.y), v => v);
        double maxy = nodes.Aggregate(1.0, (acc, curr) => Math.Max(acc, curr.Point.y), v => v);
        
        double xfac = maxx - minx;
        double yfac = maxy - miny;

        // Make sure we divide by 0
        if (xfac == 0 || yfac == 0) return graph;

        // Inverse factor
        xfac = 1/xfac;
        yfac = 1/yfac;

        // Update all nodes
        nodes.ForEach(node => {
            node.Point = (
                (node.Point.x * xfac) - (minx * xfac),
                (node.Point.y * yfac) - (miny * yfac)
            );
        });

        // Return updated graph
        return graph;
  }

  
}
