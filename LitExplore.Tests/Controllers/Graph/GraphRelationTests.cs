namespace LitExplore.Tests.Controllers.Graph;

using LitExplore.Controllers.Graph;

public class GraphRelationTests
{
  public List<PublicationDto> publications;
  public List<VisualGraphNode> nodes;
  public List<PublicationTitle> references;

  public GraphRelationTests()
  {
    // Test references
    references = GraphTestData.GetReferences();

    // Test publications
    publications = GraphTestData.GetPublications();

    // Test nodes created from publications
    var mapper = new RelationMapper();
    nodes = mapper.MapPublications(publications);
  }


  /**
  * Tests for GetRelations
  *
  */
  [Fact]
  public void GivenAListOfPubsReturnsAListWithManyToMany()
  {
    // Arrange
    var gr = new GraphRelation();

    List<PublicationDto> pubs = new List<PublicationDto> { publications[1], publications[2], publications[3], publications[4], publications[5] };

    // Act
    List<(VisualGraphNode node, RelationsHandler related)> relations = gr.GetManyToManyRelations(pubs);

    // Assert
    Assert.True(relations.Count == pubs.Count); // Test number of elements are still the same
    relations.ForEach(relation => Assert.True(pubs.Contains(relation.node.Publication))); // Test that we still have the publications
    relations.ForEach(relation => Assert.True(relation.related.Count == pubs.Count - 1)); // Test that the size is what it should be
  }



  /**
   * Tests for GetRelations
   *
   */
  [Fact]
  public void GivenAPubAndAListOfPubsReturnsListWithRelationFactors()
  {
    // Arrange
    var gr = new GraphRelation();
    var node0 = nodes[0];

    List<VisualGraphNode> testnodes = new List<VisualGraphNode> { nodes[1], nodes[2], nodes[3], nodes[4], nodes[5] };

    // Act
    RelationsHandler relations = gr.GetRelations(node0, testnodes);

    // Assert
    Assert.True(relations.Count == testnodes.Count); // Test number of elements are still the same
    relations.ForEach(relation => Assert.True(testnodes.Contains(relation.node))); // Test that we still have the publications
    relations.ForEach(relation => Assert.True(relation.factor >= 0.0 && relation.factor <= 1.0)); // Test that factors are between 0 and 1
  }

  [Fact]
  public void GivenAListOfNodesANodeWillIgnoreRelationToSelf()
  {
    // Arrange
    var gr = new GraphRelation();

    int expCount = 1;
    var expNode = nodes[3];

    var node0 = nodes[0];

    var tnodes = new List<VisualGraphNode> { node0, expNode };

    // Act
    RelationsHandler relations = gr.GetRelations(node0, tnodes);
    int actCount = relations.Count;
    var actNode = relations[0].node;

    // Assert
    Assert.Equal(expCount, actCount);
    Assert.Equal(expNode, actNode);
  }



  /**
   * Tests for GetRelation
   *
   */
  [Fact]
  public void MoreSimilarPublicationsShouldGiveHigherRelationScore()
  {
    // Arrange
    var gr = new GraphRelation();
    
    var node0 = nodes[0];
    var node1 = nodes[1];
    var node2 = nodes[2];

    // Act
    double relation_0_1 = gr.GetRelation(node0, node1);
    double relation_0_2 = gr.GetRelation(node0, node2);

    // Assert
    Assert.True(relation_0_1 > relation_0_2);
  }



  /**
   * Tests for GetTitleRelation
   *
   */
  [Fact]
  public void SameTitleShouldGiveTitleFactor1()
  {
    // Arrange
    var gr = new GraphRelation();
    
    var node0 = nodes[0]; // These share the same title
    var node1 = nodes[1];

    double expected = 1.0d;

    // Act
    double actual = gr.GetTitleRelation(node0, node1);

    // Assert
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void PubWithDifferentTitlesShouldGiveTitleFactor0()
  {
    // Arrange
    var gr = new GraphRelation();
    
    var node0 = nodes[0];
    var node2 = nodes[2];

    double expected = 0.0d;

    // Act
    double actual = gr.GetTitleRelation(node0, node2);

    // Assert
    Assert.Equal(expected, actual);
  }



  /**
   * Tests for GetReferenceRelation
   *
   */

  [Fact]
  public void AllSharedReferencesIsFactor1()
  {
    // Arrange
    var gr = new GraphRelation();
    
    var node1 = nodes[1];
    var node2 = nodes[2];

    double expected = 1.0d;

    // Act
    double actual = gr.GetReferenceRelation(node1, node2);

    // Assert
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void HalfSharedReferencesIsFactor0_5()
  {
    // Arrange
    var gr = new GraphRelation();
    
    var node5 = nodes[5];
    var node6 = nodes[6];

    double expected = 0.5d;

    // Act
    double actual = gr.GetReferenceRelation(node5, node6);

    // Assert
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void QuarterSharedReferencesIsFactor0_25()
  {
    // Arrange
    var gr = new GraphRelation();
    
    var node0 = nodes[0];
    var node5 = nodes[5];

    double expected = 0.25d;

    // Act
    double actual = gr.GetReferenceRelation(node0, node5);

    // Assert
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void PublicationWithNoReferencesIsFactor0()
  {
    // Arrange
    var gr = new GraphRelation();
    
    var node7 = nodes[7];
    var node0 = nodes[0];

    double expected = 0.0;

    // Act
    double actual = gr.GetReferenceRelation(node7, node0);

    // Assert
    Assert.Equal(expected, actual);
  }
}