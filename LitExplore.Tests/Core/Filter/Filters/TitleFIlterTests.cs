namespace LitExplore.Tests.Core.Filter;

using Newtonsoft.Json;
using LitExplore.Core;
using LitExplore.Core.Filter;
using LitExplore.Core.Filter.Filters;
using System.Text;
using Xunit.Abstractions;

using static LitExplore.Core.Filter.FilterPArgField;

// Tests for Filter<T> and EmptyFilter 
public class TitleFilterTest
{
    private Filter<PublicationDto> filter;
    private IList<PublicationDto> pubData;
    
    private readonly ITestOutputHelper output;

    public TitleFilterTest(ITestOutputHelper output)
    {
        this.output = output;

        pubData = new List<PublicationDto>() {
            new PublicationDto { Title = "0xDEAD", References = new HashSet<PublicationTitle>()},
            new PublicationDto { Title = "BEEF", References = new HashSet<PublicationTitle>()},
            new PublicationDto { Title = "0xDEADBEEF", References = new HashSet<PublicationTitle>()}
        };
        filter = new TitleFilter("0x");
    }

    [Fact]
    public void CanStringify()
    {
        StringBuilder exp = new StringBuilder();
        // Start of all pargs
        exp.Append(FilterField.START);
        exp.Append($"{FilterField.START}{FilterField.NAME}{FilterField.VALUE_SEPERATOR}");
        exp.Append("LitExplore.Core.Filter.Filters.TitleFilter");
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
        TitleFilter exp = (TitleFilter)filter;
        var act = (TitleFilter) FilterFactory.Deserialize<PublicationDto>(exp.Serialize());
        Assert.Equal(exp.Depth, act.Depth);
        Assert.Equal(exp.Serialize(), act.Serialize());
    }

    [Fact]
    public void CanApplyDeserialization()
    {
        var exp = new List<PublicationDto> { pubData[0], pubData[2] };
        var act = (TitleFilter) FilterFactory.Deserialize<PublicationDto>(filter.Serialize()); 
        foreach (var dto in act.Apply(pubData)) {
            Assert.True(exp.Contains(dto), $"Couldn't find {dto} in expected list of dtos.");
        }
    }

    [Fact]
    public void CanStringifyChain()
    {
        var f1 = this.filter;
        var f2 = new TitleFilter("DE", f1);
        var f3 = new TitleFilter("AD", f2);
        var f4 = new TitleFilter("BEEF", f3);
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
