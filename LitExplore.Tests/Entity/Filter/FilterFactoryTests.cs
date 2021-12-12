namespace LitExplore.Tests.Entity.Filter;

using LitExplore.Core.Filter;
using LitExplore.Core.Filter.Filters;

public class FilterFactoryTests {
    [Theory]
    [InlineData(typeof(MinRefsFilter), 10)]
    [InlineData(typeof(TitleFilter), "0xDEADBEEF")]
    public void Create_by_Type(Type exp, params Object[] args) {
        Filter<PublicationDto> act_filter = FilterFactory.Create<PublicationDto>(exp.Name, args);
        Assert.Equal(exp.Name, act_filter.GetType().Name);
    }

    [Theory]
    [InlineData("MinRefsFilter", 10)]
    [InlineData("TitleFilter", "0xDEADBEEF")]
    public void Create_by_UnqualifiedName(string exp, params Object[] args) {
        Filter<PublicationDto> act_filter = FilterFactory.Create<PublicationDto>(exp, args);
        Assert.Equal(exp, act_filter.GetType().Name);
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
            () => FilterFactory.Create<PublicationDto>(typeof(TitleFilter).Name, new Object{})
        );
    }

    public IEnumerable<Filter<PublicationDto>> GetDtoFilters() {
        yield return new TitleFilter("0xDEAD");
        yield return new TitleFilter("0xDEAD", new TitleFilter("BEEF"));
        yield return new MinRefsFilter(10);
    }

    [Fact]
    public void CanDeserialize_TitleFilter() {
        // Act
        TitleFilter exp = new TitleFilter("0xDEAD");
        string fs = exp.Serialize();

        Filter<PublicationDto> act = FilterFactory.Deserialize<PublicationDto>(fs);

        Assert.Equal(exp.GetType(), act.GetType());
        Assert.Equal(exp.Depth, act.Depth);
    }

    [Fact]
    public void CanDeserialize_MinRefsFilter() {
        // Act
        MinRefsFilter exp = new MinRefsFilter(10);
        string fs = exp.Serialize();

        Filter<PublicationDto> act = FilterFactory.Deserialize<PublicationDto>(fs);

        Assert.Equal(exp.GetType(), act.GetType());
        Assert.Equal(exp.Depth, act.Depth);
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