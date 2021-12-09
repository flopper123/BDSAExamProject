using Xunit;

namespace LitExplore.Tests.Controllers.Graph;

using LitExplore.Controllers.Graph;

public class GraphRelationTests
{

  public List<PublicationDto> publications;
  public List<ReferenceDto> references;

  public GraphRelationTests()
  {
    // Test references
    references = GraphTestData.GetReferences();

    // Test publications
    publications = GraphTestData.GetPublications();

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
    List<(PublicationDto pub, RelationsHandler related)> relations = gr.GetManyToManyRelations(pubs);

    // Assert
    Assert.True(relations.Count == pubs.Count); // Test number of elements are still the same
    relations.ForEach(relation => Assert.True(pubs.Contains(relation.pub))); // Test that we still have the publications
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
    var pub0 = publications[0];

    List<PublicationDto> pubs = new List<PublicationDto> { publications[1], publications[2], publications[3], publications[4], publications[5] };

    // Act
    List<(PublicationDto pub, double factor)> relations = gr.GetRelations(pub0, pubs);

    // Assert
    Assert.True(relations.Count == pubs.Count); // Test number of elements are still the same
    relations.ForEach(relation => Assert.True(pubs.Contains(relation.pub))); // Test that we still have the publications
    relations.ForEach(relation => Assert.True(relation.factor >= 0.0 && relation.factor <= 1.0)); // Test that factors are between 0 and 1
  }

  [Fact]
  public void GivenAListOfPubsAPubWillIgnoreRelationToSelf()
  {
    // Arrange
    var gr = new GraphRelation();
    var pub0 = publications[0];

    int expCount = 1;
    var expPub = publications[3];

    List<PublicationDto> pubs = new List<PublicationDto> { pub0, expPub };

    // Act
    List<(PublicationDto pub, double factor)> relations = gr.GetRelations(pub0, pubs);
    int actCount = relations.Count;
    var actPub = relations[0].pub;

    // Assert
    Assert.Equal(expCount, actCount);
    Assert.Equal(expPub, actPub);
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
    var pub0 = publications[0];
    var pub1 = publications[1];
    var pub2 = publications[2];

    // Act
    double relation_0_1 = gr.GetRelation(pub0, pub1);
    double relation_0_2 = gr.GetRelation(pub0, pub2);

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

    var pub0 = publications[0]; // These share the same title
    var pub1 = publications[1];

    double expected = 1.0d;

    // Act
    double actual = gr.GetTitleRelation(pub0, pub1);

    // Assert
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void PubWithDifferentTitlesShouldGiveTitleFactor0()
  {
    // Arrange
    var gr = new GraphRelation();

    var pub0 = publications[0];
    var pub2 = publications[2];

    double expected = 0.0d;

    // Act
    double actual = gr.GetTitleRelation(pub0, pub2);

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

    var pub1 = publications[1];
    var pub2 = publications[2];

    double expected = 1.0d;

    // Act
    double actual = gr.GetReferenceRelation(pub1, pub2);

    // Assert
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void HalfSharedReferencesIsFactor0_5()
  {
    // Arrange
    var gr = new GraphRelation();

    var pub5 = publications[5];
    var pub6 = publications[6];

    double expected = 0.5d;

    // Act
    double actual = gr.GetReferenceRelation(pub5, pub6);

    // Assert
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void QuarterSharedReferencesIsFactor0_25()
  {
    // Arrange
    var gr = new GraphRelation();

    var pub0 = publications[0];
    var pub5 = publications[5];

    double expected = 0.25d;

    // Act
    double actual = gr.GetReferenceRelation(pub0, pub5);

    // Assert
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void PublicationWithNoReferencesIsFactor0()
  {
    // Arrange
    var gr = new GraphRelation();

    var pub0 = publications[0];
    var pub7 = publications[7];

    double expected = 0.0;

    // Act
    double actual = gr.GetReferenceRelation(pub7, pub0);

    // Assert
    Assert.Equal(expected, actual);
  }
}