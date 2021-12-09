namespace LitExplore.Tests.Entity.Filter;

public class FilterEnumFactoryTests {
    [Theory]
    // Add inline data for test when u add new FilterTypes
    [InlineData(typeof(PublicationDto), FilterType.PUB)]
    [InlineData(typeof(String), FilterType.NONE)]
    [InlineData(typeof(UInt64), FilterType.NONE)]
    public void CanGetFilterTypeFromTypeExt(Type t, FilterType exp) {
        FilterType act = t.GetFilterType();
        Assert.Equal(exp, act);
    }


}