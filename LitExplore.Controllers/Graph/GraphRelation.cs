namespace LitExplore.Controllers.Graph;

using System;
using System.Linq;

using LitExplore.Core;


// ! TODO: Change types to match new types xD
public class GraphRelation
{

  public List<(VisualGraphNode, RelationsHandler)> GetManyToManyRelations(List<PublicationDto> pubs)
  {
    // Transform to VisualGraphNodes
    var mapper = new RelationMapper();
    var nodes = mapper.MapPublications(pubs);

    var relations = from n in nodes select (n, GetRelations(n, nodes));
    return relations.ToList();
  }

  // Returns relation of nodes to all other nodes
  public RelationsHandler GetRelations(VisualGraphNode node, List<VisualGraphNode> nodes) 
  {
    var relations = from n in nodes
                    where n != node // Make node is not the current node
                    select (n, GetRelation(node, n));

    return RelationsHandler.FromList(relations.ToList());
  }

  // Returns relation between first pub to second
  public double GetRelation(VisualGraphNode node1, VisualGraphNode node2)
  {
    // Collect
    double title = GetTitleRelation(node1, node2);
    double refs  = GetReferenceRelation(node1, node2);

    // Weight
    double max = 2.0 + 1.5;
    double fac = title * 2.0 + refs * 1.5;

    // Normalize
    return fac / max; 
  }

  // Returns true if titles are the same
  public double GetTitleRelation(VisualGraphNode node1, VisualGraphNode node2)
  {
    return node1.Title.Equals(node2.Title, StringComparison.OrdinalIgnoreCase) ? 1.0 : 0.0;
  }

  // First compared to second publication
  public double GetReferenceRelation(VisualGraphNode node1, VisualGraphNode node2)
  {

    // Return if 0
    if (node1.References.Count == 0 || node2.References.Count == 0) return 0.0;

    // Keeps track of not shared references
    var references = new HashSet<PublicationDtoTitle>();

    // Find the amount of references they do not share
    node1.References
      .ToList()
      .ForEach( p => references.Add(p) );

    node2.References
      .ToList()
      .ForEach( p => references.Remove(p));

    // Calculate factor
    double p1RefCount = (double) node1.References.Count;
    double diff = p1RefCount - (double) references.Count;
    return diff / p1RefCount;
  }

}