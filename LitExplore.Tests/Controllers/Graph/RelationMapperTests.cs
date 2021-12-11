
using Xunit;
namespace LitExplore.Tests.Controllers.Graph;

using LitExplore.Controllers.Graph;

using System.Linq;


public class RelationMapperTests
{
  public static List<PublicationDto> Publications = GraphTestData.GetPublications();


  [Fact]
  public void CanReturnAListOfGraphNodes() {
    // Arrange
    var rm = new RelationMapper();

    // Act
    List<VisualGraphNode> nodes = rm.MapPublications(Publications);

    // Assert
    Assert.Equal(Publications.Count, nodes.Count); // Make sure we have the same number of elements
  }


  public static IEnumerable<object[]> GetPublicationData => Publications.Select(p => new object[] { p });

  [Theory]
  [MemberData(nameof(GetPublicationData), parameters: 1)]
  public void CanReturnCorrectGraphNode(PublicationDto pub)
  {
    // Arrange
    var rm = new RelationMapper();

    // Act
    VisualGraphNode node = rm.MapPublication(pub);

    // Assert
    Assert.Equal(node.Publication, pub);

    Assert.True(node.Point.x >= 0);
    Assert.True(node.Point.x <= 1);

    Assert.True(node.Point.y >= 0);
    Assert.True(node.Point.y <= 1);
  }

  // Checks of number of chars is proportional
  [Theory]
  [InlineData("test", "t", 0.5)]
  [InlineData("test", "a", 0)]
  [InlineData("test", "s", 0.25)]
  [InlineData("aabb", "aatt", 0.5)]
  [InlineData("skrt", "skrtyeet", 1.0)]
  [InlineData("", "skrtyeet", 0.0)]
  [InlineData("test", "", 0.0)]
  [InlineData("", "", 0.0)]
  public void NumberOfSharedCharsShouldBeProportionalToFactor(string value, string comparing, double expected) {
    // Arrange
    var rm = new RelationMapper();

    // Act
    double actual = rm.StringHeuristicEqualityFactor(value, comparing);

    // Assert
    Assert.Equal(expected, actual);
  }
}
