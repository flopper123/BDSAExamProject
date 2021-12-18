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

    // [Fact]
    // public void Can_Construct_From_StringAndDir() {
    //     POV pov = new POV("1", FilterOption.SearchDirection.BI);
        
    //     var exp_pargs = new Object[] { "1".ToTitle(), FilterOption.SearchDirection.BI};
    //     var act_pargs = pov.PredicateArgs;

    //     Assert.Equal(exp_pargs, pov.PredicateArgs);
    // }

    // [Fact]
    // public void Can_Construct_From_StringDirPrv() {
    //     POV inner = new POV("0");
    //     POV pov = new POV("1", FilterOption.SearchDirection.BI, inner);
        
    //     var exp_pargs = new Object[] { "1".ToTitle(), FilterOption.SearchDirection.BI};
    //     var act_pargs = pov.PredicateArgs;

    //     Assert.Equal(exp_pargs, pov.PredicateArgs);
    //     Assert.Contains(inner.Serialize(), pov.Serialize());
    // }
    
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

    // [Fact]
    // public void Can_Construct_From_TitleDirPrv() {
    //     POV inner = new POV("0".ToTitle());
    //     POV pov = new POV("1".ToTitle(), FilterOption.SearchDirection.BI, inner);
        
    //     var exp_pargs = new Object[] { "1".ToTitle(), FilterOption.SearchDirection.BI};
    //     var act_pargs = pov.PredicateArgs;

    //     Assert.Equal(exp_pargs, pov.PredicateArgs);
    //     Assert.Contains(inner.Serialize(), pov.Serialize());
    // }

    // [Theory]
    // [MemberData(nameof(GetPOVs))]
    // public void Can_PackAndConstruct(POV exp)
    // {
    //     var act = (POV) FilterFactory.Deserialize<PublicationGraph>(exp.Serialize());
    //     Assert.Equal(exp.Serialize(), act.Serialize());
    //     Assert.Equal(exp.Depth, act.Depth);
    //     Assert.Equal(exp.GetType(), act.GetType());
    //     Assert.Equal(exp.PredicateArgs, act.PredicateArgs);
    // }


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

