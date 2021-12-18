namespace LitExplore.Tests.Entity.Filter;

using LitExplore.Core.Filter;
using LitExplore.Core.Filter.Filters;

public class FilterFactoryTests {
    //[Theory]
    //[InlineData(typeof(MaxDepthFilter), 10)]
    //[InlineData(typeof(TitleContainsFilter), "0xDEADBEEF")]
    //public void Create_PublicationNode_by_Type(Type exp, params Object[] args) {
    //    var act_filter = FilterFactory.Create<NodeDetails<PublicationNode>>(exp.Name, args);
    //    Assert.Equal(exp.Name, act_filter.GetType().Name);
    //}
    /*
    [Theory]
    [InlineData(typeof(TitleContains), "0xDEADBEEF")]
    public void Create_PublicationGraphFilter_by_Type(Type exp, params Object[] args) {
        Filter<PublicationGraph> act_filter = FilterFactory.Create<PublicationGraph>(exp.Name, args);
        Assert.Equal(exp.Name, act_filter.GetType().Name);
    }
    */

    // [Theory]
    // [InlineData("TitleContains", "0xDEADBEEF")]
    // public void Create_by_UnqualifiedName(string exp, params Object[] args) {
    //     Filter<PublicationGraph> act_filter = FilterFactory.Create<PublicationGraph>(exp, args);
    //     Assert.Equal(exp, act_filter.GetType().Name);
    // }

    [Fact]
    public void Create_Throws_NameFalse() {
        Assert.Throws<ArgumentException>(
            () => FilterFactory.Create<PublicationDto>("0xDEADBEEF", new Object{})
        );
    }

    [Fact]
    public void Create_Throws_ArgsFalse() {
        Assert.Throws<ArgumentException>(
            () => FilterFactory.Create<PublicationGraph>(typeof(TitleContains).Name, new Object{})
        );
    }

    // TO:DO implement
    [Fact]
    public void AssertReflective_DeserializePArgs_ReturnsStr_Str_tuple() 
    {
        Assert.True(true);
    }

    [Fact]
    public void AssertReflective_DeserializePArgs_ReceivesOneString()
    {
        Assert.True(true);
    }

    [Fact]
    public void AssertReflective_ConcreteTypesHaveStaticMethod_DeserializePArgs()
    {
        Assert.True(true);
    }
}