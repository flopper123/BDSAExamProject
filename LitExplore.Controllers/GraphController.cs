namespace LitExplore.Controllers;

using LitExplore.Entity.Context;
using LitExplore.Controllers.Graph;
using LitExplore.Core.Filter;


public class GraphController
{
    IFilterRepository<PublicationGraph> _fRepo;
    IPublicationRepository _pRepo;
    
    public GraphController(IFilterRepository<PublicationGraph> fRepo, 
                           IPublicationRepository pRepo) {
        this._fRepo = fRepo;
        this._pRepo = pRepo;
    }
    
    public async Task<VisualGraph> GetDefaultGraph() {
        throw new NotImplementedException();
    }

    public async Task<VisualGraph> Load(UInt64 uid) {

        // Retrieve default graph

        // if not found filter becomes EmptyFilter<PubGraph> which is fine
        var filter = await _fRepo.ReadAsync(uid);
        var graph = await GetDefaultGraph();
        // if (filter != null) graph.Filter(filter);
        return graph;
    }

    public async Task<VisualGraph> GetGraphRepresentationAsync() {
        // Fetch graph
        // THIS IS TEST DATA FOR NOW
        var publications = GraphMockData.GetPublications();

        // Find graph relations
        // This will weight publications compared to eachother.
        // And give a guess as to where they should be located on 
        //      a normalized square with (x, y) in range of [0-1]
        var graphRelation = new GraphRelation();
        var relations = graphRelation.GetManyToManyRelations(publications);

        // Generate graph
        var graph = VisualGraph.FromList(relations);
        
        // Normalize graph
        var normalizer = new GraphNormalizer();

        return normalizer.Normalize(graph);
    }
}