namespace LitExplore.Controllers.Filter;

using LitExplore.Controllers.Graph;
using LitExplore.Core.Filter;

public class FilterController
{
    IFilterRepository<PublicationGraph> _fRepo;

    public FilterController(IFilterRepository<PublicationGraph> fRepo)
    {
        _fRepo = fRepo;
    }

    public async Task<Filter<PublicationGraph>> ReadAsync(UInt64 uid) {
        return (await _fRepo.ReadAsync(uid)) ?? EmptyFilter<PublicationGraph>.Get();
    }

    /// <summary>
    /// Parse user input filter to pargs
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pargs"></param>
    /// <returns></returns>
    public Filter<PublicationGraph> ParseFilter(string name, string pargs)
    {
        // TO:DO should insert some error handling here :))
        (string name, object[] pargs) fcons = FilterParser.Parse(name, pargs);
        return (Filter<PublicationGraph>)FilterFactory.Create<PublicationGraph>(fcons.name, fcons.pargs);
    }
}