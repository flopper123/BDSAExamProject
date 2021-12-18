namespace LitExplore.Tests.Controllers.Graph;

using LitExplore.Controllers.Graph;
using System.Linq;

public class VisualGraphTests
{
    public static IEnumerable<object[]> GetPublicationData() {
        // Test publications
        var graph = new VisualGraph();
        foreach (var n in GraphTestData.GetPublications()) graph.Add(n);
        graph.OnInit();
        return graph.GetNodes().Select(n => new object[] { n });
    }

    [Theory]
    [MemberData(nameof(GetPublicationData))]
    public void DoesNormalizeOnInitCorrectly(PublicationNode actual)
    {
        // Arrange
        var node = actual.ToVisual();

        // Assert
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
    public void NumberOfSharedCharsShouldBeProportionalToFactor(string value, string comparing, double expected)
    {
        // Act
        double actual = value.StringHeuristicEqualityFactor(comparing);

        // Assert
        Assert.Equal(expected, actual);
    }
}
