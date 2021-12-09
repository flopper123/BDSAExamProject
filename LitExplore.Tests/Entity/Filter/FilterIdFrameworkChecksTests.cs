namespace LitExplore.Tests.Entity.Filter;

using System.Reflection;

public class MockFilter : FilterDecorator<int>
{
    public MockFilter() : base(n => true) { }

    public override EFilter GetId()
    {
        return EFilter.NONE;
    }
}


public class FilterIdFrameworkChecksTests
{
    [Fact]
    // We expect this to throw an exception, since there is defined a Filter in 
    // filter tests without the framework specifications
    public void AssertStaticIdVariable_Fails()
    {
        Assert.ThrowsAny<Exception>(
            () => FilterIdFrameworkChecks.Assert(Assembly.Load("LitExplore.Tests"))
        );
    }
}
