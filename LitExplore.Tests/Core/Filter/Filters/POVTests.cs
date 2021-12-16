namespace LitExplore.Tests.Core.Filter.Filters;

using Newtonsoft.Json;
using LitExplore.Core;
using LitExplore.Core.Filter;
using LitExplore.Core.Filter.Filters;
using System.Text;
using Xunit.Abstractions;
using LitExplore.Tests.Core.Publication;
using static LitExplore.Tests.Core.Publication.PublicationGraphTests;
using static LitExplore.Core.Filter.FilterPArgField;
/*
// Tests for Filter<T> and EmptyFilter 
public class POVTests
{
    private Filter<PublicationGraph> filter;
    private PublicationGraph graph;
    
    private readonly ITestOutputHelper output;

    public POVTests(ITestOutputHelper output)
    {
        this.filter = new TitleContains("0x");
        this.output = output;
        this.graph = new PublicationGraph();
        foreach(var d in GetMockData()) graph.Add(d);
    }

    private IEnumerable<PublicationDtoDetails> GetMockData() {
        yield return new PublicationDtoDetails { Title = "0xDEAD", References = GetHashSet("BEEF") };
        yield return new PublicationDtoDetails { Title = "0xBEEF", References = GetHashSet("0xDEADBEEF") };
        yield return new PublicationDtoDetails { Title = "0xDEADBEEF", References = GetHashSet("Different") };
        yield return new PublicationDtoDetails { Title = "Different" };
    }

    [Fact]
    public void CanStringify()
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
    public void CanDeserialize()
    {
        TitleContains exp = (TitleContains)filter;
        var act = (TitleContains) FilterFactory.Deserialize<PublicationDto>(exp.Serialize());
        Assert.Equal(exp.Depth, act.Depth);
        Assert.Equal(exp.Serialize(), act.Serialize());
    }

    [Fact]
    public void CanApplyDeserialization()
    {
        var exp = new List<PublicationDto> { graph[0], graph[2] };
        var act = (TitleContains) FilterFactory.Deserialize<PublicationDto>(filter.Serialize()); 
        foreach (var dto in act.Apply(graph)) {
            Assert.True(exp.Contains(dto), $"Couldn't find {dto} in expected list of dtos.");
        }
    }

    [Fact]
    public void CanStringifyChain()
    {
        var f1 = this.filter;
        var f2 = new TitleContains("DE", f1);
        var f3 = new TitleContains("AD", f2);
        var f4 = new TitleContains("BEEF", f3);
        StringBuilder exp = new StringBuilder();
        exp.Append(FilterField.START);
        exp.Append($"{f1}{Environment.NewLine}{f2}{Environment.NewLine}{f3}{Environment.NewLine}{f4}");
        exp.Append(FilterField.END);

        string act = f4.Serialize();
        Assert.Equal(exp.ToString(), act);
    }

    [Fact]
    public void OwnFilterIsLastOfFilterHistory()
    {
        // Act
        var act = filter.GetHistory().GetEnumerator();

        // Assert actual
        Assert.True(act.MoveNext(), "Failed to move to first enumeration");
        Assert.Equal(filter, act.Current);
    }
}

*/