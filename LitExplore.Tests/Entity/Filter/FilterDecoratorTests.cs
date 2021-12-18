namespace LitExplore.Tests.Entity.Filter;

// Tests for filter decorations
public class FilterDecoratorTests
{
    List<string> data;

    public FilterDecoratorTests() {
        data = new List<string>() {
            "His Little Pony", "My Little Pony", "My Little Ox", "My Large Pony", "0xDEADBEEF"
        };
    }

    [Fact]
    public void CanChainFilters() {
        // Arrange
        Filter<List<string>> filter = new MockContainsFilter(
            "Pony",
            new MockContainsFilter(
                "Little"
            )
        );

        List<string> exp = new List<string> {
            data[0], data[1]
        };

        // Act
        filter.Invoke(data);

        // Assert
        Assert.Equal(exp, data);
    }

    [Fact]
    public void IsEmpytyWhenEverythingIsFilteredAway() {
        // Arrange
        Filter<List<string>> filter = new MockContainsFilter(
            "wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww"
        );

        IEnumerable<string> exp = new List<string> {};

        filter.Invoke(data);

        // Assert
        Assert.Equal(exp, data);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(30)]
    [InlineData(50)]
    [InlineData(1_000)]
    public void TestDepthOfChainedFilters(UInt32 exp) {

        Filter<List<string>> fAct = new MockContainsFilter("Pony");
        for (UInt32 i = 1; i < exp; i++) {
            fAct = new MockContainsFilter("Pony", fAct);
        }

        Assert.Equal(exp, (UInt32) fAct.GetHistory().Count());
        Assert.Equal(exp, fAct.Depth);
    }

    [Fact]
    public void TestDepthForEmptyFilter() {
        Assert.Equal(0UL, EmptyFilter<int>.Get().Depth);
    }
}

