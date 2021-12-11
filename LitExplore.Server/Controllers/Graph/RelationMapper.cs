namespace LitExplore.Server.Controllers.Graph;

using LitExplore.Core;

using System;
using System.Linq;

// Maps a relation to (x, y) where x and y are in the range of [0-1]
public class RelationMapper
{

  // Maps a list of publications to visual graph nodes
  public List<VisualGraphNode> MapPublications(List<PublicationDto> publications) {
    return publications.Select(pub => MapPublication(pub)).ToList<VisualGraphNode>();
  }

  // Maps a publication to (x, y) using heuristics
  public VisualGraphNode MapPublication(PublicationDto pub) {
    
    var point = (
      x: StringHeuristicEqualityFactor(pub.Title, "first"),
      y: StringHeuristicEqualityFactor(pub.Title, "second")
    );

    return new VisualGraphNode(pub, point);
  }


  // Returns a double based on how much actual reminds of comparing
  public double StringHeuristicEqualityFactor(string actual, string comparing) {

    // Make sure not to divide by 0
    if (actual.Length == 0 || comparing.Length == 0) return 0.0;

    // Compare how many chars are shared, divide the total amount by length of string to get a value in range [0-1]
    var chars =
      from c in actual
      where comparing.Contains(c, StringComparison.OrdinalIgnoreCase)
      select c;

    return ((double) chars.Count()) / ((double) actual.Length);

  }

}