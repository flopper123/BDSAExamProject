namespace LitExplore.Tests.Core.Publication;

using LitExplore.Core.Graph;
using LitExplore.Core.Filter;
using System.Text;

using static LitExplore.Tests.Utilities.GraphMockData;

public class PublicationGraphTests
{
    PublicationGraph _graph = new PublicationGraph();
    TitleContains tFilter = new TitleContains("1");
    // MaxDepthFilter dFilter = new MaxDepthFilter(2);
    int N = 0;

    public PublicationGraphTests() {
        var pubs = GetPublications;
        foreach (PublicationDtoDetails dto in GetPublications())
        {
            this._graph.Add(dto);
            N++;
        }
    }

    internal static IEnumerable<PublicationDtoDetails> GetPublications()
    {
        // roots
        yield return new PublicationDtoDetails { Title = "Disconnected" };
        yield return new PublicationDtoDetails { Title = "root0", References = GetHashSet("0") };
        yield return new PublicationDtoDetails { Title = "0", References = GetHashSet("2", "3") };
        yield return new PublicationDtoDetails { Title = "1", References = GetHashSet("2", "3") };
        yield return new PublicationDtoDetails { Title = "4", References = GetHashSet("5", "6") };

        // children
        yield return new PublicationDtoDetails { Title = "2", References = GetHashSet("3", "5") };
        yield return new PublicationDtoDetails { Title = "3", References = GetHashSet("7", "8") };
        yield return new PublicationDtoDetails { Title = "5", References = GetHashSet("6") };
        yield return new PublicationDtoDetails { Title = "6", References = GetHashSet("7", "11") };
        yield return new PublicationDtoDetails { Title = "7", References = GetHashSet("11") };
        yield return new PublicationDtoDetails { Title = "8", References = GetHashSet("10") };
        yield return new PublicationDtoDetails { Title = "10" };
        yield return new PublicationDtoDetails { Title = "11" };
    }

    [Fact]
    public void Can_Construct() {
        // Construct without exceptions
        PublicationGraph gr = new PublicationGraph();
        foreach(var n in GetConnectedCycleData(100)) gr.Add(n);
        PublicationGraph exp = new PublicationGraph(GetConnectedCycleData(100));
        
        // Assert both constructions give same graph
        Assert.Equal(gr.GetNodes(), exp.GetNodes());
    }

    [Fact]
    public void Can_Get() 
    {
        foreach (var exp in _graph.GetNodes()) 
        {
            var act = _graph.GetNode(exp.Details.Title);
            Assert.NotNull(act);
            if (act == null) return; 
            Assert.True(act.CustomEquals(exp), $"Expected does not equal act: \n{exp} \n\t: \n{act}");
        }   
    }

    [Fact]
    public void Does_Get_ReturnNull_KeyNotFound() {
        Assert.Null(_graph.GetNode("Something we would never put into a graph"));
    }

    [Fact]
    public void Does_EmptyGraph_HaveSize0() {
        PublicationGraph gr = new PublicationGraph();
        Assert.Equal(0, gr.Size);
    }

    [Fact]
    public void Add_Increments_SizeBy1() {
        var exp = _graph.Size + 1;
        _graph.Add(new PublicationDtoDetails { Title = "0xDEADBEEF | 0xDEADBEEF" } );
        Assert.Equal(exp, _graph.Size);
    }

    [Fact]
    public void CanAddToEmptyGraph() {
        PublicationGraph gr = new PublicationGraph();
        gr.Add(new PublicationDtoDetails { Title = "0xDEADBEEF | 0xDEADBEEF" } );
        Assert.Equal(1, gr.Size);
    }

    [Fact]
    public void Delete_Subtracts_SizeBy1() {
        var exp = _graph.Size;
        var title = "0xDEADBEEF | 0xDEADBEEF";
        _graph.Add(new PublicationDtoDetails { Title = title } );
        _graph.Delete(title);
        Assert.Equal(exp, _graph.Size);
    }

    [Fact]
    public void Delete_DoesNothingIf_KeyNotFound() {
        var exp = _graph.Size;
        var title = "RANDOM RANDOM";
        _graph.Delete(title);
        Assert.Equal(exp, _graph.Size);
    }

    [Fact]
    public void CanEnumerate() {
        PublicationGraph graph = new PublicationGraph();
        var fst = new PublicationDtoDetails { Title = "fst" };
        var snd = new PublicationDtoDetails { Title = "snd" };
        graph.Add(fst);
        graph.Add(snd);

        int exp = 2;
        int act = 0;
        foreach (PublicationNode n in graph.GetNodes()) act++;
        Assert.Equal(exp, act);
    }

    [Fact]
    public void CanEnumerateAllElements() {
        int i = 0;
        foreach (PublicationNode n in _graph.GetNodes()) i++;
        Assert.Equal(N, i);
    }

    // Parralel filtering of the entire graph
    //
    [Fact]
    public void CanFilterEntireGraph() {
        var expNodes = this._graph
            .GetNodes()
            .Where((t) => (t.Details.Title.Contains("1")));
        
        this._graph.Filter(tFilter);
        var actNodes = this._graph.GetNodes();
        
        foreach(var exp in expNodes) {
            Assert.Contains(exp, actNodes);
        }
    }
    
    [Fact]
    public void CanHistorySaveFilterChain() {
        // Copy below filters
        // Assert value of each filter is as expected
    }

    [Fact]
    public void CanLoadFromHistory() 
    {

        // Arrange
        var key = "1xDEADBEEF";
        
        var initial = new PublicationGraph();
        var actGraph = new PublicationGraph();
        foreach (var n in GetConnectedCycleData(100)) { actGraph.Add(n); initial.Add(n); }

        actGraph.Add(new PublicationDtoDetails { Title = key });
        var f = new POV(key, FilterOption.SearchDirection.BI | FilterOption.SearchDirection.VISIT_ONCE,
                    new TitleContains("1"));

        // Act
        actGraph.Filter(f);
        var exp = actGraph.GetNodes().ToList();
        
        // Assert state is correct before we serialize
        Assert.Single(exp);
        Assert.Equal(key, exp[0].Details.Title);
        string gs = actGraph.Serialize();

        // Assert
        initial.Load(gs);
        var act = initial.GetNodes().ToList();
        Assert.Single(act);
        Assert.Equal(key, act[0].Details.Title);
    }

    [Fact]
    public void CanCopy() {
        throw new NotImplementedException("CanCopy not implemented");
    }
}
