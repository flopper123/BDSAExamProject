namespace LitExplore.Tests.Core.Filter.Filters;

using static LitExplore.Tests.Core.Publication.PublicationGraphTests;
using static LitExplore.Core.Filter.FilterPArgField;
using static LitExplore.Tests.Utilities.GraphMockData;

// Tests for Filter<T> and EmptyFilter 
public class TitleContainsTests
{
    private static Filter<PublicationGraph> filter = new TitleContains("0x");
    private PublicationGraph graph;
    
    private readonly ITestOutputHelper output;

    public TitleContainsTests(ITestOutputHelper output)
    {
        this.output = output;
        this.graph = new PublicationGraph();
        foreach(var d in GetMockData()) graph.Add(d);
    }

    public static IEnumerable<PublicationDtoDetails> GetMockData() {
        yield return new PublicationDtoDetails { Title = "0xDEAD", References = GetHashSet("0xBEEF") };
        yield return new PublicationDtoDetails { Title = "0xBEEF", References = GetHashSet("0xDEADBEEF") };
        yield return new PublicationDtoDetails { Title = "0xDEADBEEF", References = GetHashSet("Different") };
        yield return new PublicationDtoDetails { Title = "Different" };
    }

    public static PublicationGraph GetMockGraph() {
        var g = new PublicationGraph();
        foreach (var n in GetMockData()) g.Add(n);
        return g;
    }

    [Fact]
    public void Can_Serialize_Single()
    {
        StringBuilder exp = new StringBuilder();
        // Start of all pargs
        exp.Append(FilterField.START);
        exp.Append($"{FilterField.START}{FilterField.NAME}{FilterField.VALUE_SEPERATOR}");
        exp.Append("LitExplore.Core.Filter.Filters.TitleContains");
        exp.Append($"{FilterField.FIELD_SEPERATOR}{FilterField.DEPTH}{FilterField.VALUE_SEPERATOR}");
        exp.Append("1");
        exp.Append($"{FilterField.FIELD_SEPERATOR}{FilterField.P_ARGS}{FilterField.VALUE_SEPERATOR}");
        // PArgs
        exp.Append(FilterField.START);
        exp.Append($"{LINE_START}{TYPE}{VALUE_SEPERATOR}System.String{FIELD_SEPERATOR}"
                  +$"{VALUE}{VALUE_SEPERATOR}0x{LINE_END}");
        // end of current parg
        exp.Append(FilterField.END); 
        // End of All pargs
        exp.Append(FilterField.END);

        // END of serialization
        exp.Append(FilterField.END);

        string act = filter.Serialize();
        Assert.Equal(exp.ToString(), act);
    }

    [Fact]
    public void Can_Deserialize_Single()
    {
        TitleContains exp = (TitleContains)filter;
        var act = (TitleContains) FilterFactory.Deserialize<PublicationGraph>(exp.Serialize());
        Assert.Equal(exp.Depth, act.Depth);
        Assert.Equal(exp.Serialize(), act.Serialize());
    }

    [Fact]
    public void Can_Apply_Deserialization_Single()
    {
        var data = GetMockData().ToList();
        var exp_titles = new List<string> { data[0].Title, data[1].Title, data[2].Title };

        // Construct filter from deserialization
        Filter<PublicationGraph> act_filter = FilterFactory
            .Deserialize<PublicationGraph>(filter.Serialize());
        
        this.graph.Filter(act_filter); // Apply filter

        // Assert correct count and values.
        var msg = new StringBuilder();
        msg.Append($"Couldn't find in expected list of dtos.");
        msg.Append("\nexp list: [");
        foreach (var n in this.graph.GetNodes())
        {
            msg.Append(n.Details.Title);
            msg.Append(" ");
            Assert.True(exp_titles.Contains(n.Details.Title), 
                        msg.ToString() + $"\nact: {n.Details.Title}");
        }

    }

    [Fact]
    public void Can_Serialize_Chain()
    {
        var f1 = filter;
        var f2 = new TitleContains("DE", f1);
        var f3 = new TitleContains("AD", f2);
        var f4 = new TitleContains("BEEF", f3);

        // Build expected serialization from filterfield enum
        StringBuilder exp = new StringBuilder();
        exp.Append(FilterField.START);
        exp.Append($"{f1}{Environment.NewLine}{f2}{Environment.NewLine}{f3}{Environment.NewLine}{f4}");
        exp.Append(FilterField.END);

        // Build actual serialization
        string act = f4.Serialize();
        
        Assert.Equal(exp.ToString(), act);
    }
}