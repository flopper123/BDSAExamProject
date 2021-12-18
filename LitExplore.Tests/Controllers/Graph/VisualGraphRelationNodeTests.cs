namespace LitExplore.Tests.Controllers.Graph;

using LitExplore.Controllers.Graph;

public class VisualGraphRelationNodeTests
{
    
    public List<VisualGraphRelationNode> nodes;


    public VisualGraphRelationNodeTests()
    {

        // Test publications
        var graph = new PublicationGraph();
        foreach (var n in GraphTestData.GetPublications()) graph.Add(n);

        nodes = graph.GetNodes().Select(n => n.ToVisual()).ToList();
    }

    /**
     * Tests for GetRelations
     *
     */
    [Fact]
    public void CanMapOneToManyRelationFactors()
    {
        // Arrange
        VisualGraphRelationNode node0 = nodes[0];
        List<VisualGraphNode> testnodes = new List<VisualGraphNode> { nodes[1], nodes[2], nodes[3], nodes[4], nodes[5] };

        // Act
        node0.AddRelations(testnodes.AsEnumerable());

        RelationsHandler relations = node0.Relations;

        // Assert
        Assert.True(relations.Count == testnodes.Count); // Test number of elements are still the same
        relations.ForEach(relation => Assert.True(testnodes.Contains(relation.node))); // Test that we still have the publications
        relations.ForEach(relation => Assert.True(relation.factor >= 0.0 && relation.factor <= 1.0)); // Test that factors are between 0 and 1
    }

    [Fact]
    public void RelationMappingShouldNotBeReflexive()
    {
        // Arrange
        VisualGraphRelationNode node0 = nodes[0];
        List<VisualGraphNode> testnodes = new List<VisualGraphNode> { nodes[0], nodes[1], nodes[2], nodes[3], nodes[4], nodes[5] };

        // Act
        node0.AddRelations(testnodes.AsEnumerable());

        RelationsHandler relations = node0.Relations;
        Assert.DoesNotContain(node0, relations.Select(r => r.node));
    }

    // [Fact]
    // public void SimilarPublications_Have_HigherRelationScore()
    // {
    //     // Arrange
    //     List<VisualGraphRelationNode> testnodes = new List<VisualGraphRelationNode> { nodes[0], nodes[1], nodes[2] };

    //     // Act
    //     testnodes[0].AddRelations(testnodes.AsEnumerable());
    //     double relation_0_1 = testnodes[0].Relations[0].factor;
    //     double relation_0_2 = testnodes[0].Relations[1].factor;

    //     // Assert
    //     Assert.True(relation_0_1 > relation_0_2);
    // }

    // [Fact]
    // public void SameTitleShouldGiveTitleFactor1()
    // {
    //     // Arrange
    //     var node = nodes[0];

    //     double expected = 1.0d;

    //     // Act
    //     node.AddRelation(nodes[1]);
    //     double actual = node.Relations[0].factor;
        
    //     // Assert
    //     Assert.True(expected == actual, $"\nNode1.Details.Title: {node.Details.Title}\nNode2.Details.Title: {nodes[1].Details.Title}");
    //     Assert.Equal(expected, actual);
    // }

    [Fact]
    public void PubWithDifferentTitles_ShouldGive_TitleFactor0()
    {
        var node0 = nodes[0];
        var node2 = nodes[2];

        double expected = 0.0d;

        // Act
        double actual = node0.GetTitleRelation(node2);

        // Assert
        Assert.Equal(expected, actual);
    }

    // [Fact]
    // public void AllShared_References_Are_Factor1()
    // {
    //     // Arrange
    //     var node1 = nodes[1];
    //     var node2 = nodes[2];

    //     double expected = 1.0d;

    //     // Act
    //     double actual = node1.GetReferenceRelation(node2);

    //     // Assert
    //     Assert.Equal(expected, actual);
    // }


    // [Fact]
    // public void HalfShared_References_Are_Factor0_5()
    // {
    //     // Arrange
    //     var node5 = nodes[5];
    //     var node6 = nodes[6];

    //     double expected = 0.5d;

    //     // Act
    //     double actual = node5.GetReferenceRelation(node6);

    //     // Assert
    //     Assert.Equal(expected, actual);
    // }

    // [Fact]
    // public void QuarterSharedReferencesIsFactor0_25()
    // {
    //     // Arrange
    //     var node0 = nodes[0];
    //     var node5 = nodes[5];

    //     double expected = 0.25d;

    //     // Act
    //     double actual = node0.GetReferenceRelation(node5);

    //     // Assert
    //     Assert.Equal(expected, actual);
    // }

    [Fact]
    public void PublicationWithNoReferencesIsFactor0()
    {
        // Arrange
        var node7 = nodes[7];
        var node0 = nodes[0];

        double expected = 0.0;

        // Act
        double actual = node7.GetReferenceRelation(node0);

        // Assert
        Assert.Equal(expected, actual);
    }
}