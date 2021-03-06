namespace LitExplore.Tests.Core.Filter.Filters;

using Newtonsoft.Json;
using LitExplore.Core;
using LitExplore.Core.Filter;
using LitExplore.Core.Filter.Filters;
using System.Text;
using Xunit.Abstractions;
using LitExplore.Tests.Core.Publication;

using static LitExplore.Core.Filter.FilterPArgField;
using static LitExplore.Tests.Utilities.GraphMockData;

// Tests for Filter<T> and EmptyFilter 
public class POVTests
{
    public static IEnumerable<Object[]> GetPOVs() {
        yield return new Object[] { new POV("Inner") };
        yield return new Object[] { new POV("Outer", new POV("Inner")) };
        yield return new Object[] { new POV("Outer", FilterOption.SearchDirection.DEFAULT, new POV("Inner")) };
        yield return new Object[] { new POV("Inner".ToTitle()) };
        yield return new Object[] { new POV("Outer".ToTitle(), new POV("Inner".ToTitle())) };
        yield return new Object[] { new POV("Outer".ToTitle(), FilterOption.SearchDirection.DEFAULT, new POV("Inner".ToTitle())) };
    }

    [Fact]
    public void Can_Construct_From_String() {
        POV pov = new POV("1");
        var exp_pargs = new Object[] { "1".ToTitle(), FilterOption.SearchDirection.DEFAULT };
        var act_pargs = pov.PredicateArgs;
        Assert.Equal(exp_pargs, pov.PredicateArgs);
    }

    [Fact]
    public void Can_Construct_From_StringAndDir() {
        POV pov = new POV("1", FilterOption.SearchDirection.BI); 
        var exp_pargs = new Object[] { "1".ToTitle(), FilterOption.SearchDirection.BI};
        Assert.Equal(exp_pargs, pov.PredicateArgs);
    }

    [Fact]
    public void Can_Construct_From_StringDirPrv() {
        POV inner = new POV("0");
        POV pov = new POV("1", FilterOption.SearchDirection.BI, inner);
        
        var exp_pargs = new Object[] { "1".ToTitle(), FilterOption.SearchDirection.BI};
        var act_pargs = pov.PredicateArgs;

        Assert.Equal(exp_pargs, pov.PredicateArgs);
        Assert.Contains(inner.Serialize().Replace("}", String.Empty), pov.Serialize());
    }
    
    [Fact]
    public void Can_Construct_From_Title() {
        POV pov = new POV("1".ToTitle());
        
        var exp_pargs = new Object[] { "1".ToTitle(), FilterOption.SearchDirection.DEFAULT};
        var act_pargs = pov.PredicateArgs;

        Assert.Equal(exp_pargs, pov.PredicateArgs);

    }

    [Fact]
    public void Can_Construct_From_TitleAndDir() {
        POV pov = new POV("1".ToTitle(), FilterOption.SearchDirection.BI);
        
        var exp_pargs = new Object[] { "1".ToTitle(), FilterOption.SearchDirection.BI};
        var act_pargs = pov.PredicateArgs;

        Assert.Equal(exp_pargs, pov.PredicateArgs);
    }

    [Fact]
    public void Can_Construct_From_TitleDirPrv() {
        POV inner = new POV("0".ToTitle());
        POV pov = new POV("1".ToTitle(), FilterOption.SearchDirection.BI, inner); 
        var exp_pargs = new Object[] { "1".ToTitle(), FilterOption.SearchDirection.BI};
        Assert.Equal(exp_pargs, pov.PredicateArgs);
        Assert.Contains(inner.Serialize().Replace("}", String.Empty), pov.Serialize());
    }

    [Theory]
    [MemberData(nameof(GetPOVs))]
    public void Can_PackAndConstruct(POV exp)
    {
        var act = (POV) FilterFactory.Deserialize<PublicationGraph>(exp.Serialize());
        Assert.Equal(exp.Serialize(), act.Serialize());
        Assert.Equal(exp.Depth, act.Depth);
        Assert.Equal(exp.GetType(), act.GetType());
        Assert.Equal(exp.PredicateArgs, act.PredicateArgs);
    }
    
    [Theory]
    [InlineData(FilterOption.SearchDirection.CHILDREN | FilterOption.SearchDirection.VISIT_MINDEPTH)]
    [InlineData(FilterOption.SearchDirection.BI | FilterOption.SearchDirection.VISIT_MINDEPTH)]
    [InlineData(FilterOption.SearchDirection.CHILDREN | FilterOption.SearchDirection.VISIT_ONCE)]
    [InlineData(FilterOption.SearchDirection.BI | FilterOption.SearchDirection.VISIT_ONCE)]
    public void Can_POV_VISIT_CHILDREN_Search_Everything_AcyclicConnected(FilterOption.SearchDirection opts)
    {
        var act = new PublicationGraph();        
        foreach (var n in GetConnectedAcyclicData(100)) act.Add(n);
        var exp = act.Size;
        Assert.NotEqual(0, exp);
        Assert.NotEqual(1, exp);
        act.Filter(new POV("0", opts));
        Assert.Equal(exp, act.GetNodes().Count());
    }

    [Theory]
    [InlineData(FilterOption.SearchDirection.PARENTS | FilterOption.SearchDirection.VISIT_MINDEPTH)]
    [InlineData(FilterOption.SearchDirection.PARENTS | FilterOption.SearchDirection.VISIT_ONCE)]
    [InlineData(FilterOption.SearchDirection.DEFAULT)]
    public void Can_POV_VISIT_PARENTS_Search_Everything_cyclic(FilterOption.SearchDirection opts)
    {
        var act = new PublicationGraph();        
        foreach (var n in GetConnectedCycleData(100)) act.Add(n);
        var exp = act.Size;

        var exp_nodes = act.GetNodes()
                           .Select(n => n.Details.Title)
                           .ToList();
                           
        act.Filter(new POV(0.ToString(), opts));

        var act_titles = act.GetNodes()
                            .Select(n => n.Details.Title)
                            .ToList();

        foreach (var n in exp_nodes) {
            Assert.Contains(n, act_titles);
        }
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

