namespace LitExplore.Tests.Entity.Filter;

using LitExplore.Core.Filter;
using LitExplore.Core.Filter.Filters;

public class FilterFactoryTests {
    
    public static IEnumerable<object[]> FilterCreateData() {
        yield return new Object[] {typeof(TitleContains), new Object[] {"0xDEADBEEF"} };
        yield return new Object[] {typeof(POV), new Object[] { new PublicationDtoTitle { Title = "0x"} } };
        yield return new Object[] {typeof(POV), new Object[] { new PublicationDtoTitle { Title = "3x"}, FilterOption.SearchDirection.PARENTS } } ;
    }

    [Theory]
    [MemberData(nameof(FilterCreateData))]
    public void Create_PublicationGraphFilter_by_Type(Type exp, params Object[] args) {
        Filter<PublicationGraph> act_filter = FilterFactory.Create<PublicationGraph>(exp.Name, args) ?? EmptyFilter<PublicationGraph>.Get();
        Assert.Equal(exp.Name, act_filter.GetType().Name);
        Assert.Equal(args[0], (act_filter as FilterDecorator<PublicationGraph>).PredicateArgs[0]);
    }

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