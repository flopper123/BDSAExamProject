namespace LitExplore.Tests.Entity.Filter;

using LitExplore.Entity.Filter;

public class FilterFactoryTests {
    [Theory]
    [InlineData(typeof(MinRefsFilter), 10)]
    [InlineData(typeof(TitleFilter), "0xDEADBEEF")]
    public void Construction_Success(Type exp, params Object[] args) {
        Filter<PublicationDto> act_filter = Filter.Create<PublicationDto>(exp.Name, args);
        Assert.Equal(exp, act_filter.GetType());
    }

    [Fact]
    public void Construction_Throws_NameFalse() {
        Assert.Throws<ArgumentException>(
            () => Filter.Create<PublicationDto>("0xDEADBEEF", null)
        );
    }

    [Fact]
    public void Construction_Throws_ArgsFalse() {
        Assert.Throws<ArgumentException>(
            () => Filter.Create<PublicationDto>(typeof(TitleFilter).Name, null)
        );
    }
}