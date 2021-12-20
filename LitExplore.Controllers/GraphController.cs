namespace LitExplore.Controllers;

using LitExplore.Entity.Context;
using LitExplore.Controllers.Graph;
using LitExplore.Core.Filter;

public class GraphController
{
    IFilterRepository<PublicationGraph> _fRepo;
    IPublicationRepository _pRepo;

    public GraphController() {}
    public GraphController(IFilterRepository<PublicationGraph> fRepo,
                           IPublicationRepository pRepo)
    {
        this._fRepo = fRepo;
        this._pRepo = pRepo;
    }

    // At some point this method should build the graph dynamicly
    // as the data is loaded from the repository 
    public async Task<VisualGraph> GetDefaultGraphAsync()
    {
        VisualGraph graph = new VisualGraph();

        
        //await foreach (var n in _pRepo.ReadAllAsync()) graph.Add(n);
        GraphMockData.GetPublications().ForEach(pub => graph.Add(pub));

        graph.OnInit();

        return graph;
    }

    public async Task<VisualGraph> Load(UInt64 uid = 0)
    {

        // Retrieve default graph
        var graph = await GetDefaultGraphAsync();

        // if not found filter becomes EmptyFilter<PubGraph> which is fine
        Filter<PublicationGraph>? f = await _fRepo.ReadAsync(uid);

        if (f != null) graph.Filter(f);
        return graph;
    }
}