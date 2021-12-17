namespace LitExplore.Tests.Core.Publication;

using LitExplore.Core.Filter;
using static LitExplore.Tests.Utilities.GraphMockData;

public class PublicationGraphEqualityTests 
{   
    [Fact]
    public void Equality_For_SameGraphs() 
    {
        // Construct without exceptions
        PublicationGraph fst = new PublicationGraph();
        foreach(var n in GetConnectedCycleData(100)) fst.Add(n);
        PublicationGraph snd = new PublicationGraph(GetConnectedCycleData(100));

        PublicationGraph fst_cp = fst;
        // Assert both constructions give same graph
        Assert.True(fst.Equals(snd));
        Assert.True(fst.Equals(fst));
    }

    
    [Fact]
    public void Nodes_AffectHashCode() 
    {
        PublicationGraph fst = new PublicationGraph(GetConnectedCycleData(100));
        PublicationGraph snd = new PublicationGraph(GetConnectedCycleData(99));
        Assert.NotEqual(fst.GetHashCode(), snd.GetHashCode());
    }


    [Fact]
    public void Filter_AffectsHashCode() 
    {
        PublicationGraph fst = new PublicationGraph(GetConnectedCycleData(100));
        PublicationGraph snd = new PublicationGraph(GetConnectedCycleData(100));
        Assert.Equal(fst.GetHashCode(), snd.GetHashCode());

        Filter<PublicationGraph> f = new TitleContains("1");
        fst.Filter(f);
        Assert.NotEqual(fst.GetHashCode(), snd.GetHashCode());

        snd.Filter(f);
        Assert.Equal(fst.GetHashCode(), snd.GetHashCode());
    }

    [Fact]
    public void Equality_For_DifferentGraphs() 
    {
        PublicationGraph fst = new PublicationGraph(GetConnectedCycleData(100));
        PublicationGraph snd = new PublicationGraph(GetConnectedCycleData(101));
        Assert.False(fst.Equals(snd));
    }

    [Fact]
    public void Equality_For_SameObject() {
        PublicationGraph fst = new PublicationGraph(GetConnectedCycleData(100));
        Object snd = new PublicationGraph(GetConnectedCycleData(100));
        Assert.True(fst.Equals(snd));
    }

    [Theory]
    [InlineData((Object) 2)]
    [InlineData((Object) 2UL)]
    [InlineData((Object) "0xDEADBEEF")]
    public void Equality_For_DifferentObject(object obj) 
    {
        PublicationGraph fst = new PublicationGraph(GetConnectedCycleData(100));
        Assert.False(obj.Equals(fst));
    }

    [Fact]
    public void Equality_For_Collections_FromObject() {
        PublicationGraph gr = new PublicationGraph(GetConnectedCycleData(100));
        var nodes = gr.GetNodes();

        // test 10 random collections
        for (int i = 0; i < 10; i++) {
            // random type and data
            var randomized = nodes.RandomizedSubset();

            // random type
            var random_type = nodes.RandomCollection();

            Assert.False(nodes.Equals(random_type));
            Assert.False(nodes.Equals(randomized));
        }   
    }
}