using Xunit;

namespace LitExplore.Tests.Controllers.Graph;

using LitExplore.Controllers.Graph;

public class GraphRelationTests
{
  [Fact]
  public void SameTitleShouldGiveNamingFactor1()
  {
    // Arrange
    var gr = new GraphRelation();

    var title = "Test pub title";
    var pub1 = new PublicationDto { Title = title, References = new HashSet<ReferenceDto>() };
    var pub2 = new PublicationDto { Title = title, References = new HashSet<ReferenceDto>() };

    double expected = 1.0d; 

    // Act
    double actual = gr.GetTitleRelation(pub1, pub2);
   
    // Assert
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void PubWithDifferentTitlesShouldGiveNamingFactor0()
  {
    // Arrange
    var gr = new GraphRelation();

    var title1 = "Test pub title 1";
    var title2 = "Test pub title 2";
    var pub1 = new PublicationDto { Title = title1, References = new HashSet<ReferenceDto>() };
    var pub2 = new PublicationDto { Title = title2, References = new HashSet<ReferenceDto>() };

    double expected = 0.0d;

    // Act
    double actual = gr.GetTitleRelation(pub1, pub2);

    // Assert
    Assert.Equal(expected, actual);
  }


  [Fact]
  public void AllSharedReferencesIsFactor1()
  {
    // Arrange
    var gr = new GraphRelation();

    var reference = new ReferenceDto() { Title = "Test reference" };

    var pub1 = new PublicationDto {
      Title = "Test pub title 1",
      References = new HashSet<ReferenceDto>() { reference }
    };
    var pub2 = new PublicationDto {
      Title = "Test pub title 2",
      References = new HashSet<ReferenceDto>() { reference }
    };

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

    var reference0 = new ReferenceDto() { Title = "Test reference0" };
    var reference1 = new ReferenceDto() { Title = "Test reference1" };
    var reference2 = new ReferenceDto() { Title = "Test reference2" };

    var pub1 = new PublicationDto
    {
      Title = "Test pub title 1",
      References = new HashSet<ReferenceDto>() { reference0, reference1 }
    };
    var pub2 = new PublicationDto
    {
      Title = "Test pub title 2",
      References = new HashSet<ReferenceDto>() { reference1, reference2 }
    };

    double expected = 0.5d;

    // Act
    double actual = gr.GetReferenceRelation(pub1, pub2);

    // Assert
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void QuarterSharedReferencesIsFactor0_25()
  {
    // Arrange
    var gr = new GraphRelation();

    var reference0 = new ReferenceDto() { Title = "Test reference0" };
    var reference1 = new ReferenceDto() { Title = "Test reference1" };
    var reference2 = new ReferenceDto() { Title = "Test reference2" };
    
    var reference3 = new ReferenceDto() { Title = "Test reference3" };

    var pub1 = new PublicationDto
    {
      Title = "Test pub title 1",
      References = new HashSet<ReferenceDto>() { reference0, reference1, reference2, reference3 }
    };
    var pub2 = new PublicationDto
    {
      Title = "Test pub title 2",
      References = new HashSet<ReferenceDto>() { reference3 }
    };

    double expected = 0.25d;

    // Act
    double actual = gr.GetReferenceRelation(pub1, pub2);

    // Assert
    Assert.Equal(expected, actual);
  }
}