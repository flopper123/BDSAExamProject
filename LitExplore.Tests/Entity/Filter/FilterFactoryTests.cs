namespace LitExplore.Tests.Entity.Filter;

using LitExplore.Core.Filter;
using LitExplore.Core.Filter.Filters;

public class FilterFactoryTests {
    [Theory]
    [InlineData(typeof(MinRefsFilter), 10)]
    [InlineData(typeof(TitleFilter), "0xDEADBEEF")]
    public void Construction_Success(Type exp, params Object[] args) {
        Filter<PublicationDto> act_filter = FilterFactory.Create<PublicationDto>(exp.Name, args);
        Assert.Equal(exp, act_filter.GetType());
    }

    [Fact]
    public void Construction_Throws_NameFalse() {
        Assert.Throws<ArgumentException>(
            () => FilterFactory.Create<PublicationDto>("0xDEADBEEF", null)
        );
    }

    [Fact]
    public void Construction_Throws_ArgsFalse() {
        Assert.Throws<ArgumentException>(
            () => FilterFactory.Create<PublicationDto>(typeof(TitleFilter).Name, null)
        );
    }
}