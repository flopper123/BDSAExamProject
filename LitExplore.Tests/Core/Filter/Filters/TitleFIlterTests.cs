namespace LitExplore.Tests.Core.Filter;

using Newtonsoft.Json;
using LitExplore.Core;
using LitExplore.Core.Filter;

// Tests for Filter<T> and EmptyFilter 
public class TitleFilterTest
{
    private Filter<PublicationDto> filter;
    private IList<PublicationDto> pubData;

    public TitleFilterTest()
    {
        pubData = new List<PublicationDto>() {
            new PublicationDto { Title = "0xDEAD", References = new HashSet<ReferenceDto>()},
            new PublicationDto { Title = "BEEF", References = new HashSet<ReferenceDto>()},
            new PublicationDto { Title = "0xDEADBEEF", References = new HashSet<ReferenceDto>()}
        };
        filter = new TitleFilter("0x");
    }

    //JsonConvert.SerializeObject(YourPOCOHere, Formatting.Indented, 
    //new JsonSerializerSettings 
    //{ 
    //        ReferenceLoopHandling = ReferenceLoopHandling.Serialize
    //});

    [Fact]
    public void CanStringify()
    {
        string exp = "NOT_IMPLEMENTED"; // insert string literal
        string act = filter.Serialize();
        Assert.Equal(exp, act);
    }

    // [Fact]
    // public void CanDeserializeJson()
    // {
    //     TitleFilter exp = (TitleFilter)filter;
    //     var act = (TitleFilter?)JsonConvert.DeserializeObject(exp.ToJson());
    //     Assert.NotNull(act);
    //     if (act == null) return;
    //     Assert.Equal(exp.Depth, act.Depth);
    //     Assert.Equal(exp.GetHashCode(), act.GetHashCode());
    // }

    [Fact]
    public void CanStringifyChain()
    {
        Filter<PublicationDto> f_act =
            new TitleFilter("BEEF",
                new TitleFilter("AD",
                    new TitleFilter("DE", this.filter)
            ));
        string act = f_act.Serialize();
        string exp = "NOT_IMPLEMENTED";
        Assert.True(false, act);
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
