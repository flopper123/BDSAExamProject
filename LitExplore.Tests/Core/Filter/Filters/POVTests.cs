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
    public void Can_POV_ChildrenSearch_ReachEverything_Connected()
    {
        var act = new PublicationGraph();        
        foreach (var n in GetConnectedAcyclicData(100, 11)) act.Add(n);
        
        act.Filter(new POV("0"));
        Assert.Equal(0, act.GetNodes().Count());
    }
}

