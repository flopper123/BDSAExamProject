namespace LitExplore.Tests.Entity.Filter;

class FilterEven : FilterDecorator<int> {
    public FilterEven() : base(n => n % 2 == 0) {}
    
    public override EFilter GetId() {
        return EFilter.NONE;
    }
}

// Tests for Filter<T> and EmptyFilter 
public class FilterTests
{
    private Filter<int> filter;

    public FilterTests() {
        filter = new FilterEven();
    }
    
    // Apply filter
    [Theory]
    [InlineData(
        new int[] { 0, 1, 2, 3, 4, 5, 6 },
        new int[] { 0, 2, 4, 6 }
    )]
    [InlineData(
        new int[] { 9, 3, 3, 6, 6, 2, 1 },
        new int[] { 6, 6, 2 }
    )]
    [InlineData(
        new int[] { },
        new int[] { }
    )]
    [InlineData(
        new int[] { 1, 3, 5 },
        new int[] { }
    )]
    public void CanApplyFilterToInts(int[] input, int[] exp) {

        // Act
        int[] act = filter.Apply(input).ToArray<int>();

        // Assert
        Assert.Equal(exp, act);
    }

    [Fact]
    public void OwnFilterIsLastOfFilterHistory() {
        // Act
        var act = filter.GetHistory().GetEnumerator();
        
        // Assert actual
        Assert.True(act.MoveNext(), "Failed to move to first enumeration");
        Assert.Equal(filter, act.Current);
    }    
}