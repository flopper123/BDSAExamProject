namespace LitExplore.Controllers.Graph;

using LitExplore.Core;

using System;
using System.Linq;

public class GraphRelation
{

  public List<(PublicationDto, RelationsHandler)> GetManyToManyRelations(List<PublicationDto> pubs)
  {
    var relations = from p in pubs select (p, GetRelations(p, pubs));
    return relations.ToList();
  }

  // Returns relation of publication to all other publications
  public RelationsHandler GetRelations(PublicationDto pub, List<PublicationDto> pubs) 
  {
    var relations = from p in pubs
                    where p != pub
                    select (p, GetRelation(pub, p));

    return RelationsHandler.FromList(relations.ToList());
  }

  // Returns relation between first pub to second
  public double GetRelation(PublicationDto pub1, PublicationDto pub2)
  {
    // Collect
    double title = GetTitleRelation(pub1, pub2);
    double refs  = GetReferenceRelation(pub1, pub2);

    // Weight
    double max = 2.0 + 1.5;
    double fac = title * 2.0 + refs * 1.5;

    // Normalize
    return fac / max; 
  }

  // Returns true if titles are the same
  public double GetTitleRelation(PublicationDto pub1, PublicationDto pub2)
  {
    return pub1.Title.Equals(pub2.Title, StringComparison.OrdinalIgnoreCase) ? 1.0 : 0.0;
  }

  // First compared to second publication
  public double GetReferenceRelation(PublicationDto pub1, PublicationDto pub2)
  {

    // Return if 0
    if (pub1.References.Count == 0) return 0.0;

    // Keeps track of not shared references
    var references = new HashSet<ReferenceDto>();

    // Find the amount of references they do not share
    pub1.References
      .ToList()
      .ForEach( p => references.Add(p) );

    pub2.References
      .ToList()
      .ForEach( p => references.Remove(p));

    // Calculate factor
    double p1RefCount = (double) pub1.References.Count;
    double diff = p1RefCount - (double) references.Count;
    return diff / p1RefCount;
  }

}