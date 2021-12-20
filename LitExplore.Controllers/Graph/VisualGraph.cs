
namespace LitExplore.Controllers.Graph;

using LitExplore.Core;

public class VisualGraph : PublicationGraph
{
    // Add graph relations on init
    public void OnInit() {
        var nodes = GetNodes().Select(n => n.ToVisual()).ToList();  
        foreach (var n in nodes) n.AddRelations(nodes);
        Normalize();
    }

    public override PublicationNode TryGetNode(PublicationDtoTitle key)
    {
        PublicationNode? node = null;
        Nodes.TryGetValue(key.Title, out node);

        if (node == null)
        {
            node = new PublicationNode(key.ToDetails());
            node = node.ToVisual();
            this.Nodes.Add(key.Title, node);
        }

        return node;
    }


    // Makes sure that max is always 1 and min always 0
	public void Normalize() {

        // Cast to list
        var nodes = GetNodes().Select(n => n.ToVisual()).ToList();

        // x axis    
        double minx = nodes.Aggregate(1.0, (acc, curr) => Math.Min(acc, curr.Point.x), v => v);
    	double maxx = nodes.Aggregate(0.0, (acc, curr) => Math.Max(acc, curr.Point.x), v => v);
        
        // y axis
        double miny = nodes.Aggregate(1.0, (acc, curr) => Math.Min(acc, curr.Point.y), v => v);
        double maxy = nodes.Aggregate(0.0, (acc, curr) => Math.Max(acc, curr.Point.y), v => v);

        // Find diff
        double xfac = maxx - minx;
        double yfac = maxy - miny;

        // Make sure we divide by 0
        if (xfac == 0 || yfac == 0) return;

        // Inverse factor
        xfac = 1/xfac;
        yfac = 1/yfac;

        // Update all nodes
        nodes.ForEach(node => {
            node.Point.x -= minx;
            node.Point.y -= miny;

            node.Point.x *= xfac;
            node.Point.y *= yfac;
        });
    }

}