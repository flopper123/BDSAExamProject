namespace LitExplore.Tests.Core.Filter.Filters;

using Newtonsoft.Json;
using LitExplore.Core;
using LitExplore.Core.Filter;
using LitExplore.Core.Filter.Filters;
using System.Text;
using Xunit.Abstractions;
using LitExplore.Tests.Core.Publication;

using static LitExplore.Tests.Utilities.GraphMockData;

// Tests for Filter<T> and EmptyFilter 
public class POVTests
{
    [Fact]
    public void Can_POV_VISIT_ONCE_ChildrenSearch_ReachEverything_Connected()
    {
        var act = new PublicationGraph();        
        foreach (var n in GetConnectedAcyclicData(100, 11)) act.Add(n);
        var exp = act.Size;

        var opts = FilterOption.SearchDirection.CHILDREN | FilterOption.SearchDirection.VISIT_ONCE;

        act.Filter(new POV("0", opts));
        Assert.Equal(exp, act.GetNodes().Count());
    }

    [Fact]
    public void Assert_POV_ParentSearch_OnRoot_Size1()
    {
        var act = new PublicationGraph();        
        foreach (var n in GetConnectedAcyclicData(100, 11)) act.Add(n);
        var exp = 1;

        var opts = FilterOption.SearchDirection.PARENTS | FilterOption.SearchDirection.VISIT_ONCE;

        act.Filter(new POV("0", opts));
        Assert.Equal(exp, act.GetNodes().Count());
    }
}

