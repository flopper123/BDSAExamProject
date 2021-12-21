namespace LitExplore.Controllers;

using LitExplore.Controllers.Filter;
using LitExplore.Entity.Context;
using LitExplore.Controllers.Graph;
using LitExplore.Core.Filter;

public class GraphController
{
    FilterController _fController;
    private readonly IPublicationRepository _pRepo;

    public GraphController(IPublicationRepository pRepo,
                            IFilterRepository<PublicationGraph> fRepo)
    {
        this._fController = new FilterController(fRepo);
        this._pRepo = pRepo;
    }

    /// <summary>
    /// Parse user input filter to pargs
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pargs"></param>
    /// <returns></returns>
    public Filter<PublicationGraph> Filter(string name, string pargs) {
       return _fController.ParseFilter(name, pargs);
    }

    // At some point this method should build the graph dynamicly
    // as the data is loaded from the repository 
    public async Task<VisualGraph> GetDefaultGraphAsync()
    {
        VisualGraph graph = new VisualGraph();

        await foreach (var n in _pRepo.ReadAllAsync()) graph.Add(n);
        graph.OnInit();

        return graph;
    }

    public async Task<VisualGraph> Load(UInt64 uid = 0)
    {

        // Retrieve default graph
        var graph = await GetDefaultGraphAsync();

        // if not found filter becomes EmptyFilter<PubGraph> which is fine
        Filter<PublicationGraph> f = await _fController.ReadAsync(uid);

        if (f.GetType() != typeof(EmptyFilter<PublicationGraph>)) graph.Filter(f);
        
        return graph;
    }
}