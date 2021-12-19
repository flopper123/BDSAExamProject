namespace LitExplore.Tests.Controllers.Graph;

using LitExplore.Controllers.Graph;

public class VisualGraphRelationNodeTests
{
    public List<VisualGraphRelationNode> nodes;


    public VisualGraphRelationNodeTests()
    {

        // Test publications
        var graph = new VisualGraph();
        foreach (var n in GraphTestData.GetPublications()) graph.Add(n);

        graph.OnInit();

        var tmp = graph.GetNodes().Select(n => n.ToVisual()).ToList();

        
        nodes = new List<VisualGraphRelationNode>( new VisualGraphRelationNode[GraphTestData.GetPublications().Count()] );

        foreach (var t in tmp) {
            string title = t.Details.Title;
            int i = int.Parse("" + title[title.Length - 1]);
            nodes[i] = t;
        }
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
        
        // Act
        RelationsHandler relations = node0.Relations;

        // Assert
        relations.ForEach(relation => Assert.True(nodes.Contains(relation.node))); // Test that we still have the publications
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

    [Fact]
    public void SimilarPublications_Have_HigherRelationScore()
    {
        // Arrange
        nodes[0].AddRelations(nodes.AsEnumerable());
        
        // Act
        double relation_0_1 = nodes[0].Relations[0].factor;
        double relation_0_4 = nodes[0].Relations[4].factor;
        
        // Assert
        Assert.True(relation_0_1 > relation_0_4, $"fst:{relation_0_1} not higher than snd:{relation_0_4}");
    }

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

    [Fact]
    public void AllShared_References_Are_Factor1()
    {
        // Arrange
        var node1 = nodes[1];
        var node2 = nodes[2];
        double expected = 1.0d;
        // Act
        double actual = node1.GetReferenceRelation(node2);
        // Assert
        Assert.Equal(expected, actual);
    }


    [Fact]
    public void HalfShared_References_Are_Factor0_5()
    {
        // Arrange
        var node5 = nodes[5];
        var node6 = nodes[6];

        double expected = 0.5d;

        // Act
        double actual = node5.GetReferenceRelation(node6);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void QuarterSharedReferencesIsFactor0_25()
    {
        // Arrange
        var node0 = nodes[0];
        var node5 = nodes[5];

        double expected = 0.25d;

        // Act
        double actual = node0.GetReferenceRelation(node5);

        // Assert
        Assert.Equal(expected, actual);
    }

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