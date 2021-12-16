namespace LitExplore.Tests.Util;

public class MockClearFilter : FilterDecorator<PublicationGraph> 
{
    public MockClearFilter() : base() {}

    public MockClearFilter(string key, Filter<PublicationGraph>? prv_ = null) : base(key, prv_)
    { }

    protected override void Action(PublicationGraph tar) { tar.Nodes.Clear(); }

    protected override IEnumerable<(string type, string str_vals)> getPArgsStringTuple()
    {
        yield return ("", "");
    }
}