namespace LitExplore.Tests.Entity.Filter;

using LitExplore.Core.Filter;
using LitExplore.Tests.Core.Filter.Filters;

// Tests for Filter<T> and EmptyFilter 
public class FilterTests
{

    [Fact]
    public void IsFilterHistory_OrderCorrect_Single() {
        // Act
        var f = new TitleContains("0");

        var act = (f.GetHistory()).GetEnumerator();

        // Assert actual
        Assert.True(act.MoveNext(), "Failed to move to first enumeration");
        Assert.Equal(f, act.Current);
        Assert.False(act.MoveNext());
    }

    [Fact]
    public void IsFilterHistory_OrderCorrect_Chain() {
        // Act
        var f = new TitleContains("0");
        var f2 = new TitleContains("1", f);

        var act = (f2.GetHistory()).GetEnumerator();

        // Assert actual
        Assert.True(act.MoveNext(), "Failed to move to first enumeration");
        Assert.Equal(f, act.Current);
        Assert.True(act.MoveNext());
        Assert.Equal(f2, act.Current);
        Assert.False(act.MoveNext());
    }

    // Asserts a full cycle of the param graph filters, by
    // first constructing a new filter through Deserialize<PubGraph>(f.Serialize()), 
    // and then applying it to target.
    //
    // Assertion is done by checking that tar contains excatly the same values as in exp_titles,
    // disregarding order of elements.
    [Theory]
    [MemberData(nameof(TitleContainsTests.GetChainApplyData), parameters: 3)]
    public void Is_GraphFilters_PackableAndAppliable(Filter<PublicationGraph> f, 
                                                    PublicationGraph tar, 
                                                    List<string> exp_titles) 
    {
        string fs = f.Serialize();
        Filter<PublicationGraph> act_filter = FilterFactory.Deserialize<PublicationGraph>(fs);
        tar.Filter(f);
        foreach (var n in tar.GetNodes()) {
            Assert.Contains(n.Details.Title, exp_titles);
        }
    }
}