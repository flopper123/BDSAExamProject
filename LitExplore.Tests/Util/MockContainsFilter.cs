namespace LitExplore.Tests.Util;

class MockContainsFilter : FilterDecorator<List<string>>
{
    public MockContainsFilter(string key, Filter<List<string>>? prv_ = null) : base(key, prv_)
    { }

    protected override void Action(List<string> tar)
    {
        var removals = new List<string>();
        foreach (var t in tar) if (!t.Contains(p_args)) removals.Add(t);
        foreach (var del in removals) tar.Remove(del);
    }

    protected override IEnumerable<(string type, string str_vals)> getPArgsStringTuple()
    {
        yield return ("System.String", (p_args ?? "null").ToString());
    }
}