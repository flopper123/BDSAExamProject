namespace LitExplore.Tests.Core.Publication;

using LitExplore.Core.Graph;
using LitExplore.Core.Filter;

public class PublicationGraphTests
{
    PublicationGraph _graph = new PublicationGraph();
    TitleContainsFilter tFilter = new TitleContainsFilter("1");
    MaxDepthFilter dFilter = new MaxDepthFilter(2);
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

    public static IEnumerable<PublicationDtoDetails> GetPublications()
    {
        // roots
        yield return new PublicationDtoDetails { Title = "Alone" };
        yield return new PublicationDtoDetails { Title = "0", References = GetHashSet("2", "3") };
        yield return new PublicationDtoDetails { Title = "1", References = GetHashSet("2", "3") };
        yield return new PublicationDtoDetails { Title = "4", References = GetHashSet("5", "6") };

        // children
        yield return new PublicationDtoDetails { Title = "2", References = GetHashSet("3", "5") };
        yield return new PublicationDtoDetails { Title = "3", References = GetHashSet("7", "8") };
        yield return new PublicationDtoDetails { Title = "5", References = GetHashSet("6") };
        yield return new PublicationDtoDetails { Title = "6", References = GetHashSet("7", "8", "10") };
        yield return new PublicationDtoDetails { Title = "7", References = GetHashSet("10") };
        yield return new PublicationDtoDetails { Title = "8", References = GetHashSet("10") };
        yield return new PublicationDtoDetails { Title = "10" };
        yield return new PublicationDtoDetails { Title = "11" };
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
        throw new NotSupportedException();
        /*
        PublicationGraph graph = new PublicationGraph();
        var fst = new PublicationDtoDetails { Title = "fst" };
        var snd = new PublicationDtoDetails { Title = "snd" };
        graph.Add(fst);
        graph.Add(snd);

        int exp = 2;
        int act = 0;
        foreach (var n in _graph) act++;
        Assert.Equal(exp, act);
        */
    }

    [Fact]
    public void CanEnumerateAllElements() {
        throw new NotSupportedException();
    /*
        int i = 0;
        foreach (var n in _graph) i++;
        Assert.Equal(N, i);
    */
    }

    // Parralel filtering of the entire graph
    //
    [Fact]
    public void CanFilterEntireGraph() {
        throw new NotImplementedException();
    }

    [Fact]
    public void CanFilterBranch() {
        throw new NotImplementedException();
    }
}
