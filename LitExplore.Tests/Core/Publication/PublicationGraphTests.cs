namespace LitExplore.Tests.Core.Publication;

using LitExplore.Core.Graph;
using LitExplore.Core.Filter;
using System.Text;

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

    internal static HashSet<PublicationDtoTitle> GetHashSet(params string[] titles) {
        HashSet<PublicationDtoTitle> set = new HashSet<PublicationDtoTitle>(titles.Length);
        foreach (string t in titles) set.Add(new PublicationDtoTitle { Title = t });
        return set;
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

    /// 
    /// Created connected Cycle data by adding one child to the previous object,
    /// and setting the N'th child to point to fst.
    ///  
    ///  if N=1            if N = 3
    ///   _____       _________________
    ///  |     |     |                 |
    ///  v     |     v                 |
    ///  * --> *     * --> * --> * --> *
    ///  
    ///  The Title of the n'th node = n.ToString()
    ///  
    public static IEnumerable<PublicationDtoDetails> GetConnectedCycleData(int N) 
    {
        for (int i = 0; i < N; i++)
        {
            yield return new PublicationDtoDetails
            {
                Title = i.ToString(),
                References = GetHashSet($"{i + 1}"),
            };
        }

        yield return new PublicationDtoDetails { Title = $"{N}", References = GetHashSet("1") };
    }

    /// 
    /// Repeats the following pattern N times:
    ///   --> * -->
    /// * --> * --> *
    ///   --> * -->
    ///        
    ///  Where the objects @second row of * constitutes the @childCOunt                   
    internal static IEnumerable<PublicationDtoDetails> GetConnectedAcyclicData(int repeat, int childCount = 3) 
    {
        for (int d = 0; d < repeat; d++)
        {
            var refs = new HashSet<PublicationDtoTitle>();
            for (int ci = 0; ci < childCount; ci++)
            {
                refs.Add(new PublicationDtoTitle { Title = $"{d}{ci}" });
                yield return new PublicationDtoDetails { Title = $"{d}{ci}", References = GetHashSet($"{d+1}") };
            }
            yield return new PublicationDtoDetails { Title = $"{d}", References = refs };
        }
    }

    [Fact]
    public void CanGet() 
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
    public void CanGet_ReturnsNull_KeyNotFound() {
        Assert.Null(_graph.GetNode("Something we would never put into a graph"));
    }

    [Fact]
    public void EmptyGraph_HasSize0() {
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
        throw new NotImplementedException();
        //var expNodes = this._graph
        //    .GetNodes()
        //    .Where((t) => !tFilter.ShouldRemove(t.ToNodeDetails()));
        //
        //this._graph.Filter(tFilter);
        //var actNodes = this._graph.GetNodes();
        //
        //foreach(var exp in expNodes) {
        //    Assert.Contains(exp, actNodes);
        //}
    }


    [Fact]
    public void CanBranchFilterOn_DisconnectedRoot_OnlyRemovesRoot() {
        throw new NotImplementedException();

        //var exp = this._graph.Size - 1;
//
        //var fclear = new MockClearFilter();
        //var disconnected = "Disconnected";
        //// assert its contained 
        //Assert.Single(this._graph.GetNodes().Where(t => t.Details.Title.Equals(disconnected)));
        //this._graph.FilterBranch(fclear, new PublicationDtoTitle { Title = disconnected });
        //var act = this._graph.Size;
        //Assert.Equal(exp, act);
    }

    [Fact]
    public void CanBranchFilterEverything_ConnectedBranch()
    {
        throw new NotImplementedException();
        //var act = new PublicationGraph();
        //var fclear = new MockClearFilter();
        //
        //foreach (var n in GetConnectedAcyclicData(10, 2)) act.Add(n);
        //
        //act.FilterBranch(fclear, new PublicationDtoTitle { Title = "0" });
        //Assert.Equal(0, act.GetNodes().Count());
    }

    [Fact]
    public void CanBranchFilterByDepth_ConnectedBranch()
    {
        throw new NotImplementedException();
        //var exp_titles = new List<string> { "0", "1", "2", "3" };
        //var act = new PublicationGraph();
        //var fdepth = new MaxDepthFilter(4);
        //foreach (var n in GetConnectedCycleData(50)) act.Add(n);
        //act.FilterBranch(fdepth, new PublicationDtoTitle { Title = "0" });
        //foreach (var n in act.GetNodes()) Assert.Contains(n.Details.Title, exp_titles);
    }
}
