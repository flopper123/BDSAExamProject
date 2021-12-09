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
    var ref0 = new ReferenceDto() { Title = "Test reference0" };
    var ref1 = new ReferenceDto() { Title = "Test reference1" };
    var ref2 = new ReferenceDto() { Title = "Test reference2" };
    var ref3 = new ReferenceDto() { Title = "Test reference3" };
    var ref4 = new ReferenceDto() { Title = "Test reference4" };
    references = new List<ReferenceDto> { ref0, ref1, ref2, ref3, ref4 };


    // Test publications
    var pub0 = new PublicationDto { Title = "Test publication", References = new HashSet<ReferenceDto> { ref0, ref1, ref2, ref3 } };

    // Pub1 and pub0 share same title to test for title relation
    var pub1 = new PublicationDto { Title = "Test publication", References = new HashSet<ReferenceDto> { ref1, ref2, ref3 } };

    // Pub1 and pub2 share the same references
    var pub2 = new PublicationDto { Title = "Test publication2", References = new HashSet<ReferenceDto> { ref1, ref2, ref3 } };

    var pub3 = new PublicationDto { Title = "Test publication3", References = new HashSet<ReferenceDto> { ref1, ref2, ref3 } };
    var pub4 = new PublicationDto { Title = "Test publication4", References = new HashSet<ReferenceDto> { ref2, ref3, ref4 } };
    var pub5 = new PublicationDto { Title = "Test publication5", References = new HashSet<ReferenceDto> { ref3, ref4 } };
    var pub6 = new PublicationDto { Title = "Test publication6", References = new HashSet<ReferenceDto> { ref4 } };
    var pub7 = new PublicationDto { Title = "Test publication7", References = new HashSet<ReferenceDto>() };

    publications = new List<PublicationDto> { pub0, pub1, pub2, pub3, pub4, pub5, pub6, pub7 };

  }


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